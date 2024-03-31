using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_14 : BaseLevel
{
    [Header("Answers")]
    public Button num8;
    public Image HintImg;

    protected override void Start()
    {
        base.Start();
        num8.onClick.AddListener(() => RightAnswer());
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
        if (!DataManager.SuggestedHint)
        {
            DataManager.SuggestedHint = true;
            _disposeHand?.Dispose();
        }

        HintImg.DOKill();
        HintImg.fillAmount = 0;
        HintImg.DOFillAmount(1, 0.5f);

    }
}
