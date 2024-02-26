using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGamePlay : MonoBehaviour
{
    public MainUI MainUi;

    public RectTransform ContentWinlose;
    public PopupPause PopupPause;
    public PopupWin ContentWin;
    public PopupLose ContentLose;
    public LoadingBarFake FakeLoading;
    public Canvas CanvasLoading;
    public PopupReceive PopupReceive;
    public PanelEnoughMoney PopupEnoughMoney;

    public SkeletonGraphic Skeleton;

    public Image BackGroundInGame;
    public Sprite BrickInGame;


    private void Start()
    {
        InitInGame();
        MainUi.ListItemPicked.InitListItem(BrickInGame);
        ContentWinlose.gameObject.SetActive(false);
        PopupPause.gameObject.SetActive(false);
        ContentLose.gameObject.SetActive(false);
        PopupReceive.gameObject.SetActive(false);
        PopupEnoughMoney.gameObject.SetActive(false);
        CanvasLoading.gameObject.SetActive(false);
    }

    public void InitInGame()
    {
        BackGroundInGame.sprite = PrefabStorage.Instance.BackgroundCurrent;
        BrickInGame = PrefabStorage.Instance.BrickCurrent;
    }


    public void ProcessWinLose(E_LevelResult levelResult)
    {
        switch (levelResult)
        {
            case E_LevelResult.None:
                break;
            case E_LevelResult.Win:
                ShowPoupWin();
                break;
            case E_LevelResult.Lose:
                ShowPopupLose();
                break;
            default:
                break;
        }
    }

    public void ShowPoupWin()
    {
        ContentWinlose.gameObject.SetActive(true);
        GameManager.Instance.isWinLevel = true;
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundWin);
        Skeleton.AnimationState.SetAnimation(0, "Appear_win", false);
        /*Skeleton.gameObject.SetActive(true);*/
        Skeleton.AnimationState.Complete += _ =>
        {
            if (_.Animation.Name == "Appear_win")
            {
                Skeleton.AnimationState.SetAnimation(0, "Idle_win", false);
                ContentWin.gameObject.SetActive(true);
            }
        };
    }

    public void ShowPopupLose()
    {
        GameManager.Instance.isLoseLevel = true;

        ContentWinlose.gameObject.SetActive(true);
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundLose);
        Skeleton.AnimationState.SetAnimation(0, "Appear_lose", false);
        /* Skeleton.gameObject.SetActive(true);*/
        Skeleton.AnimationState.Complete += _ =>
        {
            if (_.Animation.Name == "Appear_lose")
            {
                Skeleton.AnimationState.SetAnimation(0, "Idle_lose", false);
                ContentLose.gameObject.SetActive(true);
            }
        };
    }
}
