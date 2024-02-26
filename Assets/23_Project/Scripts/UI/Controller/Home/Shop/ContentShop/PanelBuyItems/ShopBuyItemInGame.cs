using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyItemInGame : MonoBehaviour
{
    public List<ItemInGameElement> L_ItemsInGame;

    private void Start()
    {
        InitItem();
    }

    public void InitItem()
    {
        for (int i = 0; i < L_ItemsInGame.Count; i++)
        {
            L_ItemsInGame[i].initItem();
        }
    }
}
