using System.Collections.Generic;
using UnityEngine;
using System;

public class Level_200 : BaseLevel
{
    public Dictionary<SystemLanguage, GameObject> QuestionTexts;

    protected override void Start()
    {
        base.Start();
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

    protected override void UpdateText()
    {
        if (txtName != null) txtName.text = $"{Localization.Get("lb_level")} {ID}";

        foreach (var item in QuestionTexts)
        {
            item.Value.SetActive(false);
            Debug.Log("Compare " + string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal)
                + " Localization.language " + Localization.language + " item.Key " + item.Key.ToString());

            if (string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal) == 0)
            {
                item.Value.SetActive(true);
             
            }
        }
       
    }
}
