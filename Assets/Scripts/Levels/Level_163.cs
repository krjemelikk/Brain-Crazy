using DG.Tweening;
using UnityEngine;

public class Level_163 : BaseLevel
{
    public Transform obs;
    public Transform boat;

    public GameObject goObj;

    private Vector3 posStart;
    public GameObject buomObj;
    private bool isStart;

    protected override void Start()
    {
        base.Start();
        posStart = boat.transform.localPosition;
        OnclickStart();
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
        GameController.Instance.ResetLevel();
        //StartCoroutine(Helper.StartAction(() =>
        //{
          
        //}, 0.15f));
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnclickStart()
    {
        isStart = true;
        goObj.SetActive(false);
        boat.DOMoveX(obs.position.x, 5f).SetEase(Ease.Linear).OnComplete(() =>
         {
             WrongAnswer();
         });
    }

    public void OnclickDone()
    {
        if (!isStart)
            return;
        boat.DOKill();
        boat.DOLocalMoveX(posStart.x + 150f, 3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            RightAnswer();
        });

        buomObj.transform.localScale = new Vector3(-1, 1, 1);
    }
}
