using UnityEngine;

public class Level_169 : BaseLevel
{
    public DragUI barbell1;
    public DragUI barbell2;

    public Transform checkPoint;

    private bool isDone1;
    private bool isDone2;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(!isDone1 && barbell1.transform.localPosition.y > checkPoint.localPosition.y)
        {
            isDone1 = true;
        }

        if (!isDone2 && barbell2.transform.localPosition.y > checkPoint.localPosition.y)
        {
            isDone2 = true;
        }
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

    public void OnclickCheckAnswer()
    {
        if(isDone1 && isDone2)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void SetZObj(Transform tfCheck)
    {
        Vector3 posCheck = tfCheck.localPosition;
        posCheck.z = 0f;
        tfCheck.localPosition = posCheck;
    }
}
