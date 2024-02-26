using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabStorage : MonoBehaviour
{
    public static PrefabStorage Instance { get; private set; }

    public Sprite BackgroundCurrent;
    public Sprite BrickCurrent;
    public ParticleSystem FxCurrent;
    public ParticleSystem FxThunder;
    public ParticleSystem FxWind;
    public ParticleSystem FxStarFly;
    public ParticleSystem FxStarZoom;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        InitBackground();
    }

    public void InitBackground()
    {
        BackgroundCurrent = BackgroundController.instance.Bg;
        BrickCurrent = BackgroundController.instance.Brick;
       // FxCurrent = BackgroundController.instance.Effect;
    }

}
