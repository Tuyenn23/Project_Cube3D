using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AnimBase : MonoBehaviour
{
    public abstract void Open(Transform content, float duration);
    public abstract void Open(Transform content, float duration,float Scale);
    public abstract void Close(Transform content, float duration);
}
