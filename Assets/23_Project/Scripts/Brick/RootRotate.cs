using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RootRotate : MonoBehaviour
{
    public Tweener Rotation;
    private void Start()
    {
        float Angles = transform.rotation.eulerAngles.z;
        Rotation = transform.DORotate(new Vector3(0, 0, 360), (360 - Angles) / 20, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}
