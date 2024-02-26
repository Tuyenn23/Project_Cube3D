using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq;

public class ListItemPicked : MonoBehaviour
{
    public List<Image> L_Items;


    [Header("Combo")]
    [ShowInInspector] public Dictionary<E_TypeBrick, List<BrickBase>> Dic_TypeCheck = new Dictionary<E_TypeBrick, List<BrickBase>>();

    [Header("Sort")]
    [SerializeField] public List<BrickBase> L_Save1;

    private Sequence sequence;

    int TweenSequece = 0;

    public void InitListItem(Sprite Brick)
    {
        for (int i = 0; i < L_Items.Count; i++)
        {
            L_Items[i].sprite = Brick;
        }
    }

    public void AddDic(E_TypeBrick Type, BrickBase brick)
    {
        if (Dic_TypeCheck.ContainsKey(Type))
        {
            IncreaseDic(Type, brick);
        }
        else
        {
            List<BrickBase> L_Bricks = new List<BrickBase>();
            L_Bricks.Add(brick);
            Dic_TypeCheck.Add(Type, L_Bricks);
            L_Save1.Add(brick);
        }
    }

    void IncreaseDic(E_TypeBrick Type, BrickBase brick)
    {
        List<BrickBase> L_Bricks = Dic_TypeCheck[Type];

        L_Bricks.Add(brick);
        Dic_TypeCheck[Type] = L_Bricks;
        /*L_Save1.Add(brick);*/
    }

    public void CheckDic(BrickBase brick)
    {
        List<BrickBase> L_Bricks = Dic_TypeCheck[brick.E_TypeBrick];


        if (L_Bricks.Count >= 3)
        {

            TweenSequece = 0;

            if (!L_Bricks[2].IsComplete()) return;


            TweenSequece++;

            if (TweenSequece == 1)
            {
                sequence = DOTween.Sequence();

                Tweener Move1 = L_Bricks[0].transform.DOMoveX(L_Bricks[1].transform.position.x, 0.3f);
                Tweener Move2 = L_Bricks[2].transform.DOMoveX(L_Bricks[1].transform.position.x, 0.3f);

                sequence.Append(Move1);
                sequence.Join(Move2);

                sequence.OnComplete(() =>
                {

                    GameObject fxCurrentEffect = SimplePool.Spawn(PrefabStorage.Instance.FxCurrent.gameObject);
                    fxCurrentEffect.transform.position = L_Bricks[1].transform.position;

                    SoundManager.Instance.PlayFxSound(SoundManager.Instance.SoundMatch);

                    GameObject fxStarfly = SimplePool.Spawn(PrefabStorage.Instance.FxStarFly.gameObject);
                    fxStarfly.transform.position = L_Bricks[1].transform.position;
                    fxStarfly.transform.localScale = Vector3.one;


                    float RandX = 4;
                    float RandY = Random.Range(0, 1f);

                    Vector3 Path1 = new Vector3(RandX, RandY, L_Bricks[1].transform.position.z);
                    Vector3 Path2 = GameManager.Instance.UiGamePlay.MainUi.ImageRate.transform.position;


                    Vector3[] pathes = { Path1, Path2 };

                    fxStarfly.transform.DOScale(Vector3.one * 2, 0.4f);
                    fxStarfly.transform.DOPath(pathes, 0.4f, PathType.CatmullRom, PathMode.Full3D).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        DOTween.Kill(fxStarfly);
                        GameManager.Instance.UiGamePlay.MainUi.itemsAndCombo.AddStarAndUpdateCombo();
                        SoundManager.Instance.PlayFxSound(SoundManager.Instance.soundStarAppear);

                        if (GameManager.Instance.LevelManager.L_BrickInLevel.Count == 0 && !GameManager.Instance.isWinLevel)
                        {
                            GameManager.Instance.UiGamePlay.ProcessWinLose(E_LevelResult.Win);
                        }

                        if (Random.Range(0, 101) < 30 && GameManager.Instance.LevelManager.L_BrickInLevel.Count > 0)
                        {
                            GameManager.Instance.UiGamePlay.PopupReceive.gameObject.SetActive(true);
                        }

                    });



                    for (int i = 2; i >= 0; i--)
                    {
                        L_Bricks[i].KillTween();
                        Destroy(L_Bricks[i].gameObject);
                        L_Save1.Remove(L_Bricks[i]);
                        Dic_TypeCheck[brick.E_TypeBrick].Remove(L_Bricks[i]);
                    }

                    SortElement();

                    if (Dic_TypeCheck[brick.E_TypeBrick].Count <= 0)
                    {
                        Dic_TypeCheck.Remove(brick.E_TypeBrick);
                    }

                    Move1.Kill();
                    Move2.Kill();
                });
            }
        }
        else
        {
            if (CheckIsFullItem() && brick.IsAddCompleted)
            {
                List<E_TypeBrick> L_TypeInGame = new();

                for (int i = 0; i < L_Save1.Count; i++)
                {
                    if (!L_TypeInGame.Contains(L_Save1[i].E_TypeBrick))
                    {
                        L_TypeInGame.Add(L_Save1[i].E_TypeBrick);
                    }
                }


                for (int i = 0; i < L_TypeInGame.Count; i++)
                {
                    int a = GetAmoutBrickInListItem(L_TypeInGame[i]);


                    if (a >= 3)
                    {
                        return;
                    }
                }

                GameManager.Instance.UiGamePlay.ProcessWinLose(E_LevelResult.Lose);
            }
        }
    }

    public int GetPosMoveTo(E_TypeBrick e_TypeBrick)
    {
        if (L_Save1 == null) return -1;

        if (L_Save1.Count == 8) return -1;


        for (int i = L_Save1.Count - 1; i >= 0; i--)
        {
            if (L_Save1[i].E_TypeBrick == e_TypeBrick)
            {
                return i + 1;
            }
        }
        return -1;
    }

    public void SortAndMoveElement(BrickBase brick)
    {
        int k = GetPosMoveTo(brick.E_TypeBrick);
        if (k > L_Items.Count) return;
        brick.MoveToTarget(L_Items[k].transform);

        for (int i = k; i < L_Save1.Count; i++)
        {
            /*if (i < L_Save1.CountRemaining)*/
            L_Save1[i].transform.position = L_Items[i + 1].transform.position;
        }

        BrickBase[] Newlist = new BrickBase[L_Save1.Count + 1];

        int vitrichen = k;
        for (int i = 0; i < vitrichen; i++)
        {
            Newlist[i] = L_Save1[i];
        }
        Newlist[k] = brick;
        for (int j = vitrichen + 1; j < Newlist.Length; j++)
        {
            Newlist[j] = L_Save1[j - 1];
        }
        L_Save1 = new List<BrickBase>(Newlist);


        //L_Save1.Insert(k, brick);
    }

    public void SortElement()
    {
        if (L_Save1.Count > L_Items.Count) return;

        for (int i = 0; i < L_Save1.Count; i++)
        {
            L_Save1[i].transform.position = L_Items[i].transform.position;
        }
    }

    public void SortElement(List<BrickBase> List)
    {
        if (List.Count > L_Items.Count) return;

        for (int i = 0; i < List.Count; i++)
        {
            List[i].transform.position = L_Items[i].transform.position;
        }
    }

    public void ProcessItemMerge(BrickBase brick, E_TypeBrick Type)
    {
        bool isHaveBrickSameType = false;

        if (L_Save1.Count <= 0)
        {
            brick.MoveToTarget(L_Items[0].transform);
            AddDic(Type, brick);
            return;
        }

        for (int i = L_Save1.Count - 1; i >= 0; i--)
        {
            if (L_Save1[i].E_TypeBrick == Type)
            {
                AddDic(Type, brick);
                SortAndMoveElement(brick);
                isHaveBrickSameType = true;
                return;
            }
        }

        if (!isHaveBrickSameType)
        {
            AddDic(Type, brick);
            brick.MoveToTarget(L_Items[L_Save1.Count - 1].transform);
        }
    }

    public int GetAmoutBrickInListItem(E_TypeBrick Type)
    {
        int Count = 0;
        for (int i = 0; i < L_Save1.Count; i++)
        {
            if (Type == L_Save1[i].E_TypeBrick)
            {
                Count++;
            }
        }
        return Count;
    }

    public int GetAmoutRemainingItem()
    {
        return L_Items.Count - L_Save1.Count;
    }

    public bool CheckIsFullItem()
    {
        if (L_Save1.Count >= 8)
        {
            return true;
        }
        return false;
    }


    public void Move2BrickToStartPos()
    {
        int Count = 0;

        for (int i = L_Save1.Count - 1; i >= 0; i--)
        {
            Count++;

            if (Dic_TypeCheck.ContainsKey(L_Save1[i].E_TypeBrick))
            {
                Dic_TypeCheck[L_Save1[i].E_TypeBrick].Remove(L_Save1[i]);


                if (Dic_TypeCheck[L_Save1[i].E_TypeBrick].Count == 0)
                {
                    Dic_TypeCheck.Remove(L_Save1[i].E_TypeBrick);
                }
            }

            L_Save1[i].transform.SetParent(L_Save1[i].ParentBrick.transform);


            Tweener Move = L_Save1[i].transform.DOLocalMove(L_Save1[i].startPos2, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
              {
                  L_Save1[i].Rotation?.Kill();
                  L_Save1[i].IsMoved = false;
                  L_Save1[i].transform.localRotation = L_Save1[i].RotationBase;


                  L_Save1.Remove(L_Save1[i]);
              });



            if (Count == 2)
            {
                return;
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}



