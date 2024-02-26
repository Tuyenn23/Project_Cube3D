using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static class PlayerDataManager
{
    private static string DATA_SHOP_COLLECTION = "DATASHOPCOLLECTION";
    private static string DATA_LEVEL = "DataLevel";
    private static string DATA_SHOPBUYITEM = "DATASHOPBUYITEM";

    private static SAVESHOPCOLLECTION dataShopCollection;
    private static DATALEVEL dataLevel;
    private static SAVESHOPBUYITEMS dataBuyItems;

    static PlayerDataManager()
    {

        dataShopCollection = JsonConvert.DeserializeObject<SAVESHOPCOLLECTION>(PlayerPrefs.GetString(DATA_SHOP_COLLECTION));

        if (dataShopCollection == null)
        {
            dataShopCollection = new SAVESHOPCOLLECTION
            {
                L_BackgroundBuyed = new List<int>(),

                L_BrickBuyed = new List<int>(),

                L_EffectBuyed = new List<int>()
            };

        }

        dataLevel = JsonConvert.DeserializeObject<DATALEVEL>(PlayerPrefs.GetString(DATA_LEVEL));

        if (dataLevel == null)
        {
            dataLevel = new DATALEVEL();
            dataLevel.CurrentLevel = 1;
        }

        dataBuyItems = JsonConvert.DeserializeObject<SAVESHOPBUYITEMS>(PlayerPrefs.GetString(DATA_SHOPBUYITEM));

        if (dataBuyItems == null)
        {
            dataBuyItems = new SAVESHOPBUYITEMS
            {
                L_AdsAndCoinOwned = new List<int>(),
                L_ItemsOwned = new List<int>(),
                L_PackOwned = new List<int>()
            };
        }

        SaveDataShopBuyItems();
        SaveDataLevel();
        SaveDataShopCollection();
    }


    public static void SaveDataShopCollection()
    {
        string data = JsonConvert.SerializeObject(dataShopCollection);

        PlayerPrefs.SetString(DATA_SHOP_COLLECTION, data);
    }

    public static void SaveDataLevel()
    {
        string data = JsonConvert.SerializeObject(dataLevel);

        PlayerPrefs.SetString(DATA_LEVEL, data);
    }

    public static void SaveDataShopBuyItems()
    {
        string data = JsonConvert.SerializeObject(dataBuyItems);

        PlayerPrefs.SetString(DATA_SHOPBUYITEM, data);
    }


    public static int GetGold()
    {
        return PlayerPrefs.GetInt(Helper.GOLD, 0);
    }

    public static void SetGold(int Quantity)
    {
        PlayerPrefs.SetInt(Helper.GOLD, Quantity);
    }

    public static int GetStar()
    {
        return PlayerPrefs.GetInt(Helper.STAR, 0);
    }

    public static int GetTypeBuyElement(E_TypeBuyItem Type)
    {
        if (Type == E_TypeBuyItem.Coin)
        {
            return GetGold();
        }
        else
        {
            return GetStar();
        }
    }

    public static void SetTypeBuyElement(E_TypeBuyItem Type, int Quantity)
    {
        if (Type == E_TypeBuyItem.Coin)
        {
            SetGold(Quantity);
        }
        else
        {
            SetStar(Quantity);
        }
    }

    public static void SetStar(int Quantity)
    {
        PlayerPrefs.SetInt(Helper.STAR, Quantity);
    }

    public static int GetBackgroundUsing()
    {
        return PlayerPrefs.GetInt(Helper.BACKGROUND, 1);
    }

    public static void SetBackgroundUsing(int value)
    {
        PlayerPrefs.SetInt(Helper.BACKGROUND, value);
    }

    public static int GetBrickUsing()
    {
        return PlayerPrefs.GetInt(Helper.BRICK, 1);
    }

    public static void SetBrickUsing(int value)
    {
        PlayerPrefs.SetInt(Helper.BRICK, value);
    }

    public static int GetEffectUsing()
    {
        return PlayerPrefs.GetInt(Helper.EFFECT, 1);
    }

    public static void SetEffectUsing(int value)
    {
        PlayerPrefs.SetInt(Helper.EFFECT, value);
    }

    public static int GetItemAmout(E_TypeItem Type)
    {
        return PlayerPrefs.GetInt(Helper.AMOUT_ITEM + Type, 0);
    }

    public static void SetItemAmout(E_TypeItem Type, int value)
    {
        PlayerPrefs.SetInt(Helper.AMOUT_ITEM + Type, value);
    }

    public static int GetItemMegnat()
    {
        return PlayerPrefs.GetInt(Helper.ITEM_MERGE, 0);
    }

    public static void SetItemMegnat(int value)
    {
        PlayerPrefs.SetInt(Helper.ITEM_MERGE, value);
    }

    public static int GetItemPinWheel()
    {
        return PlayerPrefs.GetInt(Helper.AMOUT_ITEM + "Pinwheel", 0);
    }

    public static void SetItemPinWheel(int value)
    {
        PlayerPrefs.SetInt(Helper.AMOUT_ITEM + "Pinwheel", value);
    }

    public static int GetItemEnergy()
    {
        return PlayerPrefs.GetInt(Helper.ITEM_ENERGY, 0);
    }

    public static void SetItemEnergy(int value)
    {
        PlayerPrefs.SetInt(Helper.ITEM_ENERGY, value);
    }

    public static int GetItemHeart()
    {
        return PlayerPrefs.GetInt(Helper.ITEM_HEART, 0);
    }

    public static void SetItemHeart(int value)
    {
        PlayerPrefs.SetInt(Helper.ITEM_HEART, value);
    }


    public static int GetItemTimer()
    {
        return PlayerPrefs.GetInt(Helper.ITEM_TIMER, 0);
    }

    public static void SetItemTimer(int value)
    {
        PlayerPrefs.SetInt(Helper.ITEM_TIMER, value);
    }

    public static void SetSound(bool isOn)
    {
        PlayerPrefs.SetInt(Helper.SOUND, isOn ? 1 : 0);
    }

    public static bool GetSound()
    {
        return PlayerPrefs.GetInt(Helper.SOUND, 1) == 1;
    }


    public static void SetMusic(bool isOn)
    {
        PlayerPrefs.SetInt(Helper.MUSIC, isOn ? 1 : 0);
    }

    public static bool GetMusic()
    {
        return PlayerPrefs.GetInt(Helper.MUSIC, 1) == 1;
    }




    public static int GetCurrentLevel()
    {
        return dataLevel.GetCurrentLevel();
    }

    public static void SetLevel(int level)
    {
        dataLevel.SetLevel(level);
        SaveDataLevel();
    }

    public static int GetTotalLevel(string path)
    {
        return dataLevel.GetTotalLevel(path);
    }

    public static List<int> GetListBackGroundBuyed()
    {
        return dataShopCollection.GetListBackGroundBuyed();
    }

    public static void AddListBackGroundBuyed(int id)
    {
        dataShopCollection.AddListBackGroundBuyed(id);

        SaveDataShopCollection();
    }

    public static List<int> GetListBrickBuyed()
    {
        return dataShopCollection.GetListBrickBuyed();
    }

    public static void AddListBrickBuyed(int id)
    {
        dataShopCollection.AddListBrickBuyed(id);

        SaveDataShopCollection();
    }

    public static List<int> GetListEffectBuyed()
    {
        return dataShopCollection.GetListEffectBuyed();
    }


    public static void AddListEffectBuyed(int id)
    {
        dataShopCollection.AddListEffectBuyed(id);

        SaveDataShopCollection();
    }


    public static List<int> GetListPackOwned()
    {
        return dataBuyItems.GetListPackOwned();
    }

    public static void AddListPack(int id)
    {
        dataBuyItems.AddListPack(id);

        SaveDataShopBuyItems();
    }


    public static List<int> GetListAdsAndCoin()
    {
        return dataBuyItems.GetListAdsAndCoin();
    }

    public static void AddListAdsAndCoin(int id)
    {
        dataBuyItems.AddListAdsAndCoin(id);
        SaveDataShopBuyItems();
    }


    public static List<int> GetListItems()
    {
        return dataBuyItems.GetListItems();
    }

    public static void AddListItems(int id)
    {
        dataBuyItems.AddListItems(id);
        SaveDataShopBuyItems();
    }


}

public class DATALEVEL
{
    public int CurrentLevel;

    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    public void SetLevel(int Level)
    {
        CurrentLevel = Level;
    }

    public int GetTotalLevel(string path)
    {
        int _count = 0;

        GameObject[] Resources1 = Resources.LoadAll<GameObject>(path);
        _count = Resources1.Length;

        return _count;
    }
}

public class SAVESHOPCOLLECTION
{
    public List<int> L_BackgroundBuyed;
    public List<int> L_BrickBuyed;
    public List<int> L_EffectBuyed;


    public List<int> GetListBackGroundBuyed()
    {
        return L_BackgroundBuyed;
    }

    public void AddListBackGroundBuyed(int id)
    {
        if (L_BackgroundBuyed.Contains(id)) return;

        L_BackgroundBuyed.Add(id);
    }


    public List<int> GetListBrickBuyed()
    {
        return L_BrickBuyed;
    }

    public void AddListBrickBuyed(int id)
    {
        if (L_BrickBuyed.Contains(id)) return;

        L_BrickBuyed.Add(id);
    }


    public List<int> GetListEffectBuyed()
    {
        return L_EffectBuyed;
    }

    public void AddListEffectBuyed(int id)
    {
        if (L_EffectBuyed.Contains(id)) return;

        L_EffectBuyed.Add(id);
    }
}

public class SAVESHOPBUYITEMS
{
    public List<int> L_PackOwned;
    public List<int> L_AdsAndCoinOwned;
    public List<int> L_ItemsOwned;


    public List<int> GetListPackOwned()
    {
        return L_PackOwned;
    }

    public void AddListPack(int id)
    {
        if (L_PackOwned.Contains(id)) return;

        L_PackOwned.Add(id);
    }


    public List<int> GetListAdsAndCoin()
    {
        return L_AdsAndCoinOwned;
    }

    public void AddListAdsAndCoin(int id)
    {
        if (L_AdsAndCoinOwned.Contains(id)) return;

        L_AdsAndCoinOwned.Add(id);
    }


    public List<int> GetListItems()
    {
        return L_ItemsOwned;
    }

    public void AddListItems(int id)
    {
        if (L_ItemsOwned.Contains(id)) return;

        L_ItemsOwned.Add(id);
    }
}
