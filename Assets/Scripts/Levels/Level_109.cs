using System.Collections.Generic;
using UnityEngine.UI;

public class Level_109 : BaseLevel
{
    public List<Image> lsBalls = new List<Image>();
    public Image mask;

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

    public void LevelDone()
    {
        foreach(Image ball in lsBalls)
        {
            ball.raycastTarget = false;
        }
    }
}
