using UnityEngine;
using UnityEngine.UI;

public class Level_78 : BaseLevel
{
    [Header("Answers")]
    public RectTransform the78;
    public RectTransform theTextAnswer;
    public Text theTextInput;

    private string answer;
    private Vector3 posStartThe78;
    private bool isEnd;

    protected override void Start()
    {
        base.Start();
        posStartThe78 = the78.transform.position;
        answer = string.Empty;
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
        answer = string.Empty;
        UpdateUI();
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        isEnd = true;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnClickNumber(int number)
    {
        if (isEnd) return;

        answer += number.ToString();
        UpdateUI();
    }

    public void OnClickOK()
    {
        WrongAnswer();
    }

    public void OnClickClear()
    {
        answer = string.Empty;
        UpdateUI();
    }

    private void UpdateUI()
    {
        theTextInput.text = answer;
    }

    public void EndDrag()
    {
        var distance = Vector2.Distance(the78.transform.position, theTextAnswer.transform.position);
        Debug.Log(distance);
        if (distance <= 0.5f)
        {
            the78.transform.position = theTextAnswer.transform.position;
            RightAnswer();
        }
        else
        {
            the78.transform.position = posStartThe78;
        }
    }
}