using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_90 : BaseLevel
{
    [Header("Answers")]
    public Button[] objectChoice;
    public Image[] HintImg;
    private int numObjectFound;
    
    protected override void Start()
    {
        base.Start();
        numObjectFound = 0;
        for (int i = 0; i < objectChoice.Length; i++)
        {
            int index = i;
            objectChoice[index].onClick.RemoveAllListeners();
            objectChoice[index].interactable = true;
            objectChoice[index].onClick.AddListener(() => { objectChoice[index].interactable = false; CheckAnswer(index); });
        }
        for (int i = 0; i < HintImg.Length; i++)
        {
            HintImg[i].DOKill();
            HintImg[i].fillAmount = 0;
        }
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

        for (int i = 0; i < HintImg.Length; i++)
        {
            HintImg[i].DOKill();
            HintImg[i].fillAmount = 0;
            HintImg[i].DOFillAmount(1, 0.5f);
        }
    }

    public void CheckAnswer(int index)
    {
        numObjectFound += 1;

        HintImg[index].DOKill();
        HintImg[index].fillAmount = 0;
        HintImg[index].DOFillAmount(1, 0.5f);

        if(numObjectFound >= 4)
        {
            RightAnswer();
        }
    }
}
