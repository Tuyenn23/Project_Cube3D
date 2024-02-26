using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName ="Data/ShopCollection",fileName ="ShopCollection")]
public class ShopCollectionSO : SerializedScriptableObject
{
    public List<SHOPCOLLECTION> L_ShopCollection;

    public SHOPCOLLECTION getShopCollection(E_TypeCollection Type)
    {
        for (int i = 0; i < L_ShopCollection.Count; i++)
        {
            if (L_ShopCollection[i].TypE_TypeCollection == Type)
            {
                return L_ShopCollection[i];
            }
        }
        return null;
    }
}

public class SHOPCOLLECTION
{
    public E_TypeCollection TypE_TypeCollection;

    public Sprite Background;
    public Sprite BgInGame;
    public Sprite Brick;
    public Sprite EffectModel;
    public ParticleSystem Effect;
}
