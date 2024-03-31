using UnityEngine;
using UnityEngine.UI;
using System;

public class Level_16 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public InputField inputField_Hours;
    public InputField inputField_Mins;

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
        int _resultMins = 0;
        if(string.IsNullOrEmpty(inputField_Hours.text) || !int.TryParse(inputField_Hours.text, System.Globalization.NumberStyles.Integer,null, out _resultHours)
            || string.IsNullOrEmpty(inputField_Mins.text) || !int.TryParse(inputField_Mins.text, System.Globalization.NumberStyles.Integer, null, out _resultMins))
        {
            WrongAnswer();
            return;
        }
        Debug.Log(DateTime.Now.Hour + "h" + DateTime.Now.Minute + "m");
        if (_resultHours == DateTime.Now.Hour && _resultMins == DateTime.Now.Minute)
            RightAnswer();
        else
            WrongAnswer();
    }
}
