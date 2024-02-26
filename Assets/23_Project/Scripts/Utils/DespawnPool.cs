using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DespawnPool : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(DeplayDespawnFx());
    }

    IEnumerator DeplayDespawnFx()
    {
        yield return new WaitForSeconds(2f);

        SimplePool.Despawn(gameObject);
    }


    private void OnDisable()
    {
        StopCoroutine(DeplayDespawnFx());
    }
}
