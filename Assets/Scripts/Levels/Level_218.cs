using UnityEngine;
using DG.Tweening;

public class Level_218 : BaseLevel
{
    [SerializeField] private GameObject mocObj;
    [SerializeField] private Transform limitLeft;
    [SerializeField] private Transform limitRight;

    [SerializeField] private RectTransform mocBodyObj;
    [SerializeField] private Transform mocHeadBodyObj;

    [SerializeField] private Rigidbody2D gauRigi;
    [SerializeField] private bool isHasGau;
    private Transform parentGauStart;
    [SerializeField] private Transform posEnd;
    [SerializeField] private Transform posEndCoin;

    private bool isPull;

    private bool isEnterRightBtn;
    private bool isEnterLeftBtn;
    private int currentNumCoin;
    private bool isEnd;
    private bool isCanPull;

    protected override void Start()
    {
        base.Start();
        gauRigi.gravityScale = 0;
        parentGauStart = gauRigi.transform.parent;
    }

    private void ControllMocRight()
    {
        float moveX = mocObj.transform.position.x + 0.01f;
        moveX = Mathf.Clamp(moveX, limitLeft.position.x, limitRight.position.x);

        mocObj.transform.position = new Vector3(moveX, mocObj.transform.position.y, mocObj.transform.position.z);
    }
    private void ControllMocLeft()
    {
        float moveX = mocObj.transform.position.x - 0.01f;
        moveX = Mathf.Clamp(moveX, limitLeft.position.x, limitRight.position.x);
        Debug.Log("moveX " + moveX);

        mocObj.transform.position = new Vector3(moveX, mocObj.transform.position.y, mocObj.transform.position.z);
    }

    public void PullMoc()
    {
        if (!isCanPull)
            return;
        if (isPull)
            return;

        if(isHasGau)
        {
            gauRigi.transform.parent = parentGauStart;
            gauRigi.gravityScale = 1;
            isHasGau = false;
            return;
        }

        isPull = true;
        mocBodyObj.DOSizeDelta(new Vector2(mocBodyObj.sizeDelta.x, 145), 0.3f).OnComplete(() =>
        {
            if (Vector2.Distance(mocHeadBodyObj.position, gauRigi.transform.position) <= 0.2f)
            {
                gauRigi.gravityScale = 0;
                gauRigi.transform.parent = mocHeadBodyObj.transform;
                isHasGau = true;
            }
           
            mocBodyObj.DOSizeDelta(new Vector2(mocBodyObj.sizeDelta.x, 10), 0.3f).OnComplete(() => { isPull = false; });
        });
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if(isEnterRightBtn)
        {
            ControllMocRight();
        }
        if (isEnterLeftBtn)
        {
            ControllMocLeft();
        }

        if(Vector2.Distance(gauRigi.transform.position, posEnd.position) <= 0.2f)
        {
            RightAnswer();
            isEnd = true;
        }
    }

    public void OnEnterLeft(bool isEnter)
    {
        isEnterLeftBtn = isEnter;
    }

    public void OnEnterRight(bool isEnter)
    {
        isEnterRightBtn = isEnter;
    }

    public void OnDragCoin(Transform coinCurrent)
    { 
        if(Vector2.Distance(coinCurrent.position, posEndCoin.position) <= 0.2f)
        {
            currentNumCoin += 1;
            coinCurrent.gameObject.SetActive(false);
            if(currentNumCoin >= 5)
            {
                isCanPull = true;
            }
        }
    }
}
