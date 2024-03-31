using UnityEngine;

public class Level_221 : BaseLevel
{
    private bool isEnterRightBtn;
    private bool isEnterLeftBtn;
    private bool isEnd;

    [SerializeField] private Rigidbody2D monkeyRigi;
    [SerializeField] private Transform headMonkey;
    [SerializeField] private Transform legMonkey;
    [SerializeField] private Transform posEnd;
    [SerializeField] private Transform posGround;
    [SerializeField] private Transform limitLeft;
    [SerializeField] private Transform limitRight;
    [SerializeField] private ZoomObject ZoomMonkey;

    protected override void Update()
    {
        if (isEnd)
            return;
        
        base.Update();

        if (ZoomMonkey.isZooming)
        {
            monkeyRigi.gravityScale = 0;
        }
        else
        {
            monkeyRigi.gravityScale = 1;
        }

            if (!ZoomMonkey.isZooming)
        {
            if (isEnterRightBtn)
            {
                ControllMonkeyRight();
            }
            if (isEnterLeftBtn)
            {
                ControllMonkeyLeft();
            }

            if (Vector2.Distance(headMonkey.transform.position, posEnd.position) <= 0.2f)
            {
                RightAnswer();
                isEnd = true;
            }
        }

        
    }

    public void OnEnterLeft(bool isEnter)
    {
        isEnterLeftBtn = isEnter;
    }

    public void OnEnterRight(bool isEnter)
    {
        isEnterRightBtn = isEnter;
    }

    private void ControllMonkeyRight()
    {
        float moveX = monkeyRigi.transform.position.x + 0.01f;
        moveX = Mathf.Clamp(moveX, limitLeft.position.x, limitRight.position.x);

        monkeyRigi.transform.position = new Vector3(moveX, monkeyRigi.transform.position.y, monkeyRigi.transform.position.z);
    }
    private void ControllMonkeyLeft()
    {
        float moveX = monkeyRigi.transform.position.x - 0.01f;
        moveX = Mathf.Clamp(moveX, limitLeft.position.x, limitRight.position.x);
        Debug.Log("moveX " + moveX);

        monkeyRigi.transform.position = new Vector3(moveX, monkeyRigi.transform.position.y, monkeyRigi.transform.position.z);
    }

    public void JumpMonkey()
    {
        if (ZoomMonkey.isZooming)
            return;

            Debug.Log("Jump " );
        if (Mathf.Abs(legMonkey.transform.position.y - posGround.position.y) <= 0.2f)
        {
            Debug.Log("Jump AAA");
            monkeyRigi.AddForce(130f * Vector2.up);
        }
    }
}
