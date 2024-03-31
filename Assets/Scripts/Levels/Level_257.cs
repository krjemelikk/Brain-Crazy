using UnityEngine;
using UnityEngine.UI;

public class Level_257 : BaseLevel
{
    public Transform posEnd;

    public DragUI dragUI1;

    public bool isDone;

    public Image imgTV;
    public Sprite spTV;
    public Image imgPlayer;
    public Sprite spPlayer;

    protected override void Start()
    {
        dragUI1.SetActiveDragNew(false);
        base.Start();
    }

    public void OnTV()
    {
        if (isDone) return;
        dragUI1.SetActiveDragNew(true);
        imgTV.sprite = spTV;
        imgPlayer.sprite = spPlayer;
        isDone = true;
    }

    protected override void Update()
    {
        base.Update();
        if (isDone)
        {
            if (Vector2.Distance(dragUI1.transform.position, posEnd.position) <= 0.25f && dragUI1.isCanActive)
            {
                dragUI1.transform.position = posEnd.position;
                RightAnswer();
                dragUI1.SetActiveDragNew(false);
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
