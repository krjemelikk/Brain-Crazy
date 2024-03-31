using UnityEngine;
using UnityEngine.UI;

public class Level_106 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public InputField inputField;
    private int resultAnswer;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 31181;
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
