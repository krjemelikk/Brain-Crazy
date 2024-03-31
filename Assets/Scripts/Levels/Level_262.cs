using UnityEngine;
using UnityEngine.UI;

public class Level_262 : BaseLevel
{
    public DragUI dragUI1;
    public DragUI dragUI2;

    public DragUI dragUI3;

    public GameObject thunder;
    public GameObject vois;

    public Transform handVoi;

    public Image imgVoi;
    public Sprite spVoi1;
    public Sprite spVoi2;

    private bool isDone;
    private bool isDone1;

    protected override void Start()
    {
        base.Start();
        dragUI3.SetActiveDragNew(false);
    }

    protected override void Update()
    {
        base.Update();

        if (!isDone)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI2.transform.position) <= 0.25f)
            {
                dragUI1.SetActiveDragNew(false);
                dragUI2.SetActiveDragNew(false);

                thunder.SetActive(true);
                imgVoi.sprite = spVoi1;
                imgVoi.SetNativeSize();

                dragUI3.SetActiveDragNew(true);

                isDone = true;
            }
        }

        if (isDone && !isDone1)
        {
            if (Vector2.Distance(dragUI3.transform.position, handVoi.position) <= 0.25f)
            {
                imgVoi.sprite = spVoi2;
                imgVoi.SetNativeSize();
                dragUI3.SetActiveDragNew(false);
                dragUI3.gameObject.SetActive(false);
                vois.SetActive(false);
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
