using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : BaseLevel
{
    [Header("Answers")]
    public Button theBelt;
    public Button theSnake;
    public Button theContainer;
    public Button theBridge;

    protected override void Start()
    {
        base.Start();
        theBelt.onClick.AddListener(() => WrongAnswer());
        theSnake.onClick.AddListener(() => WrongAnswer());
        theContainer.onClick.AddListener(() => WrongAnswer());
        theBridge.onClick.AddListener(() => RightAnswer());
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
            GameController.Instance.ShowTutorial(theBridge.GetComponent<RectTransform>());
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

    private Vector3 shakeDir;
    public void CheckShakeTrigger()
    {
        shakeDir = Input.acceleration;

        if (shakeDir.sqrMagnitude >= 10f)
        {
            Debug.Log(StringHelper.StringColor("Shakeeeeeeeeee", ColorString.red));
        }
    }
}
