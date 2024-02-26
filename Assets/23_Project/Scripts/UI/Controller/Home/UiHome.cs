using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiHome : MonoBehaviour
{
    public static UiHome Instance;

    [Header("UI Home")]
    public TMP_Text Coin_txt;
    public TMP_Text Star_txt;
    public TMP_Text Energy_txt;

    public Button btn_AddGold;
    public Button btn_Setting;

    [Header("Shops")]
    public List<ShopTab> L_ShopsTab;
    public List<ShopContent> L_ShopsContent;

    public SkeletonAnimation Levelfx;

    [Header("Bg")]
    public List<Sprite> L_bgs;
    public GameObject BgHome;

    public ShopCollection shopCollection;
    public PanelEnoughMoney panelEnoughMoney;
    public PopupHomeSetting popupHomeSetting;


    private void OnEnable()
    {
        InitViewCoin();
        EventManager.StartListening(EventContains.UPDATE_VIEW_HOME_COIN, InitViewCoin);
        btn_AddGold.onClick.AddListener(AddGold);
        btn_Setting.onClick.AddListener(OpenSetting);
    }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        panelEnoughMoney.gameObject.SetActive(false);
        InitContentCollection();
        InitConentTab(2);
    }

    private void OpenSetting()
    {
        Levelfx.gameObject.SetActive(false);
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        popupHomeSetting.gameObject.SetActive(true);
    }

    private void AddGold()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        int CurrentGold = PlayerDataManager.GetGold();

        CurrentGold += 100;
        PlayerDataManager.SetGold(CurrentGold);

        InitViewCoin();
    }

    public void InitContentCollection()
    {
        shopCollection.InitView();

        //shopCollection.UpdateYourCollection();

        shopCollection.InitTickBg();
        shopCollection.InitIDBackground();

        shopCollection.InitTickBrick();
        shopCollection.InitIDBrick();

        shopCollection.InitTickEffect();
        shopCollection.InitIDEffect();

        //shopCollection.gameObject.SetActive(false);
    }

    public void InitViewCoin()
    {
        Coin_txt.text = PlayerDataManager.GetGold().ToString();
        Star_txt.text = PlayerDataManager.GetStar().ToString();
        Energy_txt.text = $"{PlayerDataManager.GetItemHeart()}/40";
    }

    public void InitConentTab(int index)
    {
        ShopTab shoptab = L_ShopsTab[index];
        BgHome.GetComponent<Image>().sprite = L_bgs[index];

        if (shoptab.Is_ActiveTab) return;

        for (int i = 0; i < L_ShopsTab.Count; i++)
        {
            L_ShopsContent[i].gameObject.SetActive(false);
            L_ShopsTab[i].DisableTab();
        }

        ShopContent shopcontent = L_ShopsContent[index];
        shopcontent.gameObject.SetActive(true);

        shoptab.ActiveTab();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventContains.UPDATE_VIEW_HOME_COIN,InitViewCoin);
        btn_AddGold.onClick.RemoveListener(AddGold);
        btn_Setting.onClick.RemoveListener(OpenSetting);
    }
}
