using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_260 : BaseLevel
{
    public DragUI dragUI1;
    public DragUI dragUI2;

    public bool isDone;
    public bool isDone1;
    public bool isDone2;
    public bool isDone3;

    public Image imgPlayer_1;
    public Sprite spPlayer_1;
    public Image imgPlayer_2;
    public Sprite spPlayer_2;

    public Transform tfSun;
    public Transform tfFires;

    public GameObject fireLong;
    public GameObject fireDongLua;

    public Image imgDark;

    protected override void Start()
    {
        dragUI1.SetActiveDragNew(false);
        dragUI2.SetActiveDragNew(false);

        base.Start();
    }

    public void OnFire()
    {
        if (isDone) return;
        dragUI1.gameObject.SetActive(true);
        dragUI1.SetActiveDragNew(true);
        isDone = true;
    }

    protected override void Update()
    {
        base.Update();
        if (isDone && !isDone1)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfSun.position) <= 0.25f && dragUI1.isCanActive)
            {
                fireLong.SetActive(true);
                isDone1 = true;
            }
        }

        if (isDone1 && !isDone2)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfFires.position) <= 0.25f && dragUI1.isCanActive)
            {
                dragUI1.SetActiveDragNew(false);
                fireDongLua.transform.DOScale(1f, 1f);
                dragUI2.transform.DOScale(1f, 1f).OnComplete(() =>
                {
                    dragUI2.SetActiveDragNew(true);
                });
                isDone2 = true;
            }
        }

        if (isDone2 && !isDone3)
        {
            if (Vector2.Distance(dragUI2.transform.position, tfSun.position) <= 0.25f && dragUI2.isCanActive)
            {
                dragUI2.SetActiveDragNew(false);
                dragUI2.transform.position = tfSun.transform.position;
                imgDark.DOFade(0.5f, 1f).OnComplete(() =>
                {
                    imgPlayer_1.sprite = spPlayer_1;
                    imgPlayer_2.sprite = spPlayer_2;
                    RightAnswer();
                });
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
