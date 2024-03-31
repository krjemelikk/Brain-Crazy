using UnityEngine;
using UnityEngine.UI;

public class Level_36 : BaseLevel
{
    [Header("Answers")]
    public Button theDog;
    public Button theRock;
    public Button theSpeaker;
    public Button theTennisBall;

    public RectTransform transformSkull;
    public RectTransform transformDog;

    public Sprite Dog_angry;
    public Sprite Dog_happy;
    private bool isEnd;
    protected override void Start()
    {
        base.Start();
        theRock.onClick.AddListener(() => WrongAnswer());
        theSpeaker.onClick.AddListener(() => WrongAnswer());
        theTennisBall.onClick.AddListener(() => WrongAnswer());
        theDog.onClick.AddListener(() => WrongAnswer());

        theDog.GetComponent<Image>().sprite = Dog_happy;
    }

    protected override void Update()
    {
        base.Update();
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
        theDog.GetComponent<Image>().sprite = Dog_angry;

        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnEndDrag()
    {
        CheckAnswer();
    }

    private void CheckAnswer()
    {
        if(isEnd) return;
        Debug.Log(Vector3.Distance(transformSkull.localPosition, transformDog.localPosition));
        if (Vector3.Distance(transformSkull.localPosition, transformDog.localPosition) >= 50)
        {
            isEnd = true;
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