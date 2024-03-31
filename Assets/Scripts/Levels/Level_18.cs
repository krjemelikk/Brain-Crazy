using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Level_18 : BaseLevel
{
    [Header("Answers")]
    public Button theDeer;
    public Button theSun;
    public Button thePyramid;
    public Button theUFO;
    public Button theAnt;

    public GameObject panelEnd;

    protected override void Start()
    {
        base.Start();
        theDeer.onClick.AddListener(() => WrongAnswer());
        theSun.onClick.AddListener(() => WrongAnswer());
        thePyramid.onClick.AddListener(() => WrongAnswer());
        theUFO.onClick.AddListener(() => WrongAnswer());
        theAnt.onClick.AddListener(() => RightAnswer());
        
        panelEnd.SetActive(false);
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
        panelEnd.SetActive(true);

        Observable.Timer(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => base.RightAnswer())
            .AddTo(this);
    }

    public override void UseHint()
    {
        base.UseHint();
    }
}
