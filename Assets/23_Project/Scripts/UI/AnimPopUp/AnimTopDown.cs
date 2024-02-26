using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class AnimTopDown : AnimBase
{
    [FoldoutGroup("Set Up")] [SerializeField] private float posY;
    [FoldoutGroup("Set Up")] [SerializeField] private AnimationCurve animCurveOpen;
    [FoldoutGroup("Set Up")] [SerializeField] private AnimationCurve animCurveClose;

    private Tweener tweenScaleOpen;
    private Tweener tweenScaleClose;
    public override void Open(Transform content, float duration)
    {
        Vector3 ThePos = content.localPosition;
        ThePos.y = -500;
        content.localPosition = ThePos;
        tweenScaleOpen = content.DOLocalMoveY(posY, duration)
            .SetUpdate(true)
            .SetEase(animCurveOpen);
    }
    public override void Close(Transform content, float duration)
    {
        tweenScaleClose = content.DOLocalMoveY(-1000, duration)
           .SetUpdate(true)
           .SetEase(animCurveClose)
           .OnComplete(() =>
           {
               DestroyImmediate(gameObject);
           });
    }
    private void OnDisable()
    {
        tweenScaleClose?.Kill(true);
        tweenScaleOpen?.Kill(true);
    }

    public override void Open(Transform content, float duration, float Scale)
    {
        throw new System.NotImplementedException();
    }
}
