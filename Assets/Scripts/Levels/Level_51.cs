using UnityEngine;
using UnityEngine.UI;
public class Level_51 : BaseLevel
{
    public Button btStart;
    public RectTransform theRabbit;
    public RectTransform theTurtle;
    public RectTransform posEnd;
    public RectTransform posStartRabbit;
    public RectTransform posStartTurtle;
    public RectTransform panelRestart;
    public float speedRabbit;
    public float speedTurtle;

    private bool isCanMoveRabbit, isCanMoveTurtle;
    private bool isEnd;
    protected override void Start()
    {
        base.Start();
        btStart.onClick.AddListener(() => StartMove());
    }

    protected override void Update()
    {
        base.Update();

        if (isEnd)
            return;

        if (isCanMoveRabbit)
            theRabbit.Translate(Vector3.right * speedRabbit * Time.deltaTime);

        if (isCanMoveTurtle)
        {
            theTurtle.Translate(Vector3.right * speedTurtle * Time.deltaTime);
        }

        CheckAnswer();
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
        panelRestart.gameObject.SetActive(true);
        theRabbit.transform.localPosition = posStartRabbit.transform.localPosition;
        theTurtle.transform.localPosition = posStartTurtle.transform.localPosition;
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (theRabbit.localPosition.x >= posEnd.localPosition.x)
        {
            isEnd = true;
            WrongAnswer();
            return;
        }

        if (theTurtle.localPosition.x >= posEnd.localPosition.x)
        {
            isEnd = true;
            RightAnswer();
        }

    }

    private void StartMove()
    {
        isCanMoveRabbit = true;
        isCanMoveTurtle = true;
        isEnd = false;
        panelRestart.gameObject.SetActive(false);
    }

    public void OnPointerDown()
    {
        isCanMoveRabbit = false;
    }

    public void OnPointerUp()
    {
        isCanMoveRabbit = true;
    }
}
