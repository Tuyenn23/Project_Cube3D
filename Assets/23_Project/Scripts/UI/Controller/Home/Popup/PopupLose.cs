using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupLose : AnimScale
{
    AnimationUIController animationUIController;

    [Header("Button")]
    public Button btn_Close;
    public Button btn_Continue;

    private void OnEnable()
    {
        InitButton();
    }

    private void Start()
    {
    }
    public void InitButton()
    {
        btn_Close.onClick.AddListener(PlayAgain);
        btn_Continue.onClick.AddListener(ContinueGame);
    }


    private void PlayAgain()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        GameManager.Instance.ResumeGame();
        GameManager.Instance.UiGamePlay.CanvasLoading.gameObject.SetActive(true);
        GameManager.Instance.isLoseLevel = false;
    }

    private void ContinueGame()
    {
        int CurrentCoin = PlayerDataManager.GetGold();

        if(CurrentCoin >= 1000)
        {
            GameManager.Instance.isLoseLevel = false;

            if(GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.CheckIsFullItem())
            {
                GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.Move2BrickToStartPos();
                GameManager.Instance.UiGamePlay.MainUi.MaxTime += 60;
                GameManager.Instance.ResumeGame();
            }
            else
            {
                GameManager.Instance.UiGamePlay.MainUi.MaxTime += 60;
                GameManager.Instance.ResumeGame();
            }


            GameManager.Instance.UiGamePlay.ContentWinlose.gameObject.SetActive(false);
            GameManager.Instance.UiGamePlay.ContentWinlose.gameObject.SetActive(false);
            GameManager.Instance.UiGamePlay.ContentLose.gameObject.SetActive(false);


            int newCoin = PlayerDataManager.GetGold() - 1000;
            PlayerDataManager.SetGold(newCoin);
        }
        else
        {
            GameManager.Instance.UiGamePlay.PopupEnoughMoney.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        btn_Close.onClick.RemoveListener(PlayAgain);
        btn_Continue.onClick.RemoveListener(ContinueGame);
    }
}
