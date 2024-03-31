using UnityEngine;
using UnityEngine.UI;

public class Level_235 : BaseLevel
{
    public Image[] viewNumber;
    public Sprite[] spNumber;
    public int countAdd = 0;
    public int sum = 0;
    private bool isSwap = false;
    private int rememberIndex9 = -1;


    protected override void Start()
    {
        base.Start();
        rememberIndex9 = -1;
    }

    protected override void Update()
    {
        base.Update();
        RotationClock();
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

    public void SetInt(int number)
    {
        if (countAdd >= 3)
            return;

        if (number == 9)
        {
            if (isSwap)
            {
                sum += 6;
                rememberIndex9 = countAdd;
            }
            else
                sum += 9;
        }
        else
            sum += number;


        if (number == 1)
        {
            viewNumber[countAdd].sprite = spNumber[0];
        }
        else if (number == 3)
        {
            viewNumber[countAdd].sprite = spNumber[1];
        }
        else if (number == 5)
        {
            viewNumber[countAdd].sprite = spNumber[2];
        }
        else if (number == 7)
        {
            viewNumber[countAdd].sprite = spNumber[3];
        }
        else if (number == 9)
        {
            viewNumber[countAdd].sprite = spNumber[4];
            if (isSwap)
                viewNumber[countAdd].transform.localScale = new Vector3(-1, -1, 1);
            else
                viewNumber[countAdd].transform.localScale = new Vector3(1, 1, 1);
        }
        else if (number == 11)
        {
            viewNumber[countAdd].sprite = spNumber[5];
        }
        else if (number == 13)
        {
            viewNumber[countAdd].sprite = spNumber[6];
        }
        else if (number == 15)
        {
            viewNumber[countAdd].sprite = spNumber[7];
        }

        countAdd++;

        if (countAdd >= 3)
        {
            if (sum == 30)
                RightAnswer();
            else
            {
               StartCoroutine(Helper.StartAction(()=>  WrongAnswer(), 0.5f));
            }
        }
    }

    #region Quay
    private Vector2 pointA;
    private Vector2 pointB;
    private bool touchStart = false;

   [SerializeField] private GameObject number9;

    private bool isTouchkimdai;

    public void TouchKimDai()
    {
        isTouchkimdai = true;
    }

    private void RotationClock()
    {
        if (isSwap)
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
            if (pointB == pointA)
                return;
            Helper.LookAtToDirection(direction, number9, 500);
            Debug.Log("Rotate " + number9.transform.eulerAngles.z);
            if (number9.transform.eulerAngles.z <= 200 && number9.transform.eulerAngles.z >= 180)
            {
                isSwap = true;
                SetInt(9);
            }
        }

    }
    #endregion
}
