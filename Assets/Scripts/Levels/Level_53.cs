using UnityEngine;

public class Level_53 : BaseLevel
{
    [Header("Answers")]
    public RectTransform theLine;
    public RectTransform theGCheck;
    public RectTransform theGCheck_2;
    private Vector3 posStartLine;

    protected override void Start()
    {
        base.Start();
        posStartLine = theLine.transform.position;
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
        theLine.transform.position = posStartLine;
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

    public void EndDrag(RectTransform tran)
    {
        var distance = Vector3.Distance(tran.transform.position, theGCheck.transform.position);
        Debug.Log(distance);
        var distance2 = Vector3.Distance(tran.transform.position, theGCheck_2.transform.position);
        Debug.Log(distance2);
        if (distance >= 0.65f && distance2 >= 0.65f)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}