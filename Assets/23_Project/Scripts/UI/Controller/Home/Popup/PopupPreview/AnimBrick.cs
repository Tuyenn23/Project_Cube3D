using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBrick : MonoBehaviour
{
    public ParticleSystem CurrentFx;
    Vector3 ModelScale;

    private void OnEnable()
    {
        transform.localScale = new Vector3(90, 90, 90);
        ModelScale = transform.localScale;
        AnimBrickZoom();
    }

    public void InitFx(ParticleSystem FxInit)
    {
        CurrentFx = FxInit;
    }
    public void AnimBrickZoom()
    {
        StopCoroutine(DelayInvoke());
        transform.DOKill();

        transform.localScale = ModelScale;

        transform.DOScale(Vector3.zero, 1.5f)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                ParticleSystem fx = Instantiate(CurrentFx);
                fx.transform.position = transform.position;
                fx.transform.localScale = Vector3.one * 0.25f;
                StartCoroutine(DelayInvoke());
            });
    }

    IEnumerator DelayInvoke()
    {
        yield return new WaitForSeconds(1f);
        AnimBrickZoom();
    }

    private void OnDisable()
    {
        transform.DOKill();
        StopCoroutine(DelayInvoke());
    }
}
