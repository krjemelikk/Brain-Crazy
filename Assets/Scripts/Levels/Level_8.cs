using UnityEngine;
using UnityEngine.UI;

public class Level_8 : BaseLevel
{
    [Header("Answers")]
    public Button num2;
    private int countCLick_num2;
    public Button num7;
    private int countCLick_num7;
    public Button num8;
    private int countCLick_num8;
    public Button num6;
    private int countCLick_num6;
    public Button num3;
    private int countCLick_num3;

    private int resultAnswer;
    private int countClick;
    private int resultClick;

    protected override void Start()
    {
        base.Start();
        resultAnswer = 10;
        countClick = resultClick = 0;
        countCLick_num2 = countCLick_num7 = countCLick_num8 = countCLick_num6 = countCLick_num3 = 0;
        num2.onClick.AddListener(() => OnClickNumber(2));
        num7.onClick.AddListener(() => OnClickNumber(7));
        num8.onClick.AddListener(() => OnClickNumber(8));
        num6.onClick.AddListener(() => OnClickNumber(6));
        num3.onClick.AddListener(() => OnClickNumber(3));
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

    private void OnClickNumber(int value)
    {
        countClick++;
        resultClick += value;
        switch (value)
        {
            case 2:
                countCLick_num2++;
                num2.GetComponentInChildren<Text>().text = $"x{countCLick_num2}";
                break;
            case 7:
                countCLick_num7++;
                num7.GetComponentInChildren<Text>().text = $"x{countCLick_num7}";
                break;
            case 8:
                countCLick_num8++;
                num8.GetComponentInChildren<Text>().text = $"x{countCLick_num8}";
                break;
            case 6:
                countCLick_num6++;
                num6.GetComponentInChildren<Text>().text = $"x{countCLick_num6}";
                break;
            case 3:
                countCLick_num3++;
                num3.GetComponentInChildren<Text>().text = $"x{countCLick_num3}";
                break;
        }

        if (countClick == 3)
            CheckAnswer();
    }

    private void CheckAnswer()
    {
        if (resultClick == resultAnswer)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
            ResetLevel();
        }

    }

    private void ResetLevel()
    {
        countClick = resultClick = 0;
        countCLick_num2 = countCLick_num7 = countCLick_num8 = countCLick_num6 = countCLick_num3 = 0;
        num2.GetComponentInChildren<Text>().text = string.Empty;
        num7.GetComponentInChildren<Text>().text = string.Empty;
        num8.GetComponentInChildren<Text>().text = string.Empty;
        num6.GetComponentInChildren<Text>().text = string.Empty;
        num3.GetComponentInChildren<Text>().text = string.Empty;
    }
}
