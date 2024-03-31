using UnityEngine;
using UnityEngine.UI;
public class Level_117 : BaseLevel
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
    public float disRight = 1f;
    public RectTransform theMu;

    private bool isCanMoveTurtle;
    private bool isEnd = true;
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

        if (isCanMoveTurtle)
        {
            theRabbit.Translate(Vector3.right * speedRabbit * Time.deltaTime);
            theTurtle.Translate(Vector3.right * speedTurtle * Time.deltaTime);
        }
        else
        {
            theRabbit.Translate(Vector3.right * speedRabbit * Time.deltaTime);
            theTurtle.Translate(Vector3.right * (speedTurtle + speedRabbit * 2f) * Time.deltaTime);
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
        isCanMoveTurtle = true;
        if (Vector3.Distance(theMu.position, theTurtle.position) > disRight)
        {
            theMu.SetParent(null);
            isCanMoveTurtle = false;
        }
        isEnd = false;
        panelRestart.gameObject.SetActive(false);
    }
}
