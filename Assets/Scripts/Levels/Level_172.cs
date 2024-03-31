using UnityEngine;
using DG.Tweening;

public class Level_172 : BaseLevel
{
    public Transform tfRocket;
    public Transform tfCheckDone;
    public DragUI fire;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isDone) return;
        if(Vector2.Distance(tfCheckDone.position,fire.transform.position) <= 0.5f)
        {
            isDone = true;
            fire.SetActiveDrag(false);
            fire.transform.DOMove(tfCheckDone.position, 0.5f).OnComplete(() => 
            {
                fire.transform.SetParent(tfCheckDone);
                tfRocket.DOLocalMoveY(100f, 0.5f).OnComplete(() => RightAnswer());
            });
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
