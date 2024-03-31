using UnityEngine.UI;
using DG.Tweening;

public class Level_118 : BaseLevel
{
    public Image imgHint;

    protected override void Start()
    {
        base.Start();
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
        imgHint.fillAmount = 0f;
        imgHint.DOFillAmount(1f, 0.7f).OnComplete(() =>
        {
           // imgHint.fillAmount = 0f;
        });
    }

    public void CheckRightAnswer()
    {
        if (imgHint.fillAmount <= 0)
        {
            imgHint.DOFillAmount(1f, 0.7f).OnComplete(() =>
            {
                RightAnswer();
            });
        }
        else
        {
            RightAnswer();
        }
    }
}
