using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Level_2 : BaseLevel
{
    [Header("Answers")]
    public Button number200;
    public Button number100;
    public Button number999;
    public Button number50;
    public Button number1;
    public Button number7;

    protected override void Start()
    {
        base.Start();
        number200.onClick.AddListener(() => WrongAnswer());
        number100.onClick.AddListener(() => WrongAnswer());
        number999.onClick.AddListener(() => WrongAnswer());
        number50.onClick.AddListener(() => WrongAnswer());
        number1.onClick.AddListener(() => WrongAnswer());
        number7.onClick.AddListener(() => RightAnswer());
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StartLevel()
    {
        base.StartLevel();
        
        Observable.Interval(TimeSpan.FromSeconds(5f)).Where( isPlaying => GameController.Instance.stateGame == StateGame.PLAYING).Subscribe(_ =>
                               {
                                   GameController.Instance.ShowTutorial(number7.GetComponent<RectTransform>());
                               }).AddTo(this);
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
