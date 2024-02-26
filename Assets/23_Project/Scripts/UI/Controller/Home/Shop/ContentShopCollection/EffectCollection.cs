using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCollection : MonoBehaviour
{
    [Header("Shop")]
    public ShopCollection shopCollection;
    public ElementCollect ElementCollect;
    public E_TypeCollection TypeCollection;

    [Header("Infor EffectModel")]
    public int ID;
    [SerializeField] bool isBuyed;
    [SerializeField] bool isUsing;

    public Image Background;
    public ParticleSystem Effect;

    public List<Button> L_btnBackground;

    bool isInit = false;

    private void OnEnable()
    {
        L_btnBackground[0].onClick.AddListener(BuyEffect);
        L_btnBackground[1].onClick.AddListener(UseEffect);
    }

    private void Start()
    {

    }

    public void InitID(int id)
    {
        ID = id;

        int CurrentIDSave = PlayerDataManager.GetEffectUsing();

        InitEffectBuyed();

        if (ID == CurrentIDSave)
        {
            isBuyed = true;
            isUsing = true;
            UseEffect();
            isInit = true;
        }
    }

    public void InitEffectBuyed()
    {
        int[] EffectBuyed = new int[PlayerDataManager.GetListEffectBuyed().Count];

        for (int i = 0; i < EffectBuyed.Length; i++)
        {
            EffectBuyed[i] = PlayerDataManager.GetListEffectBuyed()[i];
        }

        if (EffectBuyed.Length == 0) return;

        for (int i = 0; i < EffectBuyed.Length; i++)
        {
            if (EffectBuyed[i] == ID)
            {
                BuyEffect();
            }
        }
    }

    private void BuyEffect()
    {
        if(isInit)
        {
            SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        }
        isBuyed = true;

        for (int i = 0; i < L_btnBackground.Count; i++)
        {
            L_btnBackground[i].gameObject.SetActive(false);
        }

        L_btnBackground[1].gameObject.SetActive(true);

        PlayerDataManager.AddListEffectBuyed(ID);
    }
    private void UseEffect()
    {
        if (isInit)
        {
            SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        }

        GameObject TickEffect = shopCollection.TickEffectOwned;

        TickEffect.transform.SetParent(transform);
        TickEffect.transform.localPosition = Vector3.zero;
        TickEffect.transform.localScale = Vector3.one;
        TickEffect.gameObject.SetActive(true);
        shopCollection.YourCollectEffectModelSave = Background.sprite;
        shopCollection.YourCollectEffectSave = Effect;
        shopCollection.UpdateYourCollection();
        PlayerDataManager.SetEffectUsing(ID);
        PlayerDataManager.AddListEffectBuyed(ID);

        for (int i = 0; i < shopCollection.L_Effect.Count; i++)
        {
            if (shopCollection.L_Effect[i].isBuyed)
            {
                shopCollection.L_Effect[i].isUsing = false;

                shopCollection.L_Effect[i].L_btnBackground[1].gameObject.SetActive(true);
                shopCollection.L_Effect[i].L_btnBackground[0].gameObject.SetActive(false);
            }
            else
            {
                shopCollection.L_Effect[i].isBuyed = false;
                shopCollection.L_Effect[i].isUsing = false;
                shopCollection.L_Effect[i].L_btnBackground[0].gameObject.SetActive(true);
                shopCollection.L_Effect[i].L_btnBackground[1].gameObject.SetActive(false);
            }
        }

        isUsing = true;

        for (int i = 0; i < L_btnBackground.Count; i++)
        {
            L_btnBackground[i].gameObject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        L_btnBackground[0].onClick.RemoveAllListeners();
        L_btnBackground[1].onClick.RemoveAllListeners();
    }
}
