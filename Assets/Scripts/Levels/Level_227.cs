using UnityEngine;
using UnityEngine.UI;

public class Level_227 : BaseLevel
{
    public DragUI dragUI1;

    public Transform tfCheck;

    private bool isDone1;
    private bool isDone2;

    public Image view1;
    public Sprite sp1;

    public GameObject fire;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone1)
        {
            if (dragUI1.transform.localPosition.x<= -350f)
            {
                view1.sprite = sp1;
                isDone1 = true;
            }
        }

        if (isDone1 && !isDone2)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfCheck.position) <= 0.25f)
            {
                fire.gameObject.SetActive(true);
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
