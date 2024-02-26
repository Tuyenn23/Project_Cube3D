using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TigerForge;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ItemsAndCombo : MonoBehaviour
{
    [Header("Combo")]
    public TMP_Text ComboText;

    public Slider FillCombo;
    public float timeCombo;
    public int countCombo;

    public int QuantityCollection;


    [Header("Item Merge")]
    public Button btn_merge;
    public TMP_Text AmoutMerge_txt;

    [Header("Item PinWheel")]
    public Button btn_PinWheel;
    public TMP_Text AmoutPinWheel_txt;

    [Header("Item Thunder")]
    public Button btn_Thunder;
    public TMP_Text AmoutThunder_txt;

    [Header("Item Time")]
    public Button btn_Time;
    public TMP_Text AmoutTime_txt;

    public List<E_TypeBrick> L_TypeBricks;

    public int CountRemaining = 0;

    Tweener Anim;



    private void OnEnable()
    {
        InitAnimTxt();
        EventManager.StartListening(EventContains.UPDATE_ITEMS, initItem);
        InitButton();
    }

    private void Start()
    {
        initItem();
        ComboText.text = $"Combo X{countCombo}";
        FillCombo.maxValue = timeCombo;
    }
    private void Update()
    {
        Combo();
    }

    public void InitButton()
    {
        btn_merge.onClick.AddListener(OnProcessItemMerge);
        btn_Thunder.onClick.AddListener(OnProcessItemThunder);
        btn_PinWheel.onClick.AddListener(OnProcessItemPinwheel);
        btn_Time.onClick.AddListener(OnProcessItemTimer);
    }

    private void initItem()
    {
        AmoutMerge_txt.text = PlayerDataManager.GetItemMegnat().ToString();
        AmoutPinWheel_txt.text = PlayerDataManager.GetItemPinWheel().ToString();
        AmoutThunder_txt.text = PlayerDataManager.GetItemEnergy().ToString();
        AmoutTime_txt.text = PlayerDataManager.GetItemTimer().ToString();
    }

    public void InitAnimTxt()
    {
        Anim = ComboText.transform.DOScale(Vector3.one * 0.9f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    public void Fill()
    {
        FillCombo.value = timeCombo;
    }

    public void Combo()
    {
        if (timeCombo > 0 && FillCombo.IsActive())
        {
            timeCombo -= Time.deltaTime;
            Fill();
        }
        else
        {
            countCombo = 0;
            FillCombo.gameObject.SetActive(false);
        }
    }

    public void UpdateCombo()
    {
        countCombo++;
        ComboText.text = $"Combo X{countCombo}";
        timeCombo = 5f;
        FillCombo.gameObject.SetActive(true);
    }

    public void AddStarAndUpdateCombo()
    {
        QuantityCollection++;

        if (QuantityCollection == 1)
        {
            GameManager.Instance.StarInLevel++;
            int Count = PlayerDataManager.GetStar();
            Count += GameManager.Instance.StarInLevel;


            PlayerDataManager.SetStar(Count);
            EventManager.EmitEvent(EventContains.UPDATE_VIEW_RATE);
        }

        if (QuantityCollection > 1)
        {
            UpdateCombo();
            GameManager.Instance.StarInLevel += countCombo - 1;
            int Count = PlayerDataManager.GetStar();
            Count += countCombo - 1;

            PlayerDataManager.SetStar(Count);
            EventManager.EmitEvent(EventContains.UPDATE_VIEW_RATE);
        }
    }
    private void OnProcessItemMerge()
    {
        int AmoutItem = PlayerDataManager.GetItemMegnat();
        if (AmoutItem <= 0) return;

        AmoutItem--;
        PlayerDataManager.SetItemMegnat(AmoutItem);
        EventManager.EmitEvent(EventContains.UPDATE_ITEMS);
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundHintMagnet);

        InitItemOnLevelMerge();
    }

    private void OnProcessItemThunder()
    {
        if (GameManager.Instance.LevelManager.L_BrickInLevel.Count == 0) return;

        int AmoutItem = PlayerDataManager.GetItemEnergy();

        if (AmoutItem <= 0) return;
        AmoutItem--;
        PlayerDataManager.SetItemEnergy(AmoutItem);
        EventManager.EmitEvent(EventContains.UPDATE_ITEMS);
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundHintThunder);

        InitItemThunder();

        if (L_TypeBricks.Count == 0) return;

        E_TypeBrick RandomType = L_TypeBricks[new Random().Next(L_TypeBricks.Count)];

        List<BrickBase> L_Clone = new();

        for (int i = 0; i < GameManager.Instance.LevelManager.L_BrickInLevel.Count; i++)
        {
            if (GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick == RandomType)
            {
                L_Clone.Add(GameManager.Instance.LevelManager.L_BrickInLevel[i]);
            }

            if (L_Clone.Count == 3)
            {
                for (int j = 0; j < L_Clone.Count; j++)
                {
                    L_Clone[j].KillTween();
                    GameManager.Instance.LevelManager.L_BrickInLevel.Remove(L_Clone[j]);
                    GameObject fx = SimplePool.Spawn(PrefabStorage.Instance.FxThunder.gameObject);
                    fx.transform.position = L_Clone[j].transform.position;
                    Destroy(L_Clone[j].gameObject);
                }

                if (GameManager.Instance.LevelManager.L_BrickInLevel.Count == 0)
                {
                    GameManager.Instance.UiGamePlay.ProcessWinLose(E_LevelResult.Win);
                }
                AddStarAndUpdateCombo();
                L_TypeBricks.Clear();
                L_Clone.Clear();
                break;
            }
        }
    }

    private void OnProcessItemPinwheel()
    {
        int AmoutItem = PlayerDataManager.GetItemPinWheel();
        if (AmoutItem <= 0) return;

        AmoutItem--;
        PlayerDataManager.SetItemPinWheel(AmoutItem);
        EventManager.EmitEvent(EventContains.UPDATE_ITEMS);

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundHintRandom);


        GameObject fx = SimplePool.Spawn(PrefabStorage.Instance.FxWind.gameObject);
        fx.transform.position = new Vector3(0, 6, 0);
        fx.transform.rotation = Quaternion.Euler(90, 0, 0);

        ShuffleList(GameManager.Instance.LevelManager.L_BrickInLevel);
    }

    public void InitItemOnLevelMerge()
    {
        int AmoutRemainingItem = GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.GetAmoutRemainingItem();

        if (AmoutRemainingItem == 2)
        {
            for (int i = 0; i < GameManager.Instance.LevelManager.L_BrickInLevel.Count; i++)
            {
                var AmoutInlistItem = GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.GetAmoutBrickInListItem(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick);

                if (!L_TypeBricks.Contains(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick) && AmoutInlistItem <= 2 && AmoutInlistItem > 0)
                {
                    L_TypeBricks.Add(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick);
                }
            }
        }
        else if (AmoutRemainingItem == 1)
        {
            for (int i = 0; i < GameManager.Instance.LevelManager.L_BrickInLevel.Count; i++)
            {
                if (!L_TypeBricks.Contains(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick) &&
                    GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.GetAmoutBrickInListItem(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick) == 2)
                {
                    L_TypeBricks.Add(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick);
                }
            }
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.LevelManager.L_BrickInLevel.Count; i++)
            {
                if (!L_TypeBricks.Contains(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick))
                {
                    L_TypeBricks.Add(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick);
                }
            }
        }
        OnProcessMerge();
    }

    public void OnProcessMerge()
    {
        if (L_TypeBricks.Count == 0) return;

        E_TypeBrick RandomType = L_TypeBricks[new Random().Next(L_TypeBricks.Count)];

        CountRemaining += GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.GetAmoutBrickInListItem(RandomType);


        for (int i = 0; i < GameManager.Instance.LevelManager.L_BrickInLevel.Count; i++)
        {
            if (GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick == RandomType)
            {
                CountRemaining++;
                GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.ProcessItemMerge(GameManager.Instance.LevelManager.L_BrickInLevel[i], RandomType);
            }

            if (CountRemaining == 3)
            {
                CountRemaining = 0;
                L_TypeBricks.Clear();
                break;
            }
        }
    }


    public void InitItemThunder()
    {
        for (int i = 0; i < GameManager.Instance.LevelManager.L_BrickInLevel.Count; i++)
        {
            if (!L_TypeBricks.Contains(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick))
            {
                var AmoutInlistItem = CountBrickInLevel(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick);

                if (AmoutInlistItem >= 3)
                {
                    L_TypeBricks.Add(GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick);
                }
            }
        }
    }

    public int CountBrickInLevel(E_TypeBrick Type)
    {
        int Count = 0;

        for (int i = 0; i < GameManager.Instance.LevelManager.L_BrickInLevel.Count; i++)
        {
            if (GameManager.Instance.LevelManager.L_BrickInLevel[i].E_TypeBrick == Type)
            {
                Count++;
            }
        }

        return Count;
    }




    public void ShuffleList(List<BrickBase> list)
    {
        Random random = new Random();
        int n = list.Count;


        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Vector3 value = list[k].transform.position;
            list[k].transform.position = list[n].transform.position;
            list[n].transform.position = value;
        }
    }

    private void OnProcessItemTimer()
    {
        int AmoutItem = PlayerDataManager.GetItemTimer();
        if (AmoutItem <= 0) return;
        AmoutItem--;
        PlayerDataManager.SetItemTimer(AmoutItem);
        EventManager.EmitEvent(EventContains.UPDATE_ITEMS);

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundhintClock);



        GameManager.Instance.UiGamePlay.MainUi.MaxTime += 31;
        GameManager.Instance.UiGamePlay.MainUi.TimeCountDown1 = 1;
        GameManager.Instance.UiGamePlay.MainUi.AnimTimer();
    }


    private void OnDisable()
    {
        EventManager.StopListening(EventContains.UPDATE_ITEMS, initItem);
        btn_merge.onClick.RemoveListener(OnProcessItemMerge);
        btn_Thunder.onClick.RemoveListener(OnProcessItemThunder);
        Anim?.Kill();
    }
}
