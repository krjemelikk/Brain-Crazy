using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class Level_35 : BaseLevel
{
    public GameObject treeScale;
    public float maxScaleTree = 0.9f;
    public DOTweenAnimation[] arrCows;
    public GameObject panelWaiting;

    public SkeletonGraphic arrCows1Anim;
    public SkeletonGraphic arrCows2Anim;

    public ZoomObject zoomObject;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
            CheckAnswer();
        }, () => treeScale.transform.localScale.x >= maxScaleTree && treeScale.transform.localScale.y >= maxScaleTree));
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

    private void CheckAnswer()
    {
        panelWaiting.SetActive(true);
        for (int i = 0; i < arrCows.Length; i++)
        {
            arrCows[i].DOPlay();
        }

        arrCows1Anim.AnimationState.SetAnimation(0, "animation", true);
        arrCows2Anim.AnimationState.SetAnimation(0, "animation", true);
    }

    public void CompleteMoveCows()
    {
        RightAnswer();
    }
}