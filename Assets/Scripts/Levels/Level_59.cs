using UnityEngine;
using UnityEngine.UI;

public class Level_59 : BaseLevel
{
    [Header("Answers")]
    public Button thePig;
    public Button theRock;
    public Button theClock;
    public Button theBanana;

    public Image imgSleepy;
    public Image imgAngry;
    public Image imgTimeLine;

    public Sprite Pig_angry;
    public Sprite Pig_sleepy;

    private bool isEnd = false;
    private float timerHold;
    private float timeNeedHold;

    private bool isHold = false;

    protected override void Start()
    {
        base.Start();
        theRock.onClick.AddListener(() => WrongAnswer());
        theClock.onClick.AddListener(() => WrongAnswer());
        theBanana.onClick.AddListener(() => WrongAnswer());
        thePig.onClick.AddListener(() => WrongAnswer());

        thePig.GetComponent<Image>().sprite = Pig_sleepy;
        imgSleepy.gameObject.SetActive(true);
        imgAngry.gameObject.SetActive(false);
        timeNeedHold = 3f;
        timerHold = 0;
    }

    protected override void Update()
    {
        base.Update();

        if (isEnd) return;

        if (isHold)
        {
            if (timerHold >= timeNeedHold)
            {
                timerHold = 0;
                imgTimeLine.gameObject.SetActive(false);
                RightAnswer();
            }
            else
            {
                timerHold += Time.deltaTime;
                imgTimeLine.fillAmount = timerHold / timeNeedHold;
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
    }

    public override void RightAnswer()
    {
        isEnd = true;

        thePig.GetComponent<Image>().sprite = Pig_angry;
        imgSleepy.gameObject.SetActive(false);
        imgAngry.gameObject.SetActive(true);

        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnPointDown()
    {
        isHold = true;
    }

    public void OnPointUp()
    {
        isHold = false;
        timerHold = 0;
        imgTimeLine.fillAmount = timerHold / timeNeedHold;
    }

    public void EndDragWrong(RectTransform tran)
    {
        if (tran.transform.localPosition != localPositionWrong)
        {
            WrongAnswer();
            tran.transform.localPosition = localPositionWrong;
        }
    }

    Vector3 localPositionWrong;

    public void BeginDragWrong(RectTransform tran)
    {
        localPositionWrong = tran.transform.localPosition;
    }
}