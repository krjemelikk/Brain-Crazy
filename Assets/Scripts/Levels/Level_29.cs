using UnityEngine;
using UnityEngine.UI;

public class Level_29 : BaseLevel
{
    public Image theCat;
    public Sprite theCatOpenEye;
    public Sprite theCatCloseEye;

    private float timerHold;
    private float timeNeedHold;

    private bool isHold = false;
    private bool isEnd = false;

    protected override void Start()
    {
        base.Start();
        timeNeedHold = 1f;
        timerHold = 0;
        theCat.sprite = theCatOpenEye;
    }

    protected override void Update()
    {
        base.Update();

        if (isEnd) return;

        if (isHold)
        {
            if(timerHold >= timeNeedHold)
            {
                timerHold = 0;
                RightAnswer();
            }
            else
            {
                timerHold += Time.deltaTime;
            }            
        }
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
        theCat.sprite = theCatCloseEye;
        isEnd = true;
        base.RightAnswer();        
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnPointDown()
    {
        isHold = true;
    }

    public void OnPointUp()
    {
        isHold = false;
        timerHold = 0;
    }
}
