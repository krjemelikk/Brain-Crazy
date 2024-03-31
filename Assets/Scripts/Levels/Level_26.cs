using UnityEngine;

public class Level_26 : BaseLevel
{
    public RectTransform posCheck;
    public RectTransform Ques;

    protected override void Start()
    {
        base.Start();
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

    public void CheckAnswer()
    {
        if (posCheck.localPosition.x + Ques.localPosition.x < GameController.Instance.HomeScene.BoundLeft.localPosition.x)
        {
            RightAnswer();
        }
    }
}
