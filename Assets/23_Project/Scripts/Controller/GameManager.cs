using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DataController DataController;
    public UiGamePlay UiGamePlay;
    public BrickController LevelManager;

    public int StarInLevel;
    public int CoinInLevel;

    public int LevelPlaying;
    public bool isPause;
    public bool isReceiving;

    public bool isWinLevel;
    public bool isLoseLevel;

    int ToltalLevel;
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Instance.BG_In_Game_Music);
    }

    public void loadDataLevel()
    {
        LevelPlaying = PlayerDataManager.GetCurrentLevel();
        ToltalLevel = PlayerDataManager.GetTotalLevel("Level");
        DOTween.Init().SetCapacity(200, 125);

        CoinInLevel = 0;
        StarInLevel = 0;
        LoadLevel();
    }
    public void InitLevelManager(BrickController brickcontroller)
    {
        LevelManager = brickcontroller;
    }


    public void LoadLevel()
    {
        BrickController Level = Resources.Load<BrickController>("Level/Level_" + LevelPlaying);
        BrickController LevelSpawn = Instantiate(Level);

        InitLevelManager(LevelSpawn);

        LevelSpawn.transform.position = Vector3.zero;

        UiGamePlay.MainUi.itemsAndCombo.ShuffleList(LevelManager.L_BrickInLevel);

        StarInLevel = 0;
        CoinInLevel = 0;

        isWinLevel = false;
    }

    public void IncreaseLevel(int level)
    {
        Destroy(LevelManager.gameObject);

        if (PlayerDataManager.GetCurrentLevel() == ToltalLevel)
        {
            LevelPlaying = 1;
            PlayerDataManager.SetLevel(1);
            LoadLevel();
            UiGamePlay.MainUi.LoadUiGamePlay();

            return;
        }

        int currentLevel = PlayerDataManager.GetCurrentLevel();
        level++;

        LevelPlaying = level;

        if (level > currentLevel)
        {
            PlayerDataManager.SetLevel(level);
        }

        LoadLevel();
        UiGamePlay.MainUi.LoadUiGamePlay();
    }

    public void PauseGame()
    {
        if(SoundManager.Instance.SoundAudio.isPlaying)
        {
            SoundManager.Instance.SoundAudio.Pause();
        }
        isPause = true;
        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        SoundManager.Instance.SoundAudio.Play();
        isPause = false;
        Time.timeScale = 1f;
    }
}

/*public void IncreaseLevel(int level)
{
    if (DataLevel.GetLevel() == totalLevel) return;
    int currentLevel = DataLevel.GetLevel();
    level++;
    levelPlaying = level;
    if (level > currentLevel)
    {
        DataLevel.SetLevel(level);
    }
}*/