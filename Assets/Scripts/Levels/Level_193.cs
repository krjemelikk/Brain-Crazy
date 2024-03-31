using UnityEngine;

public class Level_193 : BaseLevel
{
    [SerializeField]
    private bool _hold1;
    [SerializeField]
    private bool _hold2;

    [SerializeField]
    private int countClick;

    private bool isEnd;

    public GameObject objO1;

    public GameObject objO2;

    protected override void Start()
    {
        base.Start();
        countClick = 0;
        isEnd = _hold1 = _hold2 = false;
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
        if (isEnd)
            return;
        if (countClick >= 2)
        {
            objO1.SetActive(true);
            objO2.SetActive(true);
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
        }
        if (indexHold == 2 && !_hold2)
        {
            _hold2 = true;
            countClick++;
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
    } 
}
