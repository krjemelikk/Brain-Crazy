using UnityEngine;
using UnityEngine.UI;

public class Level_54 : BaseLevel
{
    [Header("Answers")]
    public Button theCat;
    public Button[] theWrong;

    protected override void Start()
    {
        base.Start();
        theCat.onClick.AddListener(() => RightAnswer());
        for (int i = 0; i < theWrong.Length; i++)
        {
            theWrong[i].onClick.AddListener(() => WrongAnswer());
        }
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void EndDragWrong(RectTransform tran)
    {
        if (tran.transform.localPosition != localPositionWrong)
        {
            WrongAnswer();
            //tran.transform.localPosition = localPositionWrong;
        }
    }

    Vector3 localPositionWrong;

    public void BeginDragWrong(RectTransform tran)
    {
        localPositionWrong = tran.transform.localPosition;
    }
}