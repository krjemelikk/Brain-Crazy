using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_140 : BaseLevel
{
    public GameObject sunScale;
    public float maxCanoScale = 1f;
    public ZoomObject zoomObject;

    public Transform rain;
    public Image flower1;
    public Image flower2;

    public Sprite spFlower1;
    public Sprite spFlower2;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
        }, () => sunScale.transform.localScale.x >= maxCanoScale));
    }

    protected override void Update()
    {
        base.Update();
        if (isDone)
            return;
        if(Vector2.Distance(sunScale.transform.position,rain.position) <= 0.5f && !zoomObject.isCanZoom)
        {
            sunScale.transform.DOMove(rain.position, 1f).OnComplete(() => 
            {
                flower1.sprite = spFlower1;
                flower2.sprite = spFlower2;
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
