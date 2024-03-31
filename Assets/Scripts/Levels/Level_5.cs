using UnityEngine;
using UnityEngine.UI;

public class Level_5 : BaseLevel
{
    [Header("Answers")]
    public Button dog0;
    public Button dog1;
    public Button dog2;
    public Button dog3;
    public Button dog4;
    public Button dog5;

    protected override void Start()
    {
        base.Start();
        dog1.onClick.AddListener(() => WrongAnswer());
        dog2.onClick.AddListener(() => WrongAnswer());
        dog3.onClick.AddListener(() => WrongAnswer());
        dog4.onClick.AddListener(() => WrongAnswer());
        dog5.onClick.AddListener(() => WrongAnswer());
        dog0.onClick.AddListener(() => RightAnswer());
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
