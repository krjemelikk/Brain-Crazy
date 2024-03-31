using UnityEngine;

public class Level_166 : BaseLevel
{
    public GameObject FireScale;
    public float maxCanoScale = 1f;
    public ZoomObject zoomObject;

    public GameObject objDone;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
            objDone.SetActive(true);
            RightAnswer();
        }, () => FireScale.transform.localScale.x >= maxCanoScale));
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
