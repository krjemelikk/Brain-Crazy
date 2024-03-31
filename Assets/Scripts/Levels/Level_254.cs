using UnityEngine;
using UnityEngine.UI;

public class Level_254 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public InputField inputField;

    private string resultAnswer;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = "Mary";
    }

    private void CheckAnswer()
    {
       
        if (string.IsNullOrEmpty(inputField.text))
        {
            WrongAnswer();
            return;
        }
        string _result = inputField.text.ToUpper();
        if (_result == resultAnswer.ToUpper()) RightAnswer();
        else WrongAnswer();
    }
}
