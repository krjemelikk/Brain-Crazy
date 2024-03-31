using UnityEngine;
using UnityEngine.UI;

public class Level_10 : BaseLevel
{
    [Header("Answers")]
    public Button veryGood;
    public Button veryCool;
    public Button goodGood;
    public Button betterThan;

    protected override void Start()
    {
        base.Start();
        veryGood.onClick.AddListener(() => RightAnswer());
        veryCool.onClick.AddListener(() => RightAnswer());
        goodGood.onClick.AddListener(() => RightAnswer());
        betterThan.onClick.AddListener(() => RightAnswer());
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
}
