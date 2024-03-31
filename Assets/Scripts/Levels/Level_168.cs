using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_168 : BaseLevel
{

    public List<Button> lsBtnCheck = new List<Button>();
    public DragUI head;

    private Vector3 posHeadStart;

    protected override void Start()
    {
        base.Start();
        posHeadStart = head.transform.position;
        lsBtnCheck[0].onClick.AddListener(CheckAnswer);
        lsBtnCheck[1].onClick.AddListener(WrongAnswer);
        lsBtnCheck[2].onClick.AddListener(WrongAnswer);
        lsBtnCheck[3].onClick.AddListener(WrongAnswer);
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

    public void CheckAnswer()
    {
        if (Vector2.Distance(posHeadStart, head.transform.position) <= 0.45f)
        {
            WrongAnswer();
        }
        else
        {
            RightAnswer();
        }
    }
}
