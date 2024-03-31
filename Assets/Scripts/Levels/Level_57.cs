using UnityEngine;
using UnityEngine.UI;

public class Level_57 : BaseLevel
{
    private bool isEnd;
    [SerializeField] private Button[] numbersBtn;
    [SerializeField] private Button btOK;
    [SerializeField] private Text inputTxt;
    private int resultAnswer;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < numbersBtn.Length; i++)
        {
            int index = i;
            numbersBtn[i].onClick.AddListener(() => { OnClickNumber(index); });
        }

        resultAnswer = 24;

        btOK.onClick.AddListener(() => { CheckAnswer(); });
    }

    public void OnClickNumber(int number)
    {
        if (isEnd) return;

        inputTxt.text += number;
    }

    public void ClearNumber()
    {
        inputTxt.text = "";
    }

    private void CheckAnswer()
    {
        int _result = 0;
        if (string.IsNullOrEmpty(inputTxt.text) || !int.TryParse(inputTxt.text, System.Globalization.NumberStyles.Integer, null, out _result))
        {
            WrongAnswer();
            return;
        }

        if (_result == resultAnswer) RightAnswer();
        else WrongAnswer();
    }
}
