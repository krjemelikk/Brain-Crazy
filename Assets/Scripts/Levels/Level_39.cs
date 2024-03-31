using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_39 : BaseLevel
{
    [Header("Answers")]
    public Button theChocolate;
    public Button theApple;
    public Button theBread;
    public Button theIce;
    public Button woodIce;

    private Image imgIce;


    Vector3 posDownMouse;
    Vector3 rememberPosDownMouse;
    private int currentClear;
    private bool isChoiceObj;
    private float timer;
    private bool isIceOver;
    private bool isDone;

    protected override void Start()
    {
        base.Start();
        theChocolate.onClick.AddListener(() => WrongAnswer());
        theApple.onClick.AddListener(() => WrongAnswer());
        theBread.onClick.AddListener(() => WrongAnswer());
        theIce.onClick.AddListener(() => WrongAnswer());
        woodIce.onClick.AddListener(() => CheckAnswer());
        imgIce = theIce.GetComponent<Image>();
    }

    protected override void Update()
    {
        base.Update();
        ClearCream();
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

    Vector3 touch_0, touch_1;
    private void OnPointDown()
    {
        touch_0 = Input.GetTouch(0).position;
    }

    private void OnPointUp()
    {
        touch_1 = Input.GetTouch(0).position;

        if(Vector3.Distance(touch_0,touch_1) >= 5f)
        {

        }
        imgIce.DOKill();
        imgIce.fillAmount = 0;
        imgIce.DOFillAmount(1, 0.5f).OnComplete(() => { RightAnswer(); });
    }

    public void ClearCream()
    {
        if (isChoiceObj)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            posDownMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posDownMouse.z = 0;
            rememberPosDownMouse = posDownMouse;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 posUpMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Dis " + Vector2.Distance(rememberPosDownMouse, posUpMouse));
            if (Vector2.Distance(rememberPosDownMouse, posUpMouse) > 0.1f)
            {
                posUpMouse.z = 0;
                //Debug.DrawLine(posDownMouse, posUpMouse, Color.black);

                Raycasting(posDownMouse, posUpMouse);
                rememberPosDownMouse = posUpMouse;
            }
        }
    }

    void Raycasting(Vector3 posStart, Vector3 posEnd)
    {
        if(!isDone)
        {
            Vector2 mouseDirection = posEnd - posStart;
            float dis = Vector2.Distance(posStart, posEnd);
            Ray2D ray = new Ray2D(posStart, mouseDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, dis);

            if (hit.collider != null && hit.collider.gameObject.name == "Creem")
            {
                timer += Time.deltaTime;
                if (timer >= 0.2f)
                {
                    currentClear++;
                    if (currentClear >= 4)
                    {
                        isIceOver = true;
                        theIce.gameObject.SetActive(false);
                        isDone = true;
                        //dirtyImg.gameObject.SetActive(false);
                        //RightAnswer();
                        return;
                    }

                    imgIce.color = new Color(1, 1, 1, imgIce.color.a - 0.25f);
                    //dirtyImg.sprite = dirtySprites[currentClear];
                    timer = 0;
                }
            }
        }
    }

    public void EndDragWrong(RectTransform tran)
    {
        isChoiceObj = false;
        if (tran.transform.localPosition != localPositionWrong)
        {
            WrongAnswer();
            tran.transform.localPosition = localPositionWrong;
        }
    }

    Vector3 localPositionWrong;

    public void BeginDragWrong(RectTransform tran)
    {
        isChoiceObj = true;
        localPositionWrong = tran.transform.localPosition;
    }

    public void CheckAnswer()
    {
        if(isIceOver)
        {
            RightAnswer();
        }
    }
}
