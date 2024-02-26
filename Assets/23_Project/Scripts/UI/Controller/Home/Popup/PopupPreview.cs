using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPreview : AnimScale
{
    AnimationUIController animationUIController;

    [Header("Popup Elements")]
    public Image Background;
    public List<Image> L_Bricks_img;

    public List<AnimBrick> L_ModelBricks;

    public Sprite CurrentBackground;
    public Sprite CurrentBrick;
    public ParticleSystem CurrentFx;

    public Button btn_close;



    private void OnEnable()
    {
        btn_close.onClick.AddListener(ClosePopup);
        InitView();

        for (int i = 0; i < L_ModelBricks.Count; i++)
        {
            L_ModelBricks[i].InitFx(CurrentFx);
        }
    }
    private void Start()
    {
        animationUIController = GetComponent<AnimationUIController>();
        //AppearBrick();
    }

    public void InitView()
    {
        Background.sprite = CurrentBackground;
        for (int i = 0; i < L_Bricks_img.Count; i++)
        {
            L_Bricks_img[i].sprite = CurrentBrick;
        }
    }
    private void ClosePopup()
    {
        animationUIController.ClosePopUp();
    }


    private void OnDisable()
    {
        btn_close.onClick.RemoveListener(ClosePopup);
    }
}
