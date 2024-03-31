using UnityEngine;
using UnityEngine.UI;

public class Level_265 : BaseLevel
{
    public DragUI dragUI1;

    public Image imgEch;
    public Sprite spEch1;
    public Sprite spEch2;

    public Image imgPlayer;
    public Sprite spPlayer;

    public Image imgOnclick;

    public Transform tfEnd;

    private bool isDone;
    private bool isDone1;
    private int count = 0;

    public void OnEch()
    {
        if (isDone) return;
        count++;
        if(count >= 3)
        {
            imgEch.sprite = spEch1;
            imgEch.SetNativeSize();

            dragUI1.SetActiveDragNew(true);
            imgOnclick.raycastTarget = false;
            isDone = true;
        }
    }

    protected override void Start()
    {
        base.Start();
        dragUI1.SetActiveDragNew(false);
    }

    protected override void Update()
    {
        base.Update();

        if (isDone && !isDone1)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfEnd.position) <= 0.25f)
            {
                dragUI1.SetActiveDragNew(false);
                dragUI1.transform.position = tfEnd.position;

                imgPlayer.sprite = spPlayer;
                imgPlayer.SetNativeSize();

                imgEch.sprite = spEch2;
                imgEch.SetNativeSize();

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
