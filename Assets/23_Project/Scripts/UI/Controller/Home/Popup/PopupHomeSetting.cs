using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupHomeSetting : MonoBehaviour
{
    [Header("Music")]
    public Button btn_Music;
    public TMP_Text music_txt;
    public Image MarbleMusic_img;
    [Header("Sound")]
    public Button btn_Sound;
    public TMP_Text sound_txt;
    public Image MarbleSound_img;
    [Header("Close")]
    public Button btn_close;

    public List<Sprite> L_SpriteToggleButton;




    private void OnEnable()
    {
        btn_close.onClick.AddListener(ClosePopupHome);
        btn_Music.onClick.AddListener(SettingMusic);
        btn_Sound.onClick.AddListener(SettingSound);

        InitMusic();
        InitSound();
    }

    public void InitSound()
    {
        bool isOn = PlayerDataManager.GetSound();

        btn_Sound.image.sprite = isOn ? L_SpriteToggleButton[0] : L_SpriteToggleButton[1];
        sound_txt.text = isOn ? "On" : "Off";

        MarbleSound_img.rectTransform.anchoredPosition = isOn ? MarbleSound_img.rectTransform.anchoredPosition = new Vector2(0, 0) : MarbleSound_img.rectTransform.anchoredPosition = new Vector2(-190, 0);
        SoundManager.Instance.SettingFxSound(isOn);
    }

    public void InitMusic()
    {
        bool isOn = PlayerDataManager.GetMusic();

        btn_Music.image.sprite = isOn ? L_SpriteToggleButton[0] : L_SpriteToggleButton[1];
        music_txt.text = isOn ? "On" : "Off";

        MarbleMusic_img.rectTransform.anchoredPosition = isOn ? MarbleMusic_img.rectTransform.anchoredPosition = new Vector2(0, 0) : MarbleMusic_img.rectTransform.anchoredPosition = new Vector2(-190, 0);
        SoundManager.Instance.SettingMusic(isOn);
    }

    private void SettingSound()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        bool isOn = PlayerDataManager.GetSound();

        btn_Sound.image.sprite = isOn ? L_SpriteToggleButton[1] : L_SpriteToggleButton[0];
        sound_txt.text = isOn ? "Off" : "On";

        MarbleSound_img.rectTransform.anchoredPosition = isOn ? MarbleSound_img.rectTransform.anchoredPosition = new Vector2(-190, 0) : MarbleSound_img.rectTransform.anchoredPosition = new Vector2(0, 0);
        PlayerDataManager.SetSound(!isOn);
        SoundManager.Instance.SettingFxSound(!isOn);
    }

    private void SettingMusic()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        bool isOn = PlayerDataManager.GetMusic();

        btn_Music.image.sprite = isOn ? L_SpriteToggleButton[1] : L_SpriteToggleButton[0];
        music_txt.text = isOn ? "Off" : "On";

        MarbleMusic_img.rectTransform.anchoredPosition = isOn ? MarbleMusic_img.rectTransform.anchoredPosition = new Vector2(-190,0) : MarbleMusic_img.rectTransform.anchoredPosition = new Vector2(0,0);

        PlayerDataManager.SetMusic(!isOn);
        SoundManager.Instance.SettingMusic(!isOn);
    }

    private void ClosePopupHome()
    {
        UiHome.Instance.Levelfx.gameObject.SetActive(true);
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        transform.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        btn_close.onClick.RemoveListener(ClosePopupHome);
        btn_Music.onClick.RemoveListener(SettingMusic);
        btn_Sound.onClick.RemoveListener(SettingSound);
    }
}
