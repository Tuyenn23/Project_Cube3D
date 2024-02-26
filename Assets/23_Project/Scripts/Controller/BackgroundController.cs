using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController instance;

    public Sprite Bg;
    public Sprite Brick;
    public ParticleSystem Effect;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
