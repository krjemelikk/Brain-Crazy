using UnityEngine;
using UnityEngine.UI;

public class Level_63 : BaseLevel
{
    [Header("Answers")]
    public Button theGirl;
    public Button theMachine;
    public Button theLight;
    public Button theFire;
    public Button theVali;

    public Sprite Vali_Open;
    public Sprite Vali_Close;

    public Transform transformHair;
    private Transform transformVali;
    private Vector3 posStartHair;

    protected override void Start()
    {
        base.Start();
        theGirl.onClick.AddListener(() => WrongAnswer());
        theMachine.onClick.AddListener(() => WrongAnswer());
        theLight.onClick.AddListener(() => WrongAnswer());
        theFire.onClick.AddListener(() => WrongAnswer());
        theVali.onClick.AddListener(() => WrongAnswer());

        theVali.GetComponent<Image>().sprite = Vali_Open;
        transformVali = theVali.transform;
        posStartHair = transformHair.position;
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
        transformHair.gameObject.SetActive(false);
        theVali.GetComponent<Image>().sprite = Vali_Close;

        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void EndDrag()
    {
        var distance = Vector2.Distance(transformHair.position, transformVali.position);
        Debug.Log(distance);
        if (distance <= 0.25f)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
            transformHair.position = posStartHair;
        }
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