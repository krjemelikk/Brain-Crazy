using UnityEngine;
using UnityEngine.UI;

public class Level_62 : BaseLevel
{
    [Header("Answers")]
    public Button theRock;
    public Button theCat;
    public Button theHammer;
    public Button theGrand;

    public Image dirtyImg;
    public Sprite[] dirtySprites;
    private int currentClear;
    private bool isChoiceObj;
    private float timer;

    protected override void Start()
    {
        base.Start();
        theRock.onClick.AddListener(() => WrongAnswer());
        theCat.onClick.AddListener(() => WrongAnswer());
        theHammer.onClick.AddListener(() => WrongAnswer());
        theGrand.onClick.AddListener(() => WrongAnswer());
        currentClear = 0;
        dirtyImg.sprite = dirtySprites[0];
        timer = 0.25f;
    }

    protected override void Update()
    {
        base.Update();
        ClearTheWindow();
    }

    Vector3 posDownMouse;
    Vector3 rememberPosDownMouse;
    public void ClearTheWindow()
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
        {
            Vector2 mouseDirection = posEnd - posStart;
            float dis = Vector2.Distance(posStart, posEnd);
            Ray2D ray = new Ray2D(posStart, mouseDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, dis);

            if (hit.collider != null && hit.collider.gameObject.name == "Dirty")
            {
                timer += Time.deltaTime;
                if (timer >= 0.2f)
                {
                    currentClear++;
                    if (currentClear >= 4)
                    {
                        dirtyImg.gameObject.SetActive(false);
                        RightAnswer();
                        return;
                    }
                    dirtyImg.sprite = dirtySprites[currentClear];
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
}
