using System.Collections.Generic;
using UnityEngine;
using System;


public class LocalizeAnswer
{
    public GameObject Question;
    public RectTransform Answer;
}

public class Level_22 : BaseLevel
{
    public RectTransform thePlate1;
    public RectTransform thePlate2;
    public RectTransform Chicken;
    public RectTransform ChickenText;

    public Dictionary<SystemLanguage, LocalizeAnswer> ChickenTexts;

    private bool isFullPlate1;
    private bool isFullPlate2;
    private Transform currentTransform;
    float maxX_1, minX_1, maxY_1, minY_1;
    float maxX_2, minX_2, maxY_2, minY_2;
    protected override void Start()
    {
        base.Start();
        maxX_1 = thePlate1.transform.localPosition.x + thePlate1.rect.width / 2;
        minX_1 = thePlate1.transform.localPosition.x - thePlate1.rect.width / 2;
        minY_1 = thePlate1.transform.localPosition.y - thePlate1.rect.height / 2;
        maxY_1 = thePlate1.transform.localPosition.y + thePlate1.rect.height / 2;

        maxX_2 = thePlate2.transform.localPosition.x + thePlate2.rect.width / 2;
        minX_2 = thePlate2.transform.localPosition.x - thePlate2.rect.width / 2;
        minY_2 = thePlate2.transform.localPosition.y - thePlate2.rect.height / 2;
        maxY_2 = thePlate2.transform.localPosition.y + thePlate2.rect.height / 2;
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
        if ((CheckInBound_1(Chicken) && CheckInBound_2(ChickenText))
            || (CheckInBound_2(Chicken) && CheckInBound_1(ChickenText)))
        {
            RightAnswer();
        }
    }

    public void BeginDrag(Transform tran)
    {
        currentTransform = tran;
    }

    public void EndDrag(RectTransform tran)
    {
        if (CheckInBound_1(tran))
        {
            tran.localPosition = thePlate1.localPosition;
        }
        else if (CheckInBound_2(tran))
        {
            tran.localPosition = thePlate2.localPosition;
        }

        CheckAnswer();
    }

    private bool CheckInBound_1(RectTransform transform)
    {
        return transform.transform.localPosition.x < maxX_1
            && transform.transform.localPosition.x > minX_1
            && transform.transform.localPosition.y > minY_1
            && transform.transform.localPosition.y < maxY_1;
    }

    private bool CheckInBound_2(RectTransform transform)
    {
        return transform.transform.localPosition.x < maxX_2
            && transform.transform.localPosition.x > minX_2
            && transform.transform.localPosition.y > minY_2
            && transform.transform.localPosition.y < maxY_2;
    }

    protected override void UpdateText()
    {
        foreach (var item in ChickenTexts)
        {
            item.Value.Question.SetActive(false);
            Debug.Log("Compare " + string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal)
                + " Localization.language " + Localization.language + " item.Key " + item.Key.ToString());

            if (string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal) == 0)
            {
                item.Value.Question.SetActive(true);
                ChickenText = item.Value.Answer.transform.parent.GetComponent<RectTransform>();
                //if (txtQuestion != null) txtQuestion.text = Localization.Get(KeyQuestion);
                if (txtName != null) txtName.text = $"{Localization.Get("lb_level")} {IDQuestion}";
            }
        }
       
    }
}
