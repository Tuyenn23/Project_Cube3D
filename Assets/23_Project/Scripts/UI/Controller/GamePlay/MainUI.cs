using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public ListItemPicked ListItemPicked;
    public ItemsAndCombo itemsAndCombo;


    [Header("Process Time CountRemaining Down")]
    [SerializeField] private TMP_Text m_TimeCountDownTxt;
    private float TimeCountDown;
    public int MaxTime;
    private float minus;
    private float Second;

    [Header("Level")]
    [SerializeField] private TMP_Text m_LevelTxt;

    [Header("Rate")]
    [SerializeField] private TMP_Text m_RateTxt;
    public Image ImageRate;
    public int CountRate;

    public Button btn_setting;

    Tweener TweenTimer;

    public float TimeCountDown1 { get => TimeCountDown; set => TimeCountDown = value; }

    private void OnEnable()
    {
        EventManager.StartListening(EventContains.UPDATE_VIEW_RATE, InitRate);
        btn_setting.onClick.AddListener(OpenPopupSetting);
    }

    public void LoadUiGamePlay()
    {
        EventManager.EmitEvent(EventContains.UPDATE_VIEW_RATE);
        m_LevelTxt.text = $"Level: {PlayerDataManager.GetCurrentLevel()}";
        MaxTime = GameManager.Instance.LevelManager.TimeInLevel;
        itemsAndCombo.countCombo = 0;
        itemsAndCombo.FillCombo.gameObject.SetActive(false);
        itemsAndCombo.QuantityCollection = 0;
    }


    private void FixedUpdate()
    {
        CountTime();
    }

    public void AnimTimer()
    {
        TweenTimer = m_TimeCountDownTxt.transform.DOScale(1.2f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            TweenTimer = m_TimeCountDownTxt.transform.DOScale(1f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                TweenTimer.Kill();
            });
        });
    }


    public void CountTime()
    {
        if (MaxTime < 0 || GameManager.Instance.isWinLevel || GameManager.Instance.isLoseLevel || GameManager.Instance.isReceiving) return;

        TimeCountDown1 += Time.deltaTime;

        if (TimeCountDown1 > 1)
        {
            TimeCountDown1 -= 1;
            MaxTime -= 1;

            minus = MaxTime % 3660 / 60;
            Second = MaxTime % 3660 % 60;

            if (MaxTime >= 0)
            {
                m_TimeCountDownTxt.text = $"{(int)minus}m:{(int)Second}s";
            }
            else
            {
                SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundnotiTimeOut);
                GameManager.Instance.UiGamePlay.ProcessWinLose(E_LevelResult.Lose);
            }

            if (MaxTime <= 10)
            {
                if (SoundManager.Instance.SoundAudio.isPlaying == SoundManager.Instance.soundTimeOut) return;

                    SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundTimeOut);
            }
        }
    }

    private void InitRate()
    {
        m_RateTxt.text = GameManager.Instance.StarInLevel.ToString();
    }

    private void OpenPopupSetting()
    {
        GameManager.Instance.PauseGame();
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        GameManager.Instance.UiGamePlay.PopupPause.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventContains.UPDATE_VIEW_RATE, InitRate);
        btn_setting.onClick.RemoveListener(OpenPopupSetting);
    }
}
