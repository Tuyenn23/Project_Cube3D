using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Brick",fileName ="Brick")]
public class DataBrickSO : ScriptableObject
{
    public List<BRICK> L_Bricks;

   public BRICK GetBrickByType(E_TypeBrick TypeBrick)
    {
        for (int i = 0; i < L_Bricks.Count; i++)
        {
            if(TypeBrick == L_Bricks[i].E_TypeBrick)
            {
                return L_Bricks[i];
            }
        }

        return null;
    }
}

[Serializable]
public class BRICK
{
    public E_TypeBrick E_TypeBrick;
    public Texture texture;
}
