using UnityEngine;
using UnityEngine.UI;

public class Level_182 : BaseLevel
{
    public Button btStart;
    public RectTransform theRabbit;
    public RectTransform theTurtle;
    public RectTransform theHeadTurtle;
    public Image posEnd;
    public RectTransform panelRestart;
    public float speedRabbit;
    public float speedTurtle;

    private bool isEnd;
    private bool isDone;
    private bool isCanMove;

    public Image viewTurtle;
    public Sprite spTurtle;

    public RectTransform coRua;

    protected override void Start()
    {
        base.Start();
        btStart.onClick.AddListener(() => StartMove());
    }

    protected override void Update()
    {
        base.Update();

        if (isEnd || isDone)
            return;

        if (isCanMove)
        {
            theRabbit.Translate(Vector3.right * speedRabbit * Time.deltaTime);
            theTurtle.Translate(Vector3.right * speedTurtle * Time.deltaTime);
        }

        //var position = posEnd.transform.localPosition;
       // posEnd.transform.localPosition = new Vector3(position.x, Mathf.Clamp(position.y, -250f, 75f), position.z);

        CheckAnswer();


        if (isClickHead)
        {
            if (Input.mousePosition != inputMouse)
            {
                if (Input.mousePosition.x >= inputMouse.x)
                {
                    //Kéo dài đầu rùa
                    if (coRua.sizeDelta.x <= 870)
                        coRua.sizeDelta = new Vector2(coRua.sizeDelta.x + 5, coRua.sizeDelta.y);
                }
                else
                {
                    //Thu ngắn đầu rùa
                    if (coRua.sizeDelta.x >= 6)
                        coRua.sizeDelta = new Vector2(coRua.sizeDelta.x - 5, coRua.sizeDelta.y);
                }

                inputMouse = Input.mousePosition;
            }
        }

        if(coRua.sizeDelta.x >= 860)
        {
            isEnd = true;
            RightAnswer();
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
        GameController.Instance.ResetLevel();
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

        var distanceT = Mathf.Abs(theHeadTurtle.transform.position.x - posEnd.transform.position.x);
        var distanceR = Mathf.Abs(theRabbit.transform.position.x - posEnd.transform.position.x);
        if (!isDone)
        {
           if(distanceT <= 0.09f)
            {
                RightAnswer();
                isDone = true;
                return;

            }

           if(distanceR <= 0.09f)
            {
                WrongAnswer();
                isDone = true;
                return;

            }
        }
        else
        {
            //Vector3.Distance
        }
    }

    private void StartMove()
    {
        isEnd = false;
        isCanMove = true;
        panelRestart.gameObject.SetActive(false);
    }

    private bool isClickHead;
    private Vector3 inputMouse;
    public void OnclickHeadTurtle()
    {
        isClickHead = true;
        //inputMouse = Input.mousePosition;
    }


    public void PointerUpHead()
    {
        isClickHead = false;
    }

}