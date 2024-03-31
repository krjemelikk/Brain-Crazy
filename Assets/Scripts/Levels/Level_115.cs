using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_115 : BaseLevel
{
    public List<Image> lsImg = new List<Image>();
    public List<Sprite> lsSpNumber = new List<Sprite>();
    private List<int> lsAnswer = new List<int>();
    private bool isDone = true;
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

    public void OnclickNumber(int number)
    {
        if (lsAnswer.FindAll(x => x == number).Count > 0)
            return;
        lsAnswer.Add(number);
        lsImg[number - 1].gameObject.SetActive(true);
        lsImg[number - 1].sprite = lsSpNumber[lsAnswer.Count - 1];
        if (number != lsAnswer.Count)
            isDone = false;
        if (lsAnswer.Count >= 6)
        {
            if (isDone)
                RightAnswer();
            else
            {
                WrongAnswer();
                ResetAll();
            }
        }
    }

    public void ResetAll()
    {
        foreach(Image img in lsImg)
        {
            img.gameObject.SetActive(false);
        }
        lsAnswer.Clear();
        isDone = true;
    }
}
