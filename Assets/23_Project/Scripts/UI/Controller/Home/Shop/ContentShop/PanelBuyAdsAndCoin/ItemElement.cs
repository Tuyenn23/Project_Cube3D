using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemElement : ElementBase
{
    public int id;
    SHOPBUYADSANDITEM Shop;
    public void InitItem()
    {
        SHOPBUYADSANDITEM ShopBuyAdsAndItem = DataController.ins.DataShopBuyAdsAndItem.GetItemByType(TypeItem);

        id = transform.GetSiblingIndex() + 1;
        Shop = ShopBuyAdsAndItem;
        Icon_img.sprite = ShopBuyAdsAndItem.BgItem_img;
        Title_txt.text = ShopBuyAdsAndItem.NameItem;

        if (PlayerDataManager.GetListAdsAndCoin().Count > 0)
        {
            for (int i = 0; i < PlayerDataManager.GetListAdsAndCoin().Count; i++)
            {
                if (id == PlayerDataManager.GetListAdsAndCoin()[i])
                {
                    Price_txt.text = "Owned";
                    btn_Purchase.enabled = false;
                    break;
                }
                else
                {
                    Price_txt.text = ShopBuyAdsAndItem.Price.ToString();
                }
            }
        }
        else
        {
            Price_txt.text = ShopBuyAdsAndItem.Price.ToString();
        }
    }

    protected override void OnPurchase()
    {
        int price = PlayerDataManager.GetTypeBuyElement(TypeBuyItem);

        if (price >= Shop.Price)
        {
            int newPrice = price - Shop.Price;

            PlayerDataManager.SetTypeBuyElement(TypeBuyItem, newPrice);
            EventManager.EmitEvent(EventContains.UPDATE_VIEW_HOME_COIN);
            Price_txt.text = "OWNED";
            PlayerDataManager.AddListAdsAndCoin(id);
            btn_Purchase.enabled = false;
        }
        else
        {
            UiHome.Instance.panelEnoughMoney.gameObject.SetActive(true);
        }
    }
}
