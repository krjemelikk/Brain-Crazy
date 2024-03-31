using UnityEngine;
using DG.Tweening;

public class Level_238 : BaseLevel
{
    public DragUI dragUI1;
    public Transform tfCheckLock;

    public DragUI dragUI2;
    public Transform tfCheckEat;

    public Transform sit;
    public Transform tfCheckEnd;

    public GameObject objKey;

    private bool isDone1;

    private bool isDone2;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone1 && dragUI2.isCanActive)
        {
            if (Vector2.Distance(dragUI2.transform.position, tfCheckEat.position) <= 0.2f)
            {
                dragUI2.transform.position = tfCheckEat.position;
                dragUI2.SetActiveDrag(false);
                sit.DOLocalMove(tfCheckEnd.localPosition,1f);
                sit.DOScale(0.25f, 1f).OnComplete(()=>
                {
                    objKey.SetActive(true);
                    isDone1 = true;
                });
            }
        }

        if (isDone1 && !isDone2 && dragUI1.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfCheckLock.position) <= 0.2f)
            {
                dragUI1.SetActiveDrag(false);
                RightAnswer();
                isDone2 = true;
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
