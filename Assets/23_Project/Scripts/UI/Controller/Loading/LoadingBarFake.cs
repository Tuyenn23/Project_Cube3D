using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarFake : MonoBehaviour
{
    public TMP_Text text_Game;
    public Slider Loading;

    public float Duration;

    public Tweener LoadTween, Anim;

    private void Start()
    {
        StartLoading();
        AnimBackground();
    }

    public void AnimBackground()
    {
        Anim = text_Game.transform.DOScale(Vector3.one * 0.9f, 0.7f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    public void StartLoading()
    {
        Loading.value = 0;

        LoadTween = Loading.DOValue(1, Duration).OnComplete(() =>
        {
            Anim?.Kill();
            SceneManager.LoadSceneAsync(2);
        });
    }

    private void OnDisable()
    {
        LoadTween?.Kill();
    }
}
