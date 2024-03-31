using UnityEngine;
using UnityEngine.UI;

public class Level_111 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public InputField inputField;

    private int resultAnswer;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 6;
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
        if (string.IsNullOrEmpty(inputField.text) || !int.TryParse(inputField.text, System.Globalization.NumberStyles.Integer, null, out _result))
        {
            WrongAnswer();
            return;
        }

        if (_result == resultAnswer) RightAnswer();
        else WrongAnswer();
    }
}
