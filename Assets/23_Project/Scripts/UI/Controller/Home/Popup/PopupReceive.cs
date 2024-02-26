using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupReceive : AnimScale
{
    AnimationUIController AnimationUIController;
    public TMP_Text CoinReceive_txt;

    public int CoinCanReceive;

    public Button btn_close;

    public ParticleSystem LightRay;

    private void OnEnable()
    {
        GameManager.Instance.isReceiving = true;
        InitCoin();
        LightRay.Play();
        btn_close.onClick.AddListener(OnReceive);
    }

    private void Start()
    {
        AnimationUIController = GetComponent<AnimationUIController>();
    }

    public void InitCoin()
    {
         CoinCanReceive = Random.Range(5, 10);

        CoinReceive_txt.text ="X" + CoinCanReceive.ToString();
    }

    private void OnReceive()
    {
        GameManager.Instance.isReceiving = false;
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundCoinReceive);
        GameManager.Instance.CoinInLevel += CoinCanReceive;

        int newCoin = PlayerDataManager.GetGold() + CoinCanReceive;

        PlayerDataManager.SetGold(newCoin);
        

        AnimationUIController.ClosePopUp();
    }

    private void OnDisable()
    {
        btn_close.onClick.RemoveListener(OnReceive);
    }


}