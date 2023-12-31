using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(()=>{Loader.Load(Loader.Scene.MainMenu);});
    }
    private void Start() {
        GameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnpaused;

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
