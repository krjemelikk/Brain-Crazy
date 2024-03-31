using UnityEngine;


public class Level_170 : BaseLevel
{
    [SerializeField] private DragUI sun;
    [SerializeField] private DragUI gach;
    [SerializeField] private Transform checkGachOut;
    [SerializeField] private Transform checkSun;

    [SerializeField] private GameObject chickenIdle;
    [SerializeField] private GameObject chickenWakeUp;

    private bool isEnd;
    private bool isGachOut;

    protected override void Start()
    {
        base.Start();
        sun.isCanActive = false;
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if(sun.transform.position.y >= checkSun.position.y)
        {
            isEnd = true;
            //sun.SetActiveDrag(false);

            RightAnswer();

            chickenIdle.gameObject.SetActive(false);
            chickenWakeUp.gameObject.SetActive(true);
           
        }
    }

    public void GachOutHanlde()
    {
        Debug.Log("DIS " + Vector2.Distance(gach.transform.position, checkGachOut.position));
        if (Vector2.Distance(gach.transform.position, checkGachOut.position) > 0.15f)
        {
            //gach.SetActiveDrag(false);
            isGachOut = true;
            sun.isCanActive = true;
        }
    }

    //public override void RightAnswer()
    //{
    //    WrongRightEffect.Instance.Right();

    //    _completeCountDown = Observable.Timer(TimeSpan.FromSeconds(2f))
    //        .Subscribe(_ => CompleteLevel())
    //        .AddTo(this);
    //}
}
