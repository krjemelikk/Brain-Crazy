using UnityEngine;
using DG.Tweening;

public class Level_174 : BaseLevel
{

    public Transform tfCar;
    public Transform tfRotatorCar;
    public Transform tfCheckDone;
    public DragUI sun;

    public Transform tfCheckEnd;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isDone) return;
        if(Vector2.Distance(tfCheckDone.position,sun.transform.position) <= 0.5f)
        {
            isDone = true;
            sun.SetActiveDrag(false);
            sun.transform.DOMove(tfCheckDone.position, 0.5f).OnComplete(() => 
            {
                sun.transform.SetParent(tfCheckDone);
                tfRotatorCar.localEulerAngles = new Vector3(0f, 0f, -5f);
                tfCar.DOLocalMoveX(tfCheckEnd.localPosition.x, 0.5f).SetUpdate(true).OnComplete(() => RightAnswer());
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
