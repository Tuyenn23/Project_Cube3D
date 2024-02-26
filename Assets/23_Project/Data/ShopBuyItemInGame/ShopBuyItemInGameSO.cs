using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ShopBuyItemInGame",menuName= "Data/ShopBuyItemInGame")]
public class ShopBuyItemInGameSO : SerializedScriptableObject
{
    [TableList(ShowIndexLabels = true, DefaultMinColumnWidth = 400, MaxScrollViewHeight = 200)]
    public List<SHOPBUYITEMINGAME> L_ShopItemsInGame;

    public SHOPBUYITEMINGAME GetItemByType(E_TypeItem TypeItem)
    {
        for (int i = 0; i < L_ShopItemsInGame.Count; i++)
        {
            if (L_ShopItemsInGame[i].TypeItemInGame == TypeItem)
            {
                return L_ShopItemsInGame[i];
            }
        }
        return null;
    }
}

[Serializable]
public class SHOPBUYITEMINGAME
{
    public E_TypeItem TypeItemInGame;

    public Sprite ItemIcon;
    public int AmoutItem;

    public int Price;
}
