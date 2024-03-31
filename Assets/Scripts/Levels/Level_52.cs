using UnityEngine;
using UnityEngine.UI;

public class Level_52 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public InputField inputField_Hours;
    public InputField inputField_Minute;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
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
        int _resultHours = 0;
        int _resultMinute = 0;
        if (string.IsNullOrEmpty(inputField_Hours.text) || !int.TryParse(inputField_Hours.text, System.Globalization.NumberStyles.Integer, null, out _resultHours))
        {
            WrongAnswer();
            return;
        }
        if (string.IsNullOrEmpty(inputField_Minute.text) || !int.TryParse(inputField_Minute.text, System.Globalization.NumberStyles.Integer, null, out _resultMinute))
        {
            WrongAnswer();
            return;
        }

        //Debug.Log(UnbiasedTime.Instance.Now.Hour + "h" + UnbiasedTime.Instance.Now.Minute + "m");
        if (_resultHours == 10 && _resultMinute == 10)
            RightAnswer();
        else
            WrongAnswer();
    }
}
