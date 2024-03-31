using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_9 : BaseLevel
{
    [Header("Answers")]
    public Button theSun;

    public Image theOwl;
    public Sprite theOwlSleep;
    public Sprite theOwlNotSleep;
    public Image themeDark;
    private RectTransform tranformSun;
    private bool isEnd;
    public DragUI dragOwl;

    protected override void Start()
    {
        base.Start();
        tranformSun = theSun.GetComponent<RectTransform>();
        theOwl.sprite = theOwlSleep;
        themeDark.gameObject.SetActive(false);
        isEnd = false;
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

    public void CheckAnswer()
    {
        if (isEnd)
            return;

        float P0 = GameController.Instance.HomeScene.BoundTop.localPosition.y;
        float P1 = GameController.Instance.HomeScene.BoundBottom.localPosition.y;
        if (tranformSun.localPosition.y - tranformSun.rect.height / 2 < (P0  + 0.75f * (P1 - P0)))
        {
            theOwl.sprite = theOwlNotSleep;
            themeDark.gameObject.SetActive(true);
            isEnd = true;
            dragOwl.SetActiveDrag(false);

            tranformSun.transform.DOKill();
            tranformSun.transform.DOLocalMoveY(P1, 0.5f).OnComplete(() => { RightAnswer(); });
        }
    }
}
