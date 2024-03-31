using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Level_79 : BaseLevel
{
    [Header("Answers")]
    public RectTransform theLine;
    public RectTransform theLineMid;
    public Text[] theTexts;

    private Vector3 posStarttheLine;
    private bool isEnd, isMoveLine;
    private int countClick9 = 0;

    protected override void Start()
    {
        base.Start();
        posStarttheLine = theLine.transform.position;
        isMoveLine = false;
        ResetUpdateUI();
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
        countClick9 = 0;
        ResetUpdateUI();
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

        for (int i = 0; i < theTexts.Length; i++)
        {
            if (theTexts[i].gameObject.activeInHierarchy) continue;

            theTexts[i].gameObject.SetActive(true);
            theTexts[i].text = number.ToString();
            break;
        }

        if (number == 9)
        {
            countClick9++;
        }
    }

    public void OnClickOK()
    {
        if (countClick9 == 3)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void OnClickClear()
    {
        ResetUpdateUI();
    }

    private void ResetUpdateUI()
    {
        for (int i = 0; i < theTexts.Length; i++)
        {
            theTexts[i].gameObject.SetActive(false);
            theTexts[i].text = string.Empty;
        }
        theLine.transform.position = posStarttheLine;
        theLine.GetComponent<EventTrigger>().enabled = true;
        isMoveLine = false;
    }

    public void EndDrag()
    {
        var distance = Vector3.Distance(theLine.transform.position, theLineMid.transform.position);

        if (distance <= 0.1f)
        {
            theLine.transform.position = theLineMid.transform.position;
            theLine.GetComponent<EventTrigger>().enabled = false;
            isMoveLine = true;
        }
        else
        {
            isMoveLine = false;
            theLine.transform.position = posStarttheLine;
        }
    }
}