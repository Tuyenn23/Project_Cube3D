using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupPause : AnimScale
{
    AnimationUIController animController;

    [Header("Music")]
    public Button btn_Music;
    public TMP_Text music_txt;
    public Image SwitchMusic_img;
    [Header("Sound")]
    public Button btn_Sound;
    public TMP_Text sound_txt;
    public Image SwitchSound_img;
    [Header("Close")]
    public Button btn_Resume;

    public Button btn_Home;

    public List<Sprite> L_SpriteToggleButton;



    private void OnEnable()
    {
        btn_Resume.onClick.AddListener(ResumeGame);
        btn_Music.onClick.AddListener(SettingMusic);
        btn_Sound.onClick.AddListener(SettingSound);
        btn_Home.onClick.AddListener(BackToHome);

        InitSettingSound();
        InitSettingMusic();
    }

    private void Start()
    {
        animController = GetComponent<AnimationUIController>();
    }

    public void InitSettingSound()
    {
        bool isOn = PlayerDataManager.GetSound();

        btn_Sound.image.sprite = isOn ? L_SpriteToggleButton[0] : L_SpriteToggleButton[1];
        sound_txt.text = isOn ? "On" : "Off";

        SwitchSound_img.rectTransform.anchoredPosition = isOn ? SwitchSound_img.rectTransform.anchoredPosition = new Vector2(0, 0) : SwitchSound_img.rectTransform.anchoredPosition = new Vector2(-140, 0);
        SoundManager.Instance.SettingFxSound(isOn);
    }

    public void InitSettingMusic()
    {
        bool isOn = PlayerDataManager.GetMusic();
        btn_Music.image.sprite = isOn ? L_SpriteToggleButton[0] : L_SpriteToggleButton[1];
        music_txt.text = isOn ? "On" : "Off";
        SwitchMusic_img.rectTransform.anchoredPosition = isOn ? SwitchMusic_img.rectTransform.anchoredPosition = new Vector2(0, 0) : SwitchMusic_img.rectTransform.anchoredPosition = new Vector2(-140, 0);
        SoundManager.Instance.SettingMusic(isOn);
    }

    private void SettingSound()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        bool isOn = PlayerDataManager.GetSound();

        btn_Sound.image.sprite = isOn ? L_SpriteToggleButton[1] : L_SpriteToggleButton[0];
        sound_txt.text = isOn ? "Off" : "On";

        SwitchSound_img.rectTransform.anchoredPosition = isOn ? SwitchSound_img.rectTransform.anchoredPosition = new Vector2(-140, 0) : SwitchSound_img.rectTransform.anchoredPosition = new Vector2(0, 0);
        PlayerDataManager.SetSound(!isOn);
        SoundManager.Instance.SettingFxSound(!isOn);
    }

    private void SettingMusic()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        bool isOn = PlayerDataManager.GetMusic();
        btn_Music.image.sprite = isOn ? L_SpriteToggleButton[1] : L_SpriteToggleButton[0];
        music_txt.text = isOn ? "Off" : "On";
        SwitchMusic_img.rectTransform.anchoredPosition = isOn ? SwitchMusic_img.rectTransform.anchoredPosition = new Vector2(-140, 0) : SwitchMusic_img.rectTransform.anchoredPosition = new Vector2(0, 0);
        PlayerDataManager.SetMusic(!isOn);
        SoundManager.Instance.SettingMusic(!isOn);

    }

    private void ResumeGame()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        GameManager.Instance.ResumeGame();
        animController.ClosePopUp();
    }

    private void BackToHome()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
        GameManager.Instance.ResumeGame();
        SceneManager.LoadScene(1);
    }


    private void OnDisable()
    {
        btn_Resume.onClick.RemoveListener(ResumeGame);
        btn_Music.onClick.RemoveListener(SettingMusic);
        btn_Sound.onClick.RemoveListener(SettingSound);
        btn_Home.onClick.RemoveListener(BackToHome);
    }
}
