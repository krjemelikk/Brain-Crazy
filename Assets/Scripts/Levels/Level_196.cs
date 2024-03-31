using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_196 : BaseLevel
{
    public Image imgPlayer;
    public Image imgDefense;

    public Transform toPosLeft;
    public Transform toPosCenter;

    public Transform tfBall;

    public Sprite spDefense;
    public Sprite spPlayer;

    public Transform tfShue;

    public GameObject objShue;

    public DragUI dragShue;

    private bool isEnd;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isEnd) return;
        if (Vector2.Distance(tfShue.position, toPosCenter.transform.position) <= 0.25f)
        {
            isEnd = true;
            imgDefense.sprite = spDefense;
            imgDefense.SetNativeSize();
            tfShue.SetParent(toPosCenter);
            tfShue.localPosition = Vector3.zero;
            dragShue.SetActiveDrag(false);
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

    public void OnclickShoot()
    {
        if (!isEnd)
        {
            tfShue.gameObject.SetActive(false);
            imgPlayer.sprite = spPlayer;
            imgPlayer.SetNativeSize();
            objShue.SetActive(true);
            tfBall.DOMove(toPosCenter.position, 0.5f).OnComplete(() =>
            {
                imgDefense.sprite = spDefense;
                imgDefense.SetNativeSize();
                WrongAnswer();
            });
        }
        else
        {
            imgPlayer.sprite = spPlayer;
            imgPlayer.SetNativeSize();

            tfBall.DOMove(toPosLeft.position, 0.5f).OnComplete(() =>
            {
                imgDefense.sprite = spDefense;
                imgDefense.SetNativeSize();
                RightAnswer();
            });
        }
    }
}
