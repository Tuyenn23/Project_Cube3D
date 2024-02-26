using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementBuyItem : MonoBehaviour
{
    public int ID;

    public TMP_Text NamePack;
    public Image Background;

    public Image Coin_img;
    public TMP_Text Coin_txt;

    public Image Magnet_img;
    public TMP_Text Magnet_txt;

    public Image Energy_img;
    public TMP_Text Energy_txt;

    public Button btn_Purchase;
    public TMP_Text Purchase_txt;

    public SHOPBUYITEM ShopBuyItem;
    public E_TypeBuyItem TypeBuyItem;

    int AmoutMegnat;
    int AmoutHeart;
    int CoinGift;

    private void OnEnable()
    {
        btn_Purchase.onClick.AddListener(OnPurchase);
    }

    public void InitDataItem(SHOPBUYITEM shop)
    {
        ShopBuyItem = shop;

        ID = shop.ID;
        TypeBuyItem = shop.TypeBuyItem;
        Background.sprite = shop.Background;
        NamePack.text = shop.NamePack;

        Coin_img.sprite = shop.IconCoin;
        Coin_txt.text = shop.AmoutCoin.ToString();
        CoinGift = shop.AmoutCoin;

        Magnet_img.sprite = shop.IconMegnat;
        Magnet_txt.text = shop.AmoutMegnat.ToString();
        AmoutMegnat = shop.AmoutMegnat;

        Energy_img.sprite = shop.IconHeart;
        Energy_txt.text = shop.AmoutHeart.ToString();
        AmoutHeart = shop.AmoutHeart;


        if (PlayerDataManager.GetListPackOwned().Count > 0)
        {
            for (int i = 0; i < PlayerDataManager.GetListPackOwned().Count; i++)
            {
                if (ID == PlayerDataManager.GetListPackOwned()[i])
                {
                    Purchase_txt.text = "Owned";
                    btn_Purchase.enabled = false;
                    break;
                }
                else
                {
                    Purchase_txt.text = shop.Price.ToString() + "d";
                }
            }
        }
        else
        {
            Purchase_txt.text = shop.Price.ToString() + "d";
        }
    }

    private void OnPurchase()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        int price = PlayerDataManager.GetTypeBuyElement(TypeBuyItem);

        if (price >= ShopBuyItem.Price)
        {
            int newPrice = price - ShopBuyItem.Price;

            PlayerDataManager.SetTypeBuyElement(TypeBuyItem, newPrice);

            int newAmoutMegnat = AmoutMegnat + PlayerDataManager.GetItemMegnat();
            PlayerDataManager.SetItemMegnat(newAmoutMegnat);

            int newAmoutHeart = AmoutHeart + PlayerDataManager.GetItemHeart();
            PlayerDataManager.SetItemHeart(newAmoutHeart);

            int newAmoutCoin = CoinGift + PlayerDataManager.GetGold();
            PlayerDataManager.SetGold(newAmoutCoin);

            EventManager.EmitEvent(EventContains.UPDATE_VIEW_HOME_COIN);

            PlayerDataManager.AddListPack(ID);
            Purchase_txt.text = "OWNED";
            btn_Purchase.enabled = false;
        }
        else
        {
            UiHome.Instance.panelEnoughMoney.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        btn_Purchase.onClick.RemoveListener(OnPurchase);

    }
}
