using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEnoughMoney : AnimScale
{
    public Button btn_close;
    public Button btn_close1;

    private void OnEnable()
    {
        Open(transform, 0.2f, 0.6f);
    }

    private void Start()
    {
        btn_close.onClick.AddListener(OnClose);
        btn_close1.onClick.AddListener(OnClose);
    }
    public void OnClose()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundButton);

        Close(transform, 0.2f);
    }
}
