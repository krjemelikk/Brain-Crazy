using UnityEngine;
using UnityEngine.UI;

public class Level_189 : BaseLevel
{

    public Transform tfPlayer;
    public Transform tfCheckDone;

    public Image viewPlayer;
    public Sprite spPlayerDone;
    public Sprite spPlayerNormal;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(Mathf.Abs(tfPlayer.localPosition.x - tfCheckDone.localPosition.x) <= 20f)
        {
            isDone = true;
            viewPlayer.sprite = spPlayerDone;
        }
        else
        {
            viewPlayer.sprite = spPlayerNormal;
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
        if (isDone)
            RightAnswer();
        else
            WrongAnswer();
    }
}
