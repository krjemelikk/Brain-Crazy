using UnityEngine;

public class Level_256 : BaseLevel
{
    public Transform posEnd;

    public DragUI dragUI1;
    public DragUI dragUI2;
    public DragUI dragUI3;
    public DragUI dragUI4;

    public bool isDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI2.transform.position) >= 0.5f &&
                Vector2.Distance(dragUI1.transform.position, dragUI3.transform.position) >= 0.5f &&
                Vector2.Distance(dragUI1.transform.position, dragUI4.transform.position) >= 0.5f)
            {
                dragUI2.SetActiveDrag(false);
                dragUI3.SetActiveDrag(false);
                dragUI4.SetActiveDrag(false);
                isDone = true;
            }
        }
        else
        {
            if (Vector2.Distance(dragUI1.transform.position, posEnd.position) <= 0.25f && dragUI1.isCanActive)
            {
                dragUI1.transform.position = posEnd.position;
                RightAnswer();
                dragUI1.SetActiveDrag(false);
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
