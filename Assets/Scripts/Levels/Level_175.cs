using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_175 : BaseLevel
{
    public Transform tfFoot;
    public Transform tfLeft;
    public Transform tfRight;

    public Image imgGian;
    public Sprite spGian;

    private bool isLeft;
    private bool isPause;
    public bool isDone;
    public bool isFaild;
    public void MoveFoot()
    {
        if (isPause) return;
        if (isLeft)
        {
            if (Vector2.Distance(tfFoot.position, tfLeft.position) <= 0.05f)
            {
                isLeft = false;
            }
            Vector2 dir = tfLeft.position - tfFoot.position;
            tfFoot.Translate(dir * Time.deltaTime * 10f);
        }
        else
        {
            if (Vector2.Distance(tfFoot.position, tfRight.position) <= 0.05f)
            {
                isLeft = true;
            }
            Vector2 dir = tfRight.position - tfFoot.position;
            tfFoot.Translate(dir * Time.deltaTime * 10f);
        }
    }

    public void Attack()
    {
        isPause = true;
        isFaild = false;
        isDone = false;
        tfFoot.DOLocalMoveY(-165f, 0.25f).OnComplete(() =>
        {
            tfFoot.DOLocalMoveY(tfLeft.localPosition.y, 0.25f).OnComplete(() =>
            {
                if (isDone && !isFaild)
                {
                    //RightAnswer()
                }
                else
                {
                    isPause = false;
                }
            });
        });
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        MoveFoot();
        if(Input.GetMouseButtonDown(0)&& !isPause)
        {
            Attack();
        }
    }

    public override void StartLevel()
    {
        base.StartLevel();
        StartCoroutine(Helper.StartAction(() => RightAnswer(), () => isDone && !isFaild));
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

    public void SetGian()
    {
        imgGian.sprite = spGian;
    }
}
