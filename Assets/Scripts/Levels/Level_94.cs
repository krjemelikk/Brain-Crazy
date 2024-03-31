using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Level_94 : BaseLevel
{
    [Header("Answers")]
    public Button theCurtain_Left;
    public Button theCurtain_Right;
    public Button thePrince;
    public Button thePrincess;
    public Button theSpeaker;
    public Button theRock;

    public Image thePrincess_Getup;
    private RectTransform rectCurtain_Left;
    private RectTransform rectCurtain_Right;
    private int countSwipe;
    private bool isOnDrag;

    public GameObject nightObj;
    public GameObject sunObj;

    private bool isRight;

    protected override void Start()
    {
        base.Start();
        theCurtain_Left.onClick.AddListener(() => WrongAnswer());
        theCurtain_Right.onClick.AddListener(() => WrongAnswer());
        thePrince.onClick.AddListener(() => WrongAnswer());
        thePrincess.onClick.AddListener(() => WrongAnswer());
        theSpeaker.onClick.AddListener(() => WrongAnswer());
        theRock.onClick.AddListener(() => WrongAnswer());

        rectCurtain_Left = theCurtain_Left.GetComponent<RectTransform>();
        rectCurtain_Right = theCurtain_Right.GetComponent<RectTransform>();
        countSwipe = 0;
    }

    protected override void Update()
    {
        RotationClock();
        base.Update();
        if (isOnDrag)
        {
            rectCurtain_Right.localPosition = new Vector3(Mathf.Clamp(rectCurtain_Right.localPosition.x, 230, 330), rectCurtain_Right.localPosition.y, rectCurtain_Right.localPosition.z);
            rectCurtain_Left.localPosition = new Vector3(Mathf.Clamp(rectCurtain_Left.localPosition.x, -330, -210), rectCurtain_Left.localPosition.y, rectCurtain_Left.localPosition.z);
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (isRight)
            return;

        nightObj.SetActive(false);

        rectCurtain_Left.transform.DOLocalMoveX(-433, 0.5f);
        rectCurtain_Right.transform.DOLocalMoveX(433, 0.5f);

        sunObj.transform.DOLocalMoveY(100, 0.45f).OnComplete(()=> {
            thePrincess.gameObject.SetActive(false);
            thePrincess_Getup.gameObject.SetActive(true);
            StartCoroutine(Helper.StartAction(RightAnswer, 0.2f));
           });
    }

    public void OnDrag(bool isLeft)
    {
        isOnDrag = true;
        //if (isLeft)
        //{
        //    Mathf.Clamp(rectCurtain_Left.localPosition.x, -250, -350);
        //    //rectCurtain_Left.localPosition = new Vector3(Mathf.Clamp(rectCurtain_Left.localPosition.x, -250, -350), rectCurtain_Left.localPosition.y, rectCurtain_Left.localPosition.z);
        //}
        //else
        //{
        //    Mathf.Clamp(rectCurtain_Right.localPosition.x, 270, 350);
        //    //rectCurtain_Right.localPosition = new Vector3(Mathf.Clamp(rectCurtain_Right.localPosition.x, 270, 350), rectCurtain_Right.localPosition.y, rectCurtain_Right.localPosition.z);
        //}
    }

    public void OnEndDrag(bool isLeft)
    {
        isOnDrag = false;

        if (isLeft)
        {
            if (rectCurtain_Left.localPosition.x <= -320)
            {
                countSwipe -= -1;
                rectCurtain_Left.gameObject.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            if (rectCurtain_Right.localPosition.x >= 320)
            {
                countSwipe -= -1;
                rectCurtain_Right.gameObject.GetComponent<EventTrigger>().enabled = false;
            }
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


    #region Quay Kim đồng hồ
    private Vector2 pointA;
    private Vector2 pointB;
    private bool touchStart = false;
    private Vector2 RememberPos;
    public Transform center;
    public GameObject kimDai;
    public GameObject kimNgan;
    private bool isTouchkimdai;

    public void TouchKimDai()
    {
        isTouchkimdai = true;
    }

    private void RotationClock()
    {
        if (isRight)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }

        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouchkimdai = false;
        }

        if (!isTouchkimdai)
            return;

        if (touchStart)
        {
            Vector2 offset = (pointB - pointA).normalized;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f).normalized;
            Vector2 a = new Vector2(center.position.x + direction.x, center.position.y + direction.y) * -1;
            if (pointB == pointA)
                return;

            if (a != RememberPos)
            {
                Helper.LookAtToDirection(direction, kimDai, 500);
                kimNgan.transform.eulerAngles = new Vector3(kimNgan.transform.eulerAngles.x, kimNgan.transform.eulerAngles.y, kimNgan.transform.eulerAngles.z - 1.3f);
                if (kimNgan.transform.eulerAngles.z <= 120)//8h sáng
                {
                    CheckAnswer();
                    isRight = true;
                }
                RememberPos = a;
            }

        }
    }
    #endregion
}
