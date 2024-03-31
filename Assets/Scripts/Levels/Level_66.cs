using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_66 : BaseLevel
{
    [Header("Answers")]
    public Button theBee;
    public Image imgCircle;

    protected override void Start()
    {
        base.Start();
        theBee.onClick.AddListener(() => RightAnswer());
        //imgCircle.gameObject.SetActive(false);
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
        //imgCircle.gameObject.SetActive(true);
        imgCircle.DOKill();
        imgCircle.fillAmount = 0;
        imgCircle.DOFillAmount(1, 0.5f).OnComplete
        (() => 
        {
               base.RightAnswer();
        });        
    }

    public override void UseHint()
    {
        if (!DataManager.SuggestedHint)
        {
            DataManager.SuggestedHint = true;
            _disposeHand?.Dispose();
        }

        imgCircle.DOKill();
        imgCircle.fillAmount = 0;
        imgCircle.DOFillAmount(1, 0.5f);
    }
}
