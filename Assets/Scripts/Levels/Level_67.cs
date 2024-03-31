using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Level_67 : BaseLevel
{
    [Header("Answers")]
    public Button theFan;
    public Button theWaterbucket;
    public Button theIceCream;

    public Image theBoy_Happy;
    public Image tickV;
    public RectTransform transformBoy;

    private int countItems = 0;
    private float maxY, minY;

    protected override void Start()
    {
        base.Start();
        theFan.onClick.AddListener(() => WrongAnswer());
        theIceCream.onClick.AddListener(() => WrongAnswer());
        theWaterbucket.onClick.AddListener(() => WrongAnswer());
        minY = transformBoy.localPosition.y - transformBoy.rect.height / 2;
        maxY = transformBoy.localPosition.y + transformBoy.rect.height / 2;
    }

    protected override void Update()
    {
        base.Update();
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (countItems >= 6)
        {
            theBoy_Happy.gameObject.SetActive(true);
            transformBoy.gameObject.SetActive(false);
            RightAnswer();
        }
    }

    Vector3 positionRight;
    public void BeginDrag(RectTransform tran)
    {
        positionRight = tran.transform.position;
    }

    public void EndDrag(RectTransform tran)
    {
        var distance = Vector3.Distance(tran.position, transformBoy.position);
        Debug.Log(distance);
        if (distance >= 0.25f)
        {
            var tick = Instantiate(tickV, tran);
            tick.gameObject.SetActive(true);
            countItems -= -1;
            tran.GetComponent<EventTrigger>().enabled = false;
            tran.gameObject.GetComponent<Image>().DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => { tick.gameObject.SetActive(false); });
        }
        else
        {
            tran.transform.position = positionRight;
        }

        CheckAnswer();
    }

    public void EndDragShoe(RectTransform tran)
    {
        var distance = Vector3.Distance(tran.position, transformBoy.position);
        Debug.Log(distance);
        if (distance >= 0.25f)
        {
            var tick = Instantiate(tickV, tran);
            tick.gameObject.SetActive(true);
            countItems -= -1;
            tran.GetComponent<EventTrigger>().enabled = false;
            tran.gameObject.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => { tick.gameObject.SetActive(false); });
            tran.gameObject.transform.GetChild(1).GetComponent<Image>().DOFade(0, 0.5f).SetUpdate(true);
        }
        else
        {
            tran.transform.position = positionRight;
        }

        CheckAnswer();
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