using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PopupWin : AnimScale
{
    public PopupClaim popupClaim;

    [Header("BonusStarAndGold")]
    public TMP_Text Star_txt;
    public TMP_Text Gold_txt;


    [Header("BonusBar")]
    public Slider BonusBar;
    public TMP_Text Bonus_txt;
    Tweener TweenBonusBar, TweenBonusText, TweenNextLevelTxt;
    int QuantityBonus = 1;

    public int newStarBonus;
    public int newCoinBonus;

    [Header("Home And Claim")]
    public Button btn_Home;
    public Button btn_Claim;
    public TMP_Text Claim_txt;

    public Button btn_NextLevel;
    public TMP_Text NextLevel_txt;

    bool isClaim;



    private void OnEnable()
    {
        ProcessBonusBar();
        InitButton();
        InitCoinAndStar();
        InitPopupClaim();
    }
    private void Start()
    {
        InitPopupClaim();
    }
    private void FixedUpdate()
    {
        UpdateBonus();
    }
    public void InitButton()
    {
        btn_Home.onClick.AddListener(BackToHome);
        btn_Claim.onClick.AddListener(ClaimBonus);
        btn_NextLevel.onClick.AddListener(NextLevel);
    }

    public void InitCoinAndStar()
    {
        Star_txt.text = GameManager.Instance.StarInLevel.ToString();
        Gold_txt.text = GameManager.Instance.CoinInLevel.ToString();
    }

    public void InitPopupClaim()
    {
        /*popupClaim = GetComponentInChildren<PopupClaim>();*/
        popupClaim.gameObject.SetActive(false);
    }

    public void ProcessBonusBar()
    {
        BonusBar.value = 0f;
        TweenBonusBar = BonusBar.DOValue(1, 5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        TweenBonusText = Bonus_txt.transform.DOScale(Vector3.one * 0.9f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        TweenNextLevelTxt = NextLevel_txt.transform.DOScale(Vector3.one * 0.9f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public void UpdateBonus()
    {
        Bonus_txt.text = $"Bonus X{QuantityBonus}";
        Claim_txt.text = $"Claim X{QuantityBonus}";


        if (BonusBar.value >= 0 && BonusBar.value < 0.25f)
        {
            QuantityBonus = 2;
        }
        else if (BonusBar.value >= 0.25f && BonusBar.value < 0.45f)
        {
            QuantityBonus = 3;
        }
        else if (BonusBar.value >= 0.45f && BonusBar.value < 0.55f)
        {
            QuantityBonus = 5;
        }
        else if (BonusBar.value >= 0.55f && BonusBar.value < 0.75f)
        {
            QuantityBonus = 3;
        }
        else if (BonusBar.value >= 0.75f && BonusBar.value < 1f)
        {
            QuantityBonus = 2;
        }
    }
    private void NextLevel()
    {
        isClaim = false;

        GameManager.Instance.IncreaseLevel(GameManager.Instance.LevelPlaying);
        GameManager.Instance.UiGamePlay.ContentWinlose.gameObject.SetActive(false);
        GameManager.Instance.UiGamePlay.ContentWin.gameObject.SetActive(false);
    }

    private void ClaimBonus()
    {
        if (isClaim) return;

        newStarBonus = GameManager.Instance.StarInLevel * QuantityBonus;
        newCoinBonus = GameManager.Instance.CoinInLevel * QuantityBonus;

       
        int newCoin = PlayerDataManager.GetGold() + newCoinBonus;
        int newStar = PlayerDataManager.GetStar() + newStarBonus;

        PlayerDataManager.SetStar(newStar);
        PlayerDataManager.SetGold(newCoin);

        Star_txt.text = newStarBonus.ToString();
        Gold_txt.text = newCoinBonus.ToString();

        popupClaim.gameObject.SetActive(true);

        isClaim = true;

    }

    private void BackToHome()
    {
        gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(1);
    }

    public void RomveButton()
    {
        btn_Home.onClick.RemoveListener(BackToHome);
        btn_Claim.onClick.RemoveListener(ClaimBonus);
        btn_NextLevel.onClick.RemoveListener(NextLevel);
    }

    private void OnDisable()
    {
        RomveButton();

        TweenBonusBar?.Kill();
        TweenBonusText?.Kill();
        TweenNextLevelTxt?.Kill();
    }
}
