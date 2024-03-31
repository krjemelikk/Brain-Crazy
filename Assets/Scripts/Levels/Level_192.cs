using UnityEngine;

public class Level_192 : BaseLevel
{
    public GameObject mouseScale;
    public float minMouse = 1f;
    public ZoomObject zoomObject;

    public Transform tfCheckDone;
    public Transform tfCheckWrong;

    public DragUI dragMouse;

    private bool isDone;
    private bool isEnd;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
            isDone = true;
        }, () => mouseScale.transform.localScale.x <= minMouse));
    }

    protected override void Update()
    {
        base.Update();

        if (isDone && !isEnd)
        {
            if(mouseScale.transform.position.x >= tfCheckDone.position.x)
            {
                isEnd = true;
                dragMouse.SetActiveDrag(false);
                RightAnswer();
            }
        }

        if(!isDone && !isEnd)
        {
            if (mouseScale.transform.position.x >= tfCheckWrong.position.x)
            {
                isEnd = true;
                dragMouse.SetActiveDrag(false);
                WrongAnswer();
            }
        }
    }

    public void CheckWrong()
    {
        //if (!isEnd)
        //{
        //    WrongAnswer();
        //}
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
}
