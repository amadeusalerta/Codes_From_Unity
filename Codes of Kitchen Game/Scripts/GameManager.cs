using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get;private set;}
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer=1f;
    private float countdawnToStartTimer=3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax=20f;
    private bool isGamePaused=false;

    private void Awake()
    {
        Instance=this;
        state=State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction+=Instance_OnPauseAction;
    }

    private void Instance_OnPauseAction(object sender,System.EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
{
    switch(state)
    {
        case State.WaitingToStart:
            waitingToStartTimer -= Time.deltaTime;
            if(waitingToStartTimer < 0f)
            {
                state = State.CountDownToStart;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
        case State.CountDownToStart:
            countdawnToStartTimer -= Time.deltaTime;
            if(countdawnToStartTimer < 0f)
            {
                state = State.GamePlaying; // Burayı düzelt
                gamePlayingTimer=gamePlayingTimerMax;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
        case State.GamePlaying:
            gamePlayingTimer -= Time.deltaTime;
            if(gamePlayingTimer < 0f)
            {
                state = State.GameOver; // Burayı düzelt
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
        case State.GameOver:
            break;
    }
}

    public bool isGamePlaying()
    {
        return state==State.GamePlaying;
    }

    public bool isCountdownToStartActive()
    {
        return state==State.CountDownToStart;
    }
    public float GetCountdownToStartTimer()
    {
        return countdawnToStartTimer;
    }

    public bool isGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1-(gamePlayingTimer/gamePlayingTimerMax);
    }

    private void TogglePauseGame()
    {
        isGamePaused=!isGamePaused;
        if(isGamePaused)
        {
            Time.timeScale=0f;
            OnGamePaused?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            Time.timeScale=1f;
            OnGameUnPaused?.Invoke(this,EventArgs.Empty);
        }
    }
}
