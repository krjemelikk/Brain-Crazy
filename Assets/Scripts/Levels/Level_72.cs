using UnityEngine;
using UnityEngine.UI;

public class Level_72 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public Button btNext;
    public Button btPre;
    public Text txtNumber;

    private int resultAnswer;

    private int answer;

    private bool isUsedHint;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        btNext.onClick.AddListener(() => OnClickNext());
        btPre.onClick.AddListener(() => OnClickPre());

        resultAnswer = 6;
        answer = 0;
        UpdateUI();
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
        if (answer == resultAnswer)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    private void OnClickNext()
    {
        answer++;
        UpdateUI();
    }

    private void OnClickPre()
    {
        answer--;
        if (answer < 0)
            answer = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        txtNumber.text = answer.ToString();
    }
}
