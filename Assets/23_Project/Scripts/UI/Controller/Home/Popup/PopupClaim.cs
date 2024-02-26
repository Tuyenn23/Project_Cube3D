using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PopupClaim : AnimScale
{
    AnimationUIController animationUIController;
    public TMP_Text Star_txt;
    public TMP_Text Gold_text;

    public Button btn_Claim;

    private void OnEnable()
    {
        InitClaim();
        btn_Claim.onClick.AddListener(OnClaim);
    }

    private void Start()
    {
        animationUIController = GetComponent<AnimationUIController>();
    }

    public void InitClaim()
    {
        int CoinWithoutBonus = GameManager.Instance.CoinInLevel;
        int StarWithoutBonus = GameManager.Instance.StarInLevel;

        DOTween.To(() => StarWithoutBonus, x => StarWithoutBonus = x, GameManager.Instance.UiGamePlay.ContentWin.newStarBonus, 2f)
     .OnUpdate(() =>
     {
         Star_txt.text = StarWithoutBonus.ToString();
     })
     .SetEase(Ease.Linear);


        DOTween.To(() => CoinWithoutBonus, x => CoinWithoutBonus = x, GameManager.Instance.UiGamePlay.ContentWin.newCoinBonus, 2f)
     .OnUpdate(() =>
     {
         Gold_text.text = CoinWithoutBonus.ToString();

     })
     .SetEase(Ease.Linear);
    }

    private void OnClaim()
    {
        animationUIController.ClosePopUp();
    }

    private void OnDisable()
    {
        btn_Claim.onClick.RemoveListener(OnClaim);
    }
}
