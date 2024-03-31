using System.Collections.Generic;
using UnityEngine.UI;

public class Level_230 : BaseLevel
{
    public List<Text> lsText = new List<Text>();
    private int index = 0;

    private bool isDone;

    public void SetTextCustum(int value)
    {
        lsText[index].text = value.ToString();
        if (index == 0 && value == 1)
            isDone = true;
        else if (index == 1 && value == 2 && isDone)
            isDone = true;
        else if (index == 2 && value == 2 && isDone)
            isDone = true;
        else if (index == 3 && value == 4 && isDone)
            isDone = true;
        else
            isDone = false;
        index++;
    }

    public void Clear()
    {
        foreach(Text tx in lsText)
        {
            tx.text = "";
        }
        index = 0;
    }

    public void Submit()
    {
        if (isDone)
            RightAnswer();
        else
            WrongAnswer();
    }

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
        Clear();
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
}
