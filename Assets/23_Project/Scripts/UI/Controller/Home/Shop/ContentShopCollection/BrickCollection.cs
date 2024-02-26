using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickCollection : MonoBehaviour
{
    public int ID;

    public ShopCollection shopcollection;
    public ElementCollect ElementCollect;
    public E_TypeCollection TypeCollection;

    [SerializeField] bool isBuyed;
    [SerializeField] bool isUsing;

    public Image Background;
    public Image Background1;

    public List<Button> L_btnBackground;

    bool isInit = false;

    private void OnEnable()
    {
        L_btnBackground[0].onClick.AddListener(BuyBrick);
        L_btnBackground[1].onClick.AddListener(UseBrick);
    }

    private void Start()
    {

    }

    public void InitIDBrick(int id)
    {
        ID = id;
        InitBrickBuyed();

        int CurrentIDSave = PlayerDataManager.GetBrickUsing();

        if (ID == CurrentIDSave)
        {
            UseBrick();
            isInit = true;
        }
    }

    public void InitBrickBuyed()
    {
        int[] ArraybrickBuyed = new int[PlayerDataManager.GetListBrickBuyed().Count];

        for (int i = 0; i < ArraybrickBuyed.Length; i++)
        {
            ArraybrickBuyed[i] = PlayerDataManager.GetListBrickBuyed()[i];
        }

        if (ArraybrickBuyed.Length == 0) return;

        for (int i = 0; i < ArraybrickBuyed.Length; i++)
        {
            if (ArraybrickBuyed[i] == ID)
            {
                BuyBrick();
            }
        }
    }

    private void BuyBrick()
    {
        if(isInit)
        {
            SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        }

        for (int i = 0; i < L_btnBackground.Count; i++)
        {
            L_btnBackground[i].gameObject.SetActive(false);
        }

        isBuyed = true;

        L_btnBackground[1].gameObject.SetActive(true);

        PlayerDataManager.AddListBrickBuyed(ID);
    }

    private void UseBrick()
    {
        if (isInit)
        {
            SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        }

        GameObject TickBrick = shopcollection.TickBrickOwned;
        TickBrick.transform.SetParent(transform);
        TickBrick.transform.localPosition = Vector3.zero;
        TickBrick.transform.localScale = Vector3.one;
        TickBrick.gameObject.SetActive(true);
        shopcollection.YourCollectBrickSave = Background.sprite;
        shopcollection.UpdateYourCollection();
        PlayerDataManager.SetBrickUsing(ID);
        PlayerDataManager.AddListBrickBuyed(ID);

        for (int i = 0; i < shopcollection.L_Brick.Count; i++)
        {
            if (shopcollection.L_Brick[i].isBuyed)
            {
                shopcollection.L_Brick[i].isUsing = false;

                shopcollection.L_Brick[i].L_btnBackground[1].gameObject.SetActive(true);
                shopcollection.L_Brick[i].L_btnBackground[0].gameObject.SetActive(false);
            }
            else
            {
                shopcollection.L_Brick[i].isBuyed = false;
                shopcollection.L_Brick[i].isUsing = false;

                shopcollection.L_Brick[i].L_btnBackground[0].gameObject.SetActive(true);
                shopcollection.L_Brick[i].L_btnBackground[1].gameObject.SetActive(false);
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
