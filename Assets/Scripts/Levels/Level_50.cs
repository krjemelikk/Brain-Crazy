using UnityEngine;

public class Level_50 : BaseLevel
{
    public RectTransform theTextYou;

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

    private void CheckAnswer()
    {
        RightAnswer();
    }

    public void EndDrag(RectTransform tran)
    {
        var distance = Vector2.Distance(tran.transform.position,theTextYou.transform.position);
        if (distance <= 0.35f)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}
