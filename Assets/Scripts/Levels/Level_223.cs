using UnityEngine;

public class Level_223 : BaseLevel
{
    public DragUI dragGlass;
    public Transform tfCheckDone;

    public GameObject objDone;

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
            if (Vector2.Distance(dragGlass.transform.position, tfCheckDone.position) <= 0.25f)
            {
                dragGlass.SetActiveDrag(false);
                dragGlass.transform.SetParent(tfCheckDone);
                dragGlass.transform.localPosition = Vector3.zero;
                isDone = true;
                objDone.SetActive(true);
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
