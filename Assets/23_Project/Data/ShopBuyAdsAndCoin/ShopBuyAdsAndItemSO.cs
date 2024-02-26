using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System;

[CreateAssetMenu(fileName ="ShopBuyItemAndAds",menuName = "Data/ShopBuyItemAndAds")]
public class ShopBuyAdsAndItemSO : SerializedScriptableObject
{
    [TableList(ShowIndexLabels = true, DefaultMinColumnWidth = 400, MaxScrollViewHeight = 200)]
    public List<SHOPBUYADSANDITEM> L_ShopAdsAndItem;

    public SHOPBUYADSANDITEM GetItemByType(E_TypeItem Type)
    {
        for (int i = 0; i < L_ShopAdsAndItem.Count; i++)
        {
            if (L_ShopAdsAndItem[i].TypeItem == Type)
            {
                return L_ShopAdsAndItem[i];
            }
        }
        return null;
    }
}

[Serializable]
public class SHOPBUYADSANDITEM
{
    public E_TypeItem TypeItem;
    public Sprite BgItem_img;
    public string NameItem;
    public int Price;

}
