using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_220 : BaseLevel
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
        if (!isDone)
        {
            if (Vector2.Distance(tfPlayer.position, tfCheckDone.position) <= 0.25f)
            {
                isDone = true;
                viewPlayer.sprite = spPlayerDone;

                viewPlayer.transform.DOLocalMoveY(400f, 1f).OnComplete(() =>
                {
                    viewPlayer.sprite = spPlayerNormal;
                    RightAnswer();
                });
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
}
