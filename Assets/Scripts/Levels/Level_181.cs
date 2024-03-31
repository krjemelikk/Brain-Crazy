using UnityEngine;
using DG.Tweening;

public class Level_181 : BaseLevel
{

    public DragUI tfCream;
    public DragUI tfHead;

    public Transform tfCheckDone;

    private bool isDone;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isDone) return;
        if (Vector2.Distance(tfCheckDone.position, tfHead.transform.position) <= 0.25f)
        {
            isDone = true;
            tfHead.transform.SetParent(tfCheckDone);
            tfHead.transform.localPosition = Vector3.zero;
            tfHead.SetActiveDrag(false);
            tfCream.SetActiveDrag(false);
            tfCream.transform.DOMove(Vector3.zero, 0.5f).OnComplete(() =>
            {
                RightAnswer();
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
