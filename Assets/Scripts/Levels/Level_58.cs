using DG.Tweening;
using UnityEngine;
using Spine.Unity;

public class Level_58 : BaseLevel
{
    public DOTweenAnimation[] arrCows;
    public GameObject panelWaiting;

    public SkeletonGraphic arrCows1Anim;
    public SkeletonGraphic arrCows2Anim;
    public SkeletonGraphic arrCows3Anim;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            CheckAnswer();
        }, () => txtQuestion.transform.localPosition.y <= 100));
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
        txtQuestion.transform.DOLocalMoveY(-124, 1f).OnComplete(() =>
        {
            for (int i = 0; i < arrCows.Length; i++)
            {
                arrCows[i].DOPlay();
            }

            arrCows1Anim.AnimationState.SetAnimation(0, "animation", true);
            arrCows2Anim.AnimationState.SetAnimation(0, "animation", true);
            arrCows3Anim.AnimationState.SetAnimation(0, "animation", true);
        }
    );
    }

    public void CompleteMoveCows()
    {
        RightAnswer();
    }
}