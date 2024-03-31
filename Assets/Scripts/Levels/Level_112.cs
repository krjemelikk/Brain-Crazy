using UnityEngine;
using DG.Tweening;

public class Level_112 : BaseLevel
{
    public Transform tfStart;
    public Transform tfsEnd;
    public DragUI player;
    public LineRenderer line;
    public bool isComplete;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Vector3 posStart = tfStart.position;
        Vector3 posEnd = tfsEnd.position;
        posStart.z = posEnd.z = 0f;
        line.SetPosition(0, posStart);
        line.SetPosition(1, posEnd);
        if (player.transform.localPosition.y > 250f && player.transform.localPosition.x < -30f && !isComplete)
        {
            player.SetActiveDrag(false);
            player.transform.DOLocalMove(new Vector3(-125f, 350f, 0f), 1f).OnComplete(()=>
            {
                RightAnswer();
            });
            isComplete = true;
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
