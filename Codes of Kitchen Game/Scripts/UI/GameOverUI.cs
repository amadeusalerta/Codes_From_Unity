using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
        [SerializeField] private TextMeshProUGUI recipiesDeliveredText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged+=GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender,System.EventArgs e)
    {
        if(GameManager.Instance.isGameOver())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        recipiesDeliveredText.text=DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
