using UnityEngine;
using DG.Tweening;

public class Level_219 : BaseLevel
{

    private bool isEnterRightBtn;
    private bool isEnterLeftBtn;
    private bool isEnd;

    [SerializeField] private Rigidbody2D monkeyRigi;
    [SerializeField] private Transform legMonkey;
    [SerializeField] private Transform posEnd;
    [SerializeField] private Transform posGround;
    [SerializeField] private Transform limitLeft;
    [SerializeField] private Transform limitRight;

    private bool isJump;
    [SerializeField] private DragUI TextDrag;
    [SerializeField] private Transform posEndText;
    [SerializeField] private Transform posFinishText;
    private bool isMonkeyMoveOnText;

    protected override void Update()
    {
        if (isEnd)
            return;

        if (isMonkeyMoveOnText)
            return;

        base.Update();

        if (isEnterRightBtn)
        {
            ControllMonkeyRight();
        }
        if (isEnterLeftBtn)
        {
            ControllMonkeyLeft();
        }

        if (Vector2.Distance(monkeyRigi.transform.position, posEnd.position) <= 0.2f)
        {
            RightAnswer();
            isEnd = true;
        }

        if(Mathf.Abs(TextDrag.transform.position.y - posFinishText.position.y) <= 0.2f)
        {
            isMonkeyMoveOnText = true;
            monkeyRigi.DOMoveX(TextDrag.transform.position.x, 0.5f).OnComplete(
                () => 
                {
                    TextDrag.transform.DOMoveY(posEndText.transform.position.y, 0.5f).OnComplete(() => { isMonkeyMoveOnText = false; });
                });
        }

        //if (Mathf.Abs(legMonkey.transform.position.y - posGround.position.y) <= 0.2f)
        //{
        //    isJump = false;
        //}
        //else
        //{
        //    isJump = true;
        //}
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
        if (Mathf.Abs(legMonkey.transform.position.y - posGround.position.y) <= 0.2f)
        {
            monkeyRigi.AddForce(130f * Vector2.up);
        }
    }
}
