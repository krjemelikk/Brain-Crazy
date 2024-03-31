using UnityEngine;
using UnityEngine.UI;

public class Level_179 : BaseLevel
{
    public Button btStart;
    public RectTransform theRabbit;
    public RectTransform theTurtle;
    public Image posEnd;
    public RectTransform posStartRabbit;
    public RectTransform posStartTurtle;
    public RectTransform panelRestart;
    public float speedRabbit;
    public float speedTurtle;

    private bool isEnd, isCheckEnd;
    private bool isCanMoveRabbit, isCanMoveTurtle;

    public Transform posEnd_Top;

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
            theTurtle.Translate(Vector3.right * speedTurtle * Time.deltaTime);
        
        var position = posEnd.transform.localPosition;
        posEnd.transform.localPosition = new Vector3(position.x,Mathf.Clamp(position.y, -250f, 75f), position.z);

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
        GameController.Instance.ResetLevel();
        //panelRestart.gameObject.SetActive(true);
        //theRabbit.transform.localPosition = posStartRabbit.transform.localPosition;
        //theTurtle.transform.localPosition = posStartTurtle.transform.localPosition;
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

        var distance = Mathf.Abs(theTurtle.transform.position.x - posEnd.transform.position.x);
        var distance_2 = Mathf.Abs(theRabbit.transform.position.x - posEnd.transform.position.x);
        Debug.Log("distance_2 " + distance_2);
        Debug.Log("distance " + distance);

        if (posEnd_Top.position.y >= theRabbit.transform.position.y)
        {
            if (distance_2 <= 0.1f)
            {
                // isEnd = true;
                WrongAnswer();
                return;
            }
        }
        else
        {
            if (distance <= 0.1f)
            {
                isEnd = true;
                RightAnswer();
            }
        }
    }

    private void StartMove()
    {
        isEnd = false;
        isCanMoveRabbit = true;
        isCanMoveTurtle = true;
        panelRestart.gameObject.SetActive(false);
    }

    public void OnBeginDrag()
    {
        isCheckEnd = true;
    }

    public void OnEndDrag()
    {
        isCheckEnd = false;
    }
}