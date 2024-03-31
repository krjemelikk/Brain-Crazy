using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_178 : BaseLevel
{
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;

    [SerializeField] private DragUI monkeyObj;
    private Transform iconMonkey;
    [SerializeField] private Rigidbody2D monkeyRigi;

    [SerializeField] private Level_178_Point[] points;
    [SerializeField] private int[] pointsIndexRerence;
    private int currentPoint;
    [SerializeField] private Transform finishObj;
    private bool isEnd;

    [SerializeField] private Transform Map;

    [SerializeField] private Transform posMonkeyLeft;
    [SerializeField] private Transform posMonkeyRight;

    [SerializeField] private Transform snakeTrans;

    private bool isCanMoveMap;
    private bool isTroop;

    protected override void Start()
    {
        base.Start();
        monkeyObj.isCanActive = false;
        monkeyRigi.gravityScale = 0;

        leftBtn.onClick.RemoveAllListeners();
        leftBtn.onClick.AddListener(TabLeft);

        rightBtn.onClick.RemoveAllListeners();
        rightBtn.onClick.AddListener(TabRight);

        currentPoint = -1;

        iconMonkey = monkeyObj.gameObject.transform.GetChild(0);
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();
        if (Vector2.Distance(finishObj.position, monkeyObj.transform.position) <= 0.2f)
        {
            RightAnswer();
            isEnd = true;
        }

        if(!isTroop)
        {
            MoveMap();
        }
    }

    private void TabLeft()
    {
        if (isEnd || isCanMoveMap)
            return;
        if (currentPoint  >= points.Length - 1)
        {
            //Jump Fail
            JumpFail();
            //monkeyObj.gameObject.transform.DOMove(snakeTrans.position, 0.1f).OnComplete(() => {
            //    WrongAnswer();
            //    GameController.Instance.ResetLevel();
            //});
            //isEnd = true;
            return;
        }
        if (pointsIndexRerence[currentPoint + 1] == 0)
        {
            //Jump
            monkeyObj.gameObject.transform.DOMove(posMonkeyLeft.position, 0.1f)
                .OnComplete(() => { iconMonkey.transform.localScale = new Vector3(1, iconMonkey.transform.localScale.y, iconMonkey.transform.localScale.z); }); ;

            currentPoint++;
            if (currentPoint == points.Length - 1)
            {
                monkeyObj.isCanActive = true;
               // isTroop = true;
            }
           
            isCanMoveMap = true;
        }
        else
        {
            JumpFail();
        }
    }

    private void TabRight()
    {
        if (isEnd || isCanMoveMap)
            return;
        if (currentPoint  >= points.Length - 1)
        {
            //Jump Fail
            monkeyObj.gameObject.transform.DOMove(snakeTrans.position, 0.1f).OnComplete(() => {
                StartCoroutine(Helper.StartAction(() =>
                {
                    WrongAnswer();
                    GameController.Instance.ResetLevel();
                }, 0.5f));
            });
            isEnd = true;
            return;
        }
        if (pointsIndexRerence[currentPoint + 1] == 1)
        {
            //Jump
            
            monkeyObj.gameObject.transform.DOMove(posMonkeyRight.position, 0.1f)
                .OnComplete(()=> { iconMonkey.transform.localScale = new Vector3(-1, iconMonkey.transform.localScale.y, iconMonkey.transform.localScale.z); });

            currentPoint++;
            if (currentPoint == points.Length - 1)
            {
                monkeyObj.isCanActive = true;
                //isTroop = true;
            } 
            isCanMoveMap = true;
        }
        else
        {
            JumpFail();
        }
    }

    private void JumpFail()
    {
        isEnd = true;
        float posMoveY = monkeyObj.gameObject.transform.position.y;
        monkeyObj.gameObject.transform.DOMoveY(posMoveY + 0.5f, 0.5f).OnComplete(() =>
        {
            monkeyRigi.gravityScale = 1;
            StartCoroutine(Helper.StartAction(() =>
            {
                WrongAnswer();
                GameController.Instance.ResetLevel();
            }, 0.5f));

        });
    }

    private void MoveMap()
    {
        if (!isCanMoveMap)
            return;
        Map.transform.Translate(Vector2.down * 5f * Time.deltaTime);
        if(points[currentPoint].transform.position.y <= monkeyObj.transform.position.y)
        {
            monkeyObj.transform.position = new Vector3(monkeyObj.transform.position.x, points[currentPoint].transform.position.y, monkeyObj.transform.position.z);
            isCanMoveMap = false;
        }
        
    }
}
