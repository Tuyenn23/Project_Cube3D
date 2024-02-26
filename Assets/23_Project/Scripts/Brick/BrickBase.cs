using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BrickBase : MonoBehaviour
{
    public bool IsMoved;
    [SerializeField] protected E_TypeBrick e_TypeBrick;
    [SerializeField] protected Material M_MaBrick;
    [SerializeField] private bool isSorted;
    public LayerMask raycastLayer;

    public ParentBrick ParentBrick;

    bool isDragging = false;

    public bool IsAddCompleted = false;

    bool IsFirstElement;

    public Vector3 startPos2;
    public Quaternion RotationBase;

    public Tween Move, Rotate, Rotation, tweern;

    public E_TypeBrick E_TypeBrick { get => e_TypeBrick; }

    protected virtual void Start()
    {
        ParentBrick = transform.parent.GetComponent<ParentBrick>();

        RotationBase = transform.rotation;

        InitData();
        InitPos();

    }

    private void Update()
    {

    }
    public virtual void InitData()
    {
        BRICK brick = GameManager.Instance.DataController.DataBrickSO.GetBrickByType(E_TypeBrick);
        M_MaBrick.mainTexture = brick.texture;
    }

    public void InitPos()
    {
        startPos2 = transform.localPosition;
        float randX = Random.Range(-15, 15);
        float randY = Random.Range(0, 8);
        transform.localPosition = new Vector3(randX, randY, 30);
        tweern = transform.DOLocalMove(startPos2, Random.Range(0.6f, 0.8f));
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.isPause) return;
        if (GameManager.Instance.isLoseLevel) return;
        isSorted = false;
        if (GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.CheckIsFullItem()) return;
        if (IsMoved) return;
        if (GameManager.Instance.LevelManager.RotateBricks.IsRotating) return;

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.SoundClickCube);

        if (GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.L_Save1.Count > 0)
        {
            for (int i = 0; i < GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.L_Save1.Count; i++)
            {
                if (e_TypeBrick == GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.L_Save1[i].e_TypeBrick)
                {
                    IsAddCompleted = false;
                    GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.SortAndMoveElement(this);
                    GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.AddDic(E_TypeBrick, this);
                    isSorted = true;
                    break;
                }
            }
        }
        else
        {
            IsAddCompleted = false;
            GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.AddDic(E_TypeBrick, this);
            MoveToTarget(GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.L_Items[0].transform);
            IsFirstElement = true;
        }
        if (!isSorted && !IsFirstElement)
        {
            IsAddCompleted = false;
            GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.AddDic(E_TypeBrick, this);
            int index = GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.L_Save1.Count - 1;
            MoveToTarget(GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.L_Items[index].transform);
        }
    }

    public void MoveToTarget(Transform target, System.Action complete = null)
    {

        Move = transform.DOMove(target.position, 0.3f).SetEase(Ease.Linear);
        Rotate = transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.Linear);

        Move.OnComplete(() =>
        {
            Rotate?.Kill();
            UpdateRotate();
            complete?.Invoke();
            GameManager.Instance.LevelManager.L_BrickInLevel.Remove(this);
            GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.SortElement();
            IsAddCompleted = true;
            GameManager.Instance.UiGamePlay.MainUi.ListItemPicked.CheckDic(this);
        });

        IsMoved = true;
        transform.SetParent(null);
    }

    public void UpdateRotate()
    {
        transform.localRotation = GameManager.Instance.LevelManager.root.transform.rotation;
        float Angles = transform.localRotation.eulerAngles.z;
        Rotation = transform.DORotate(new Vector3(0, 0, 360), (360 - Angles) / 20, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    public bool IsComplete()
    {
        return Move.IsComplete();
    }

    public void KillTween()
    {
        Move?.Kill();
        tweern?.Kill();
        Rotate?.Kill();
        Rotation?.Kill();
    }


    private void OnDisable()
    {
        KillTween();
    }
}
