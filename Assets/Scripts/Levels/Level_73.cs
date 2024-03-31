using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Level_73 : BaseLevel
{
    public Button[] theCrocodiles;
    public Sprite sprRed;
    public RectTransform RedText;

    public Dictionary<SystemLanguage, LocalizeAnswer> RedTexts;

    private bool isRed = false;
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < theCrocodiles.Length; i++)
        {
            theCrocodiles[i].onClick.AddListener(WrongAnswer);
        }
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

    public void EndDragWrong()
    {
        WrongAnswer();
    }

    public void EndDrag(RectTransform tran)
    {
        if (isRed) return;

        float minDistance = 0.35f;
        int minCrocodile = -1;

        for (int i = 0; i < theCrocodiles.Length; i++)
        {
            var distance = Vector2.Distance(tran.position, theCrocodiles[i].transform.position);
            Debug.Log(i + " / " + distance);
            if (distance > 1f) continue;

            if (distance < minDistance)
            {
                minDistance = distance;
                minCrocodile = i;
            }
        }
        Debug.Log("minCrocodile " + minCrocodile);
        if(minCrocodile >= 0)
        {
            theCrocodiles[minCrocodile].onClick.RemoveAllListeners();
            theCrocodiles[minCrocodile].onClick.AddListener(RightAnswer);
            theCrocodiles[minCrocodile].image.sprite = sprRed;
            isRed = true;
        }     
    }

    protected override void UpdateText()
    {
        foreach (var item in RedTexts)
        {
            item.Value.Question.SetActive(false);
            Debug.Log("Compare " + string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal)
                + " Localization.language " + Localization.language + " item.Key " + item.Key.ToString());

            if (string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal) == 0)
            {
                item.Value.Question.SetActive(true);
                RedText = item.Value.Answer.transform.parent.GetComponent<RectTransform>();
                //if (txtQuestion != null) txtQuestion.text = Localization.Get(KeyQuestion);
                
            }

           
        }

        if (txtName != null) txtName.text = $"{Localization.Get("lb_level")} {ID}";

    }
}
