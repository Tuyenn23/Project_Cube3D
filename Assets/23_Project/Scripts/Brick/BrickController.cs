using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrickController : MonoBehaviour
{
    public RotateBricks RotateBricks;

    public RootRotate root;

    public int TimeInLevel;


    public List<BrickBase> L_BrickInLevel;


    private void Awake()
    {
        BrickBase[] brick = GetComponentsInChildren<BrickBase>();

        L_BrickInLevel = new List<BrickBase>(brick);

    }
}
