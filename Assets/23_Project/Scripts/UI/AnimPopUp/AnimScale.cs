using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class AnimScale : AnimBase
{
    [FoldoutGroup("Set Up")] [SerializeField] private AnimationCurve animCurveOpen;
    [FoldoutGroup("Set Up")] [SerializeField] private AnimationCurve animCurveClose;
    private Tweener tweenScaleOpen;
    private Tweener tweenScaleClose;
    public override void Open(Transform content, float duration)
    {
        content.localScale = Vector3.one * 0.4f;
        tweenScaleOpen = content.DOScale(Vector3.one, duration)
            .SetUpdate(true)
            .SetEase(animCurveOpen);
    }

    public override void Open(Transform content, float duration,float scale)
    {
        content.localScale = Vector3.one * 0.4f;
        tweenScaleOpen = content.DOScale(Vector3.one * scale, duration)
            .SetUpdate(true)
            .SetEase(animCurveOpen);
    }

    public override void Close(Transform content, float duration)
    {
        Vector3 theScale = Vector3.one;
        tweenScaleClose = content.DOScale(theScale * 0.4f, duration)
           .SetUpdate(true)
           .SetEase(animCurveClose)
           .OnComplete(() =>
           {
               gameObject.SetActive(false);
           });
    }
    private void OnDisable()
    {
        tweenScaleClose?.Kill(true);
        tweenScaleOpen?.Kill(true);
    }
}
