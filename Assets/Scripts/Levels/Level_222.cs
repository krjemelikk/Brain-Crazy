using System.Collections.Generic;
using UnityEngine;

public class Level_222 : BaseLevel
{
    public Transform tfCheckDone;

    public List<DragUI> lsDragUI = new List<DragUI>();

    private bool isDone;

    private int count = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone && count <= 4)
        {
            if (GetDraging() != null)
            {
                foreach (DragUI dragUI in lsDragUI)
                {
                    if(dragUI != GetDraging())
                    {
                        CheckDistance(dragUI, GetDraging());
                    }
                }

                if(count >= 4)
                {
                    if (Vector2.Distance(GetDraging().transform.position, tfCheckDone.position) <= 0.25f)
                    {
                        GetDraging().SetActiveDrag(false);
                        RightAnswer();
                        isDone = true;
                    }
                }
            }
        }
    }

    public DragUI GetDraging()
    {
        foreach (DragUI dragUI in lsDragUI)
        {
            if (dragUI.isDraging)
                return dragUI;
        }
        return null;
    }

    public void CheckDistance(DragUI dragUI1, DragUI dragUI2)
    {
        if (dragUI1.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI2.transform.position) <= 0.25f)
            {
                dragUI1.SetActiveDrag(false);
                dragUI1.transform.SetParent(dragUI2.transform);
                count++;
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
        if (isDone) return;
        base.WrongAnswer();
        GameController.Instance.ResetLevel();
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
