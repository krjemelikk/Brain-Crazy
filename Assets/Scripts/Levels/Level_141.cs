using UnityEngine;
using DG.Tweening;

public class Level_141 : BaseLevel
{
    public GameObject FireScale;
    public float maxCanoScale = 1f;
    public ZoomObject zoomObject;

    public Transform center;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
        }, () => FireScale.transform.localScale.x >= maxCanoScale));
    }

    protected override void Update()
    {
        base.Update();
        if (isDone)
            return;
        if(Vector2.Distance(FireScale.transform.position,center.position) <= 0.5f && !zoomObject.isCanZoom)
        {
            FireScale.transform.DOMove(center.position, 1f).OnComplete(() => 
            {
                RightAnswer();
            });
            isDone = true;
        }
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
