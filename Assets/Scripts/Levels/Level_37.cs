using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_37 : BaseLevel
{
    [Header("Answers")]
    public Button theLock;
    public Button theSaw;
    public Button theHammer;
    public Button theScissors;
    public Button theBox;
    public RectTransform TransformKey;
    public RectTransform TransformLock;
    public RectTransform EndKeyMove;
    
    private Vector3 dir;
    private bool isEnd = false;
    private float maxX, minX, minY, maxY;
    protected override void Start()
    {
        base.Start();
        theLock.onClick.AddListener(() => WrongAnswer());
        theSaw.onClick.AddListener(() => WrongAnswer());
        theHammer.onClick.AddListener(() => WrongAnswer());
        theScissors.onClick.AddListener(() => WrongAnswer());
        theBox.onClick.AddListener(() => WrongAnswer());

        maxX = TransformLock.transform.localPosition.x + TransformLock.rect.width / 2;
        minX = TransformLock.transform.localPosition.x - TransformLock.rect.width / 2;
        minY = TransformLock.transform.localPosition.y - TransformLock.rect.height / 2;
        maxY = TransformLock.transform.localPosition.y + TransformLock.rect.height / 2;
    }

    protected override void Update()
    {
        base.Update();
        
        Acceleration();
    }
    
    public override void StartLevel()
    {
        base.StartLevel();
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();
    }

    public override void WrongAnswer()
    {
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }
    
    private void Acceleration()
    {
        if(isEnd) return;
        
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;
        
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        if (dir.y >= 0.9f)
        {
            isEnd = true;
            TransformKey.DOLocalMove(EndKeyMove.localPosition, 0.5f).SetEase(Ease.Linear);
        }
    }
    
    public void EndDrag(RectTransform tran)
    {
        if (tran.transform.localPosition.x < maxX
            && tran.transform.localPosition.x > minX
            && tran.transform.localPosition.y > minY
            && tran.transform.localPosition.y < maxY)
        {
            RightAnswer();
        }
    }

    public void EndDragWrong(RectTransform tran)
    {
        if (tran.transform.localPosition != localPositionWrong)
        {
            WrongAnswer();
            tran.transform.localPosition = localPositionWrong;
        }
    }

    Vector3 localPositionWrong;

    public void BeginDragWrong(RectTransform tran)
    {
        localPositionWrong = tran.transform.localPosition;
    }
}