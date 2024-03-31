using UnityEngine;
using UnityEngine.UI;

public class Level_214 : BaseLevel
{
    public Image imgSmartPhone;

    public Sprite spSmartPhone;

    private bool isDown;
    private float timeDown = 0f;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(isDown && !isDone)
        {
            timeDown += Time.deltaTime;
            if(timeDown >= 2)
            {
                imgSmartPhone.sprite = spSmartPhone;
                RightAnswer();
                isDone = true;
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnDogDown()
    {
        isDown = true;
        timeDown = 0f;
    }
    
    public void OnDogUp()
    {
        isDown = false;
    }
}
