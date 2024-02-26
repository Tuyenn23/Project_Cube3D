using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyItems : ShopContent
{
    [Header("Element")]
    public ElementBuyItem ElementBuyItemPrefab;
    public E_TypeShop TypeShop;

    public List<ElementBuyItem> L_ElementBuyItems;

    public RectTransform Content;
    public RectTransform PanelBuyItem;

    [Header("Button")]

    public List<Button> L_btnShow;


    private void OnEnable()
    {
        L_btnShow[0].onClick.AddListener(ShowMore);
        L_btnShow[1].onClick.AddListener(ShowLess);
    }

    private void Start()
    {
        InitElement(DataController.ins.DataShopBuyPack.GetListContentShopWithType(TypeShop));
        InitButtonShow();

    }



    public void InitButtonShow()
    {
        for (int i = 0; i < L_btnShow.Count; i++)
        {
            L_btnShow[i].gameObject.SetActive(false);
        }

        L_btnShow[0].gameObject.SetActive(true);
    }

    public void InitElement(List<SHOPBUYITEM> L_ShopBuyItems)
    {
        CreateElement(L_ShopBuyItems.Count);
        Content.sizeDelta = new Vector2(Content.rect.x, 2350);

        for (int i = L_ShopBuyItems.Count - 1; i >= 0; i--)
        {
            L_ElementBuyItems[i].InitDataItem(L_ShopBuyItems[i]);
            L_ElementBuyItems[i].transform.SetAsFirstSibling();
            L_ElementBuyItems[2].gameObject.SetActive(false);
            L_ElementBuyItems[3].gameObject.SetActive(false);
        }

        PanelBuyItem.gameObject.SetActive(false);
    }

    public void CreateElement(int Amoutitem)
    {
        if (L_ElementBuyItems.Count > Amoutitem) return;

        for (int i = 0; i < Amoutitem; i++)
        {
            ElementBuyItem elementBuyItem = Instantiate(ElementBuyItemPrefab, transform);
            L_ElementBuyItems.Add(elementBuyItem);

            elementBuyItem.transform.SetParent(Content);
        }
    }
    private void ShowMore()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        for (int i = 0; i < L_btnShow.Count; i++)
        {
            L_btnShow[i].gameObject.SetActive(false);
        }

        L_btnShow[1].gameObject.SetActive(true);
        Content.sizeDelta = new Vector2(Content.rect.x, 4600);
        L_ElementBuyItems[2].gameObject.SetActive(true);
        L_ElementBuyItems[3].gameObject.SetActive(true);
        PanelBuyItem.gameObject.SetActive(true);
    }

    private void ShowLess()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        for (int i = 0; i < L_btnShow.Count; i++)
        {
            L_btnShow[i].gameObject.SetActive(false);
        }

        L_btnShow[0].gameObject.SetActive(true);
        L_ElementBuyItems[2].gameObject.SetActive(false);
        L_ElementBuyItems[3].gameObject.SetActive(false);
        PanelBuyItem.gameObject.SetActive(false);
        Content.sizeDelta = new Vector2(Content.rect.x, 2350);


    }

    private void OnDisable()
    {
        L_btnShow[0].onClick.RemoveListener(ShowMore);
        L_btnShow[1].onClick.RemoveListener(ShowLess);
    }
}
