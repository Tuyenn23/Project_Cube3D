using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyAdsAndItem : MonoBehaviour
{
    public List<ItemElement> L_items;


    private void Start()
    {
        InitItems();
    }

    public void InitItems()
    {
        for (int i = 0; i < L_items.Count; i++)
        {
            L_items[i].InitItem();
        }
    }
}
