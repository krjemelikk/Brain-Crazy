using UnityEngine;
using UnityEngine.UI;

public class Level_232 : BaseLevel
{
    public DragUI dragUI1;
    public DragUI dragUI2;
    public DragUI dragUI3;

    private bool isDone1;
    private bool isDone2;
    private bool isDone3;

    public Image viewBaby;
    public Image viewDone;

    public Sprite spBaby;
    public Sprite spDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone1 && dragUI2.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI2.transform.position) <= 0.5f)
            {
                dragUI3.SetActiveDrag(false);
                dragUI2.SetActiveDrag(false);
                dragUI1.GetComponent<Image>().raycastTarget = true;

                dragUI2.gameObject.SetActive(false);
                viewBaby.sprite = spBaby;
                viewBaby.SetNativeSize();

                isDone1 = true;
            }
        }

        if (isDone1 && !isDone2)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI3.transform.position) <= 0.5f)
            {
                dragUI1.gameObject.SetActive(false);
                viewDone.sprite = spDone;
                viewDone.SetNativeSize();
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
