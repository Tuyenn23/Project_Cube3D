using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ElementBase : MonoBehaviour
{
    public E_TypeItem TypeItem;
    public E_TypeBuyItem TypeBuyItem;
    public Image Icon_img;
    public TMP_Text Title_txt;
    public TMP_Text Price_txt;


    public Button btn_Purchase;

    protected virtual void OnEnable()
    {
        btn_Purchase.onClick.AddListener(OnPurchase);
    }

    protected abstract void OnPurchase();


    protected virtual void OnDisable()
    {
        btn_Purchase.onClick.RemoveListener(OnPurchase);
    }
}
