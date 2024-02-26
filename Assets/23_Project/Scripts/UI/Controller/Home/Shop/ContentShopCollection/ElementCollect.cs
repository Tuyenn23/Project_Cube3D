using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementCollect : MonoBehaviour
{
    public E_TypeCollection TypeShop;

    public ShopCollection ShopCollection;
    public BackgroundCollection backgroundCollection;
    public BrickCollection brickCollection;
    public EffectCollection effectCollection;

    public TMP_Text Type_txt;
    public Button btn_ShowPopup;

    SHOPCOLLECTION shops;
    private void OnEnable()
    {
        btn_ShowPopup.onClick.AddListener(OpenPopupPreview);
    }

    public void InitView(ShopCollection ShopController)
    {
        SHOPCOLLECTION shopcollection = DataController.ins.ShopCollection.getShopCollection(TypeShop);

        ShopCollection = ShopController;
        shops = shopcollection;
        Type_txt.text = TypeShop.ToString();
        backgroundCollection.TypeCollection = TypeShop;
        brickCollection.TypeCollection = TypeShop;
        effectCollection.TypeCollection = TypeShop;

        backgroundCollection.Background.sprite = shopcollection.Background;
        backgroundCollection.BackgroundInGame = shopcollection.BgInGame;
        brickCollection.Background.sprite = shopcollection.Brick;
        brickCollection.Background1.sprite = shopcollection.Brick;
        effectCollection.Background.sprite = shopcollection.EffectModel;
        effectCollection.Effect = shopcollection.Effect;
    }

    private void OpenPopupPreview()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        ShopCollection.PopupPreview.CurrentBackground = shops.Background;
        ShopCollection.PopupPreview.CurrentBrick = shops.Brick;
        ShopCollection.PopupPreview.CurrentFx = shops.Effect;
        ShopCollection.PopupPreview.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        btn_ShowPopup.onClick.RemoveListener(OpenPopupPreview);
    }
}
