using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCollection : MonoBehaviour
{
    public ShopCollection shopCollection;
    public ElementCollect ElementCollect;
    public E_TypeCollection TypeCollection;

    public int ID;

    public bool isBuyed;
    public bool isUsing;

    public Image Background;
    public Sprite BackgroundInGame;

    public List<Button> L_btnBackground;

    bool isInit = false;

    private void OnEnable()
    {
        L_btnBackground[0].onClick.AddListener(BuyBackground);
        L_btnBackground[1].onClick.AddListener(UseBackground);
    }

    private void Start()
    {
/*        if(isUsing)
        {
            UseBackground();
        }*/
    }

    public void InitID(int id)
    {
        ID = id;
        int CurrentID = PlayerDataManager.GetBackgroundUsing();

        InitBackgroundBuyed();

        if (ID == CurrentID)
        {
            isBuyed = true;
            isUsing = true;
            shopCollection.img_YourCollectBackground.sprite = Background.sprite;
            UseBackground();
            isInit = true;
        }
    }

    public void InitBackgroundBuyed()
    {
        if (PlayerDataManager.GetListBackGroundBuyed().Count == 0) return;

        int[] BackgroundBuyed = new int[PlayerDataManager.GetListBackGroundBuyed().Count];

        for (int i = 0; i < PlayerDataManager.GetListBackGroundBuyed().Count; i++)
        {

            BackgroundBuyed[i] = PlayerDataManager.GetListBackGroundBuyed()[i];
        }

        if (BackgroundBuyed.Length == 0) return;


        for (int i = 0; i < BackgroundBuyed.Length; i++)
        {
            if (BackgroundBuyed[i] == ID)
            {
                BuyBackground();
            }
        }
    }

    public void BuyBackground()
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

        PlayerDataManager.AddListBackGroundBuyed(ID);
    }

    public void UseBackground()
    {
        if (isInit)
        {
            SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        }

        shopCollection.TickBackGroundOwned.transform.SetParent(transform);
        shopCollection.TickBackGroundOwned.transform.localPosition = Vector3.zero;
        shopCollection.TickBackGroundOwned.transform.localScale = Vector3.one;
        shopCollection.TickBackGroundOwned.gameObject.SetActive(true);
        shopCollection.YourCollectBackgroundSave = Background.sprite;
        BackgroundController.instance.Bg = BackgroundInGame;
        shopCollection.UpdateYourCollection();

        PlayerDataManager.SetBackgroundUsing(ID);
        PlayerDataManager.AddListBackGroundBuyed(ID);

        for (int i = 0; i < shopCollection.L_Background.Count; i++)
        {
            if (shopCollection.L_Background[i].isBuyed)
            {
                shopCollection.L_Background[i].isUsing = false;
                shopCollection.L_Background[i].L_btnBackground[1].gameObject.SetActive(true);
                shopCollection.L_Background[i].L_btnBackground[0].gameObject.SetActive(false);

            }
            else
            {
                shopCollection.L_Background[i].isBuyed = false;
                shopCollection.L_Background[i].isUsing = false;
                shopCollection.L_Background[i].L_btnBackground[0].gameObject.SetActive(true);
                shopCollection.L_Background[i].L_btnBackground[1].gameObject.SetActive(false);
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
