using UnityEngine;

public class Level_233 : BaseLevel
{
    public DragUI dragUI1;
    public Transform tfCheckLock;

    private bool isDone1;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone1 && dragUI1.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfCheckLock.position) <= 0.2f)
            {
                dragUI1.transform.position = tfCheckLock.transform.position;
                dragUI1.SetActiveDrag(false);
                RightAnswer();

                isDone1 = true;
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
