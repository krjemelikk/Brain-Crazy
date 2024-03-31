using UnityEngine;
using UnityEngine.UI;

public class Level_12 : BaseLevel
{
    private bool _hold1;
    private bool _hold2;

    public Sprite Light;
    public Sprite Dark;

    public GameObject check1;
    public GameObject check2;

    public Image imgLight;
    private bool isRight;

    protected override void Start()
    {
        base.Start();

        imgLight.sprite = Dark;
        isRight = false;
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
        if (isRight)
            return;
        base.RightAnswer();
        isRight = true;
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void CheckAnswer()
    {        
        if (Vector2.Distance(check1.transform.position, check2.transform.position) <= 0.02f || check1.transform.position.y < check2.transform.position.y)
        {
            imgLight.sprite = Light;
            RightAnswer();
        }
    }   

    public void HoldClick(int indexHold)
    {
        if (indexHold == 1 && !_hold1)
        {
            _hold1 = true;
        }
        else if(indexHold == 2 && !_hold2)
        {
            _hold2 = true;
        }
    }

    public void OnPointDown(int indexHold)
    {
        if (indexHold == 1 && !_hold1)
        {
            _hold1 = true;
        }
        else if(indexHold == 2 && !_hold2)
        {
            _hold2 = true;
        }
        CheckAnswer();
    }
    
    public void OnPointUp(int indexHold)
    {
        if (indexHold == 1 && _hold1)
        {
            _hold1 = false;
        }
        else if(indexHold == 2 && _hold2)
        {
            _hold2 = false;
        }
    }
}
