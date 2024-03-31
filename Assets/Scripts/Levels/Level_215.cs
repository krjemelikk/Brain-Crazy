using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_215 : BaseLevel
{
    public Transform posEat;

    public DragUI dragUI1;
    public DragUI dragUI2;
    public DragUI dragUI3;

    private bool isDone1;
    private bool isDone2;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(Helper.StartAction(() =>
        {
            dragUI1.SetActiveDrag(false);
            dragUI2.GetComponent<Image>().raycastTarget = true;
            dragUI3.GetComponent<Image>().raycastTarget = true;
            dragUI1.transform.DOLocalMoveY(-700f, 1f);
        }, () => dragUI1.transform.localPosition.y <= -500f));
    }

    protected override void Update()
    {
        base.Update();

        if (!isDone1)
        {
            if (Vector2.Distance(dragUI2.transform.position, posEat.transform.position) <= 0.25f)
            {
                isDone1 = true;
                dragUI2.SetActiveDrag(false);
                dragUI2.gameObject.SetActive(false);

                if (isDone1 && isDone2)
                {
                    RightAnswer();
                }
            }
        }

        if (!isDone2)
        {
            if (Vector2.Distance(dragUI3.transform.position, posEat.transform.position) <= 0.25f)
            {
                isDone2 = true;
                dragUI3.SetActiveDrag(false);
                dragUI3.gameObject.SetActive(false);

                if (isDone1 && isDone2)
                {
                    RightAnswer();
                }
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
