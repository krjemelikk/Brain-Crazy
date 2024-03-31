using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_186 : BaseLevel
{
    public Image imgLeft;
    public Image imgRight;
    public Image imgCenter;

    public Transform toPosLeft;
    public Transform toPosRight;
    public Transform toPosCenter;

    public Transform tfBall;

    private bool isDone;
    private bool isEnd;
    private bool isHoleDenfend;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnPointDown();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            OnPointUp();
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
        StartCoroutine(Helper.StartAction(() =>
        {
            GameController.Instance.ResetLevel();
        }, 1f));
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnclickShoot(int index)
    {
        if (!isEnd)
            isEnd = true;
        switch (index)
        {
            case 0:
                isHoleDenfend = isDone;
                if (!isHoleDenfend)
                {
                    imgCenter.color = new Color(1f, 1f, 1f, 0f);
                    imgLeft.color = Color.white;
                }
                tfBall.DOMove(toPosLeft.position, 0.5f).OnComplete(() =>
                {
                    if (isHoleDenfend)
                    {
                        RightAnswer();
                    }
                    else
                    {

                        WrongAnswer();
                    }
                });
                break;
            case 1:
                tfBall.DOMove(toPosCenter.position, 0.5f).OnComplete(() =>
                {
                    WrongAnswer();
                });
                break;
            case 2:
                isHoleDenfend = isDone;
                if (!isHoleDenfend)
                {
                    imgCenter.color = new Color(1f, 1f, 1f, 0f);
                    imgRight.color = Color.white;
                }
                tfBall.DOMove(toPosRight.position, 0.5f).OnComplete(() =>
            {
                if (isHoleDenfend)
                {
                    RightAnswer();
                }
                else
                {

                    WrongAnswer();
                }
            });
                break;
        }
    }

    public void OnPointDown()
    {
        if (!isDone)
        {
            isDone = true;
        }
    }

    public void OnPointUp()
    {
        if (isDone)
        {
            isDone = false;
        }
    }
}
