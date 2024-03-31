using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_23 : BaseLevel
{
    [Header("Answers")]
    public Button theDot;

    public Image successImg;

    protected override void Start()
    {
        base.Start();
        theDot.onClick.AddListener(() =>
        {
            successImg.DOKill();
            successImg.fillAmount = 0;
            successImg.DOFillAmount(1, 0.5f).OnComplete(() => { RightAnswer(); });
        });
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
