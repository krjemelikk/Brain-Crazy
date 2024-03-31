using UnityEngine;

public class Level_11 : BaseLevel
{
    public RectTransform theRiver;
    public RectTransform theDeer;

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
        if (theRiver.localPosition.x < theDeer.localPosition.x - theDeer.rect.width / 2)
        {
            RightAnswer();
        }
    }
}