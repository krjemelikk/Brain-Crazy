using UnityEngine;

public class Level_228 : BaseLevel
{
    public GameObject TruckScale;
    public float minTruck = 0.15f;
    public ZoomObject zoomObject;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
            RightAnswer();
        }, () => TruckScale.transform.localScale.x <= minTruck));
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
