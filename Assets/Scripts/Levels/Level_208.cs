using UnityEngine;
using DG.Tweening;

public class Level_208 : BaseLevel
{
    public GameObject objScale;
    public float maxCanoScale = 0.75f;
    public float minCanoScale = 0.65f;
    public ZoomObject zoomObject;

    public Transform center;

    public DragUI dragScale;

    private bool isDone;
    private bool isEnd;
    protected override void Start()
    {
        base.Start();
        maxCanoScale = 0.67f;
        minCanoScale = 0.6f;
        StartCoroutine(Helper.StartAction(() =>
        {
            isDone = true;
        }, () => objScale.transform.localScale.x <= maxCanoScale && objScale.transform.localScale.x >= minCanoScale));
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone || isEnd) 
            return;
        if(Vector2.Distance(objScale.transform.position,center.position) <= 0.25f)
        {
            dragScale.SetActiveDrag(false);
            zoomObject.transform.localScale = Vector3.one * 0.7f;
            zoomObject.enabled = false;
            isEnd = true;
            objScale.transform.DOMove(center.position, 1f).OnComplete(() => 
            {
                RightAnswer();
            });
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
