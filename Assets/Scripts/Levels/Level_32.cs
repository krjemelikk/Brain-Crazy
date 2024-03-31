using UnityEngine;

public class Level_32 : BaseLevel
{
    private bool _hold1;
    private bool _hold2;

    private int countClick;

    private bool isEnd;

    protected override void Start()
    {
        base.Start();
        countClick = 0;
        isEnd = _hold1 = _hold2 = false;
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnPointDown(1);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnPointUp(1);
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void CheckAnswer()
    {
        //if (_hold1 && _hold2)
        //{
        //    RightAnswer();
        //}
        //else
        //{
        //    WrongAnswer();
        //}
        if (isEnd)
            return;
        if (countClick >= 2)
        {
            RightAnswer();
            isEnd = true;
        }
        else
        {
            WrongAnswer();
        }
    }

    public void OnPointDown(int indexHold)
    {
        if (indexHold == 1 && !_hold1)
        {
            _hold1 = true;
            countClick++;
           // CheckAnswer();
        }
        if (indexHold == 2 && !_hold2)
        {
            _hold2 = true;
            countClick++;
           // CheckAnswer();
        }
    }

    public void OnPointUp(int indexHold)
    {
        CheckAnswer();
        if (indexHold == 1 && _hold1)
        {
            _hold1 = false;
            countClick--;
            if (countClick < 0)
                countClick = 0;
        }
        if (indexHold == 2 && _hold2)
        {
            _hold2 = false;
            countClick--;
            if (countClick < 0)
                countClick = 0;
        }
        //if (!isEnd)
           // WrongAnswer();
    }

    
}
