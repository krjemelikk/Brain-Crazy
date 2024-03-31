using UnityEngine;
using UnityEngine.UI;

public class Level_177 : BaseLevel
{
    public Button btStart;
    public RectTransform theRabbit;
    public RectTransform theTurtle;
    public RectTransform posEnd;
    public RectTransform posStartRabbit;
    public RectTransform posStartTurtle;
    public RectTransform panelRestart;
    public Image imgTrap;
    public Sprite[] sprTraps;
    public float speedRabbit;
    public float speedTurtle;

    private bool isEnd;
    private bool isCanMoveRabbit, isCanMoveTurtle;
    private int countTrap = 0;

    protected override void Start()
    {
        base.Start();
        btStart.onClick.AddListener(() => StartMove());
        imgTrap.GetComponent<Button>().onClick.AddListener(() => OnClickTrap());
        imgTrap.sprite = sprTraps[countTrap];
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
        countTrap = 0;
        imgTrap.sprite = sprTraps[countTrap];
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
        if (countTrap >= 3 && theRabbit.gameObject.activeInHierarchy)
        {
            var distance = Vector2.Distance(theRabbit.transform.position,imgTrap.transform.position);
            if (distance <= 0.15f)
            {
                isCanMoveRabbit = false;
                theRabbit.gameObject.SetActive(false);
            }
        }

        if (theRabbit.gameObject.activeInHierarchy && theRabbit.localPosition.x >= posEnd.localPosition.x)
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
        isEnd = false;
        isCanMoveRabbit = true;
        isCanMoveTurtle = true;
        panelRestart.gameObject.SetActive(false);
    }

    private void OnClickTrap()
    {
        countTrap++;
        if (countTrap >= sprTraps.Length)
            return;
        imgTrap.sprite = sprTraps[countTrap];
    }
}