using UnityEngine;
using UnityEngine.UI;

public class Level_138 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;

    private int resultAnswer;

    [SerializeField]
    public Text txtAnswer;

    private int answerCurrent = 0;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 17;
        txtAnswer.text = answerCurrent.ToString();

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
        int _result = 0;
        if (string.IsNullOrEmpty(txtAnswer.text) || !int.TryParse(txtAnswer.text, System.Globalization.NumberStyles.Integer, null, out _result))
        {
            CheckClear();
            WrongAnswer();
            return;
        }

        if (_result == resultAnswer) RightAnswer();
        else WrongAnswer();
    }

    public void CheckClear()
    {
        answerCurrent = 0;
        txtAnswer.text = answerCurrent.ToString();
    }

    public void NextAmswer()
    {
        answerCurrent++;
        txtAnswer.text = answerCurrent.ToString();
    }

    public void BackAmswer()
    {
        answerCurrent--;
        txtAnswer.text = answerCurrent.ToString();
    }
}
