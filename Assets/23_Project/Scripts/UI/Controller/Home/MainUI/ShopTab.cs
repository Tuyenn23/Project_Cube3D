using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTab : MonoBehaviour
{
    public Button btn_tab;
    public List<Sprite> ListSpriteTab;
    public Image Bgbutton;

    public UiHome Uihome;
    public bool Is_ActiveTab;


    private void Start()
    {
        btn_tab.onClick.AddListener(OnclickTab);
    }

    public void ActiveTab()
    {
        Bgbutton.sprite = ListSpriteTab[1];

        Is_ActiveTab = true;
    }


    public void DisableTab()
    {
        Bgbutton.sprite = ListSpriteTab[0];

        Is_ActiveTab = false;
    }


    public void OnclickTab()
    {
        Uihome.InitConentTab(transform.GetSiblingIndex());
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);
    }
}
