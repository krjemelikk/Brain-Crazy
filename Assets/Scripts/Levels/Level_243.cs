using UnityEngine;
using DG.Tweening;

public class Level_243 : BaseLevel
{
    public Transform tfDone;

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
        StartCoroutine(Helper.StartAction(() =>
        {
            GameController.Instance.ResetLevel();
        }, 1f));
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnClickDone()
    {
        tfDone.DORotate(new Vector3(0, 0, -90f), 1f).OnComplete(()=> 
        {
            RightAnswer();
        });
    }
}
