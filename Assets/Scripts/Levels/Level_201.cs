using UnityEngine;
using UnityEngine.UI;

public class Level_201 : BaseLevel
{

    public Transform tfPlayer;
    public Transform tfCheckDone;

    public Image viewPlayer;
    public Sprite spPlayerDone;
    public DragUI dragPlayer;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isDone) return;
        if (Vector2.Distance(tfPlayer.position, tfCheckDone.position) <= 0.25f)
        {
            isDone = true;
            dragPlayer.SetActiveDrag(false);
            viewPlayer.sprite = spPlayerDone;
            RightAnswer();
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
}
