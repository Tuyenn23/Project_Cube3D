using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IE_Load());
    }

    IEnumerator IE_Load()
    {
        yield return null;
        GameManager.Instance.loadDataLevel();
        yield return new WaitForEndOfFrame();
        GameManager.Instance.UiGamePlay.MainUi.LoadUiGamePlay();
    }
}
