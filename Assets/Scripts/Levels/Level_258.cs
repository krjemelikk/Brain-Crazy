using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_258 : BaseLevel
{
    public Transform posEnd;
    public Transform posEnd1;
    public Transform posDog;

    public DragUI dragUI1;
    public DragUI dragUI2;

    public bool isDone;
    public bool isDone2;
    public bool isDone3;

    public Image imgDog;
    public Sprite spDog;

    public GameObject objDog1;
    public GameObject objDog2;
    public GameObject objAttack;

    protected override void Start()
    {
        dragUI1.SetActiveDragNew(false);
        dragUI2.SetActiveDragNew(false);

        base.Start();
    }

    public void OnDog()
    {
        if (isDone) return;
        imgDog.sprite = spDog;
        imgDog.transform.DOMove(posDog.position, 1f).OnComplete(() =>
        {
            dragUI1.SetActiveDragNew(true);
            isDone = true;
        });
    }

    protected override void Update()
    {
        base.Update();
        if (isDone && !isDone2)
        {
            if (Vector2.Distance(dragUI1.transform.position, posEnd.position) >= 0.5f && dragUI1.isCanActive)
            {
                dragUI1.SetActiveDragNew(false);
                dragUI2.SetActiveDragNew(true);

                isDone2 = true;
            }
        }
        if (isDone2 && !isDone3)
        {
            if (Vector2.Distance(dragUI2.transform.position, posEnd1.position) <= 0.25f && dragUI2.isCanActive)
            {
                dragUI2.transform.position = posEnd1.position;
                dragUI2.SetActiveDragNew(false);

                objDog1.SetActive(false);
                objDog2.SetActive(false);
                objAttack.SetActive(true);

                RightAnswer();
                isDone3 = true;
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
