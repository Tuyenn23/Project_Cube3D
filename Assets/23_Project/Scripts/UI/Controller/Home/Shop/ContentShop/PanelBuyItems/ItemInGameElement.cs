using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInGameElement : ElementBase
{
    public int ID;
    SHOPBUYITEMINGAME Shop;
    public void initItem()
    {
        SHOPBUYITEMINGAME ShopBuyItemInGame = DataController.ins.ShopBuyItemInGame.GetItemByType(TypeItem);
        Shop = ShopBuyItemInGame;

        ID = transform.GetSiblingIndex() + 1; 
        Icon_img.sprite = ShopBuyItemInGame.ItemIcon;
        Title_txt.text ="x"+ShopBuyItemInGame.AmoutItem.ToString();

        if (PlayerDataManager.GetListItems().Count > 0)
        {
            for (int i = 0; i < PlayerDataManager.GetListItems().Count; i++)
            {
                if (ID == PlayerDataManager.GetListItems()[i])
                {
                    Price_txt.text = "Owned";
                    btn_Purchase.enabled = false;
                    break;
                }
                else
                {
                    Price_txt.text = ShopBuyItemInGame.Price.ToString();
                }
            }
        }
        else
        {
            Price_txt.text = ShopBuyItemInGame.Price.ToString();
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

            int newItem = Shop.AmoutItem + PlayerDataManager.GetItemAmout(TypeItem);
            PlayerDataManager.SetItemAmout(TypeItem, newItem);
            PlayerDataManager.AddListItems(ID);

            Price_txt.text = "OWNED";
            btn_Purchase.enabled = false;
        }
        else
        {
            UiHome.Instance.panelEnoughMoney.gameObject.SetActive(true);
        }
    }
}
