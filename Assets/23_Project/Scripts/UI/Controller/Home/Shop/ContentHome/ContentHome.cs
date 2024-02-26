using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContentHome : ShopContent
{
    public Button btn_Play;
    public TMP_Text Level_txt;

    private void OnEnable()
    {
        btn_Play.onClick.AddListener(OnPlayGame);
    }

    private void Start()
    {
        Level_txt.text = $"Level {PlayerDataManager.GetCurrentLevel()}";
    }

    private void OnPlayGame()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        SceneManager.LoadScene(2);
    }
}
