using UnityEngine;
using DG.Tweening;

public class Level_131 : BaseLevel
{
    public GameObject canoScale;
    public float minCanoScale = 0.3f;
    public ZoomObject zoomObject;
    private Vector3 dir;

    private bool isEnd = false;

    public Transform posEnd;

    protected override void Start()
    {
        base.Start();
        isEnd = false;
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
        }, () => canoScale.transform.localScale.x <= minCanoScale));
    }

    protected override void Update()
    {
        base.Update();
        Acceleration();
    }

    private void Acceleration()
    {
        if (zoomObject.isCanZoom || isEnd) return;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;

        if (dir.x >= 0.5f)
        {
            isEnd = true;
            zoomObject.transform.DOMove(posEnd.position, 1f).OnComplete(()=>
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
