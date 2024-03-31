using UnityEngine;
using DG.Tweening;

public class Level_137 : BaseLevel
{
    public DragUI boxDrag;
    public Transform end;

    public bool isDone;
    private Vector3 posStartBox;

    protected override void Start()
    {
        base.Start();
        posStartBox = boxDrag.transform.localPosition;
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
        if (isDone)
            return;
        base.WrongAnswer();
        boxDrag.transform.localPosition = posStartBox;
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void CheckRightAnswer()
    {
        isDone = true;
        boxDrag.isCanActive = false;
        end.gameObject.SetActive(false);
        boxDrag.transform.DOMove(end.GetChild(0).position, 1f).OnComplete(() => RightAnswer());
    }
}