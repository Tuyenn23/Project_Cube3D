using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController ins;

    public DataBrickSO DataBrickSO;
    public ShopCollectionSO ShopCollection;
    public ContentShopSO DataShopBuyPack;
    public ShopBuyAdsAndItemSO DataShopBuyAdsAndItem;
    public ShopBuyItemInGameSO ShopBuyItemInGame;


    private void Awake()
    {
        if(ins == null)
        {
            ins = this;
        }
    }


}
