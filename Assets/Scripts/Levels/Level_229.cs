using UnityEngine;
using UnityEngine.UI;

public class Level_229 : BaseLevel
{
    public Image viewBoss;
    public Sprite spBoss;

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

    int count = 0;
    public void BtnDone()
    {
        if (count >= 3) return;
        count++;
        if(count >= 3)
        {
            viewBoss.sprite = spBoss;
            RightAnswer();
        }
    }
}
