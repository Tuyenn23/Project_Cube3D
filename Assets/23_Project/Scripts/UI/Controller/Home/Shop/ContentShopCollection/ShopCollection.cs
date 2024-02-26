using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class ShopCollection : ShopContent
{
    public List<ElementCollect> L_Collection;

    public GameObject TickOwnedPrefab;
    public GameObject TickBackGroundOwned;
    public GameObject TickBrickOwned;
    public GameObject TickEffectOwned;


    [Header("Your Collection")]
    public Image img_YourCollectBackground;
    public Image img_YourCollectBrick;
    public Image img_YourCollectBrick1;
    public Image img_YourCollectEffect;
    public Button btn_preview;



    public Sprite YourCollectBackgroundSave;
    public Sprite YourCollectBrickSave;
    public Sprite YourCollectEffectModelSave;

    public ParticleSystem YourCollectEffectSave;

    public List<BackgroundCollection> L_Background;
    public List<BrickCollection> L_Brick;
    public List<EffectCollection> L_Effect;

    [Header("Content")]
    public RectTransform Content;

    [Header("Popup Preview")]
    public PopupPreview PopupPreview;

    private void OnEnable()
    {
        Content.transform.localPosition = Vector3.zero;
        EventManager.StartListening(EventContains.UPDATE_YOURCOLLECTION, UpdateYourCollection);
        btn_preview.onClick.AddListener(OpenPopupReview);
    }


    private void Start()
    {
    }

    public void InitIDBackground()
    {
        for (int i = 0; i < L_Background.Count; i++)
        {
            L_Background[i].InitID(i + 1);
        }
    }

    public void InitIDBrick()
    {
        for (int i = 0; i < L_Brick.Count; i++)
        {
            L_Brick[i].InitIDBrick(i + 1);
        }
    }

    public void InitIDEffect()
    {
        for (int i = 0; i < L_Effect.Count; i++)
        {
            L_Effect[i].InitID(i + 1);
        }
    }

    public void InitView()
    {
        for (int i = 0; i < L_Collection.Count; i++)
        {
            L_Collection[i].InitView(this);
        }
    }

    public void InitTickBg()
    {
        GameObject obj = Instantiate(TickOwnedPrefab);
        TickBackGroundOwned = obj;
        TickBackGroundOwned.gameObject.SetActive(false);
    }

    public void InitTickBrick()
    {
        GameObject obj = Instantiate(TickOwnedPrefab);
        TickBrickOwned = obj;
        TickBrickOwned.gameObject.SetActive(false);
    }

    public void InitTickEffect()
    {
        GameObject obj = Instantiate(TickOwnedPrefab);
        TickEffectOwned = obj;
        TickEffectOwned.gameObject.SetActive(false);
    }

    public void UpdateYourCollection()
    {
        // update YourCollection
        img_YourCollectBackground.sprite = YourCollectBackgroundSave;
        img_YourCollectBrick.sprite = YourCollectBrickSave;
        img_YourCollectBrick1.sprite = YourCollectBrickSave;
        img_YourCollectEffect.sprite = YourCollectEffectModelSave;


        // update PopupPreview YourCollection
        PopupPreview.CurrentBackground = YourCollectBackgroundSave;
        PopupPreview.CurrentBrick = YourCollectBrickSave;
        PopupPreview.CurrentFx = YourCollectEffectSave;


        // update BackGroundController
       /* UiHome.Instance.backgroundController.Bg = YourCollectBackgroundSave;*/
        BackgroundController.instance.Brick = YourCollectBrickSave;
        BackgroundController.instance.Effect = YourCollectEffectSave;

    }

    private void OpenPopupReview()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        PopupPreview.CurrentBackground = YourCollectBackgroundSave;
        PopupPreview.CurrentBrick = YourCollectBrickSave;
        PopupPreview.CurrentFx = YourCollectEffectSave;

        PopupPreview.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventContains.UPDATE_YOURCOLLECTION, UpdateYourCollection);
        btn_preview.onClick.RemoveListener(OpenPopupReview);
    }
}
