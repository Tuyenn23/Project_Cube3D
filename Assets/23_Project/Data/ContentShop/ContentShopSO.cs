using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System;

[CreateAssetMenu(menuName = "Data/ContentShop",fileName ="ContentShop")]
public class ContentShopSO : SerializedScriptableObject
{
    [TableList(ShowIndexLabels = true, DrawScrollView = true, MinScrollViewHeight = 200, MaxScrollViewHeight = 400)]
    public Dictionary<E_TypeShop, List<SHOPBUYITEM>> L_ContentShop;


    public List<SHOPBUYITEM> GetListContentShopWithType(E_TypeShop TypeShop)
    {
        if (!L_ContentShop.ContainsKey(TypeShop))
        {
            throw new NullReferenceException();
        }
        return L_ContentShop[TypeShop];
    }
}


[Serializable]
public class SHOPBUYITEM
{
    public int ID;

    public E_TypeBuyItem TypeBuyItem;

    public string NamePack;

    public Sprite Background;

    public Sprite IconCoin;
    public int AmoutCoin;

    public Sprite IconMegnat;
    public int AmoutMegnat;

    public Sprite IconHeart;
    public int AmoutHeart;


    public int Price;
}
