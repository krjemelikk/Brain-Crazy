using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using DG.Tweening;

public class Level_3 : BaseLevel
{
    [Header("Answers")]
    public RectTransform car1;
    public RectTransform car2;
    public RectTransform car3;
    public RectTransform car4;
    public Button car0;

    public RectTransform[] moveCar1;
    public RectTransform[] moveCar2;
    public RectTransform[] moveCar3;
    public RectTransform[] moveCar4;

    protected override void Start()
    {
        base.Start();
        car1.gameObject.GetComponent<Button>().onClick.AddListener(() => WrongAnswer());
        car2.gameObject.GetComponent<Button>().onClick.AddListener(() => WrongAnswer());
        car3.gameObject.GetComponent<Button>().onClick.AddListener(() => WrongAnswer());
        car4.gameObject.GetComponent<Button>().onClick.AddListener(() => WrongAnswer());
        car0.onClick.AddListener(() => RightAnswer());
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StartLevel()
    {
        base.StartLevel();
        Move_Car_1();
        Move_Car_2();
        Move_Car_3();
        Move_Car_4();

        Observable.Interval(TimeSpan.FromSeconds(5f)).Where( isPlaying => GameController.Instance.stateGame == StateGame.PLAYING).Subscribe(_ =>
                               {
                                   GameController.Instance.ShowTutorial(car0.GetComponent<RectTransform>());
                               }).AddTo(this);
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

    private void Move_Car_1()
    {
        car1.DOLocalMove(moveCar1[1].localPosition, 2f).OnComplete(
            () =>
            {
                car1.localScale = new Vector3(-car1.localScale.x, car1.localScale.y, car1.localScale.z);
                Turn_Car_1();
            });
    }
    
    private void Turn_Car_1()
    {
        car1.DOLocalMove(moveCar1[0].localPosition, 2f).OnComplete(
            () =>
            {
                car1.localScale = new Vector3(-car1.localScale.x, car1.localScale.y, car1.localScale.z);
                Move_Car_1();
            });
    }

    private void Move_Car_2()
    {
        car2.DOLocalMove(moveCar2[1].localPosition, 3f).OnComplete(
            () =>
            {
                car2.localScale = new Vector3(-car2.localScale.x, car2.localScale.y, car2.localScale.z);
                Turn_Car_2();
            });
    }

    private void Turn_Car_2()
    {
        car2.DOLocalMove(moveCar2[0].localPosition, 3f).OnComplete(
            () =>
            {
                car2.localScale = new Vector3(-car2.localScale.x, car2.localScale.y, car2.localScale.z);
                Move_Car_2();
            });
    }

    private void Move_Car_3()
    {
        car3.DOLocalMove(moveCar3[1].localPosition, 3f).OnComplete(
            () =>
            {
                car3.localScale = new Vector3(-car3.localScale.x, car3.localScale.y, car3.localScale.z);
                Turn_Car_3();
            });
    }

    private void Turn_Car_3()
    {
        car3.DOLocalMove(moveCar3[0].localPosition, 3f).OnComplete(
            () =>
            {
                car3.localScale = new Vector3(-car3.localScale.x, car3.localScale.y, car3.localScale.z);
                Move_Car_3();
            });
    }

    private void Move_Car_4()
    {
        car4.DOLocalMove(moveCar4[1].localPosition, 4f).OnComplete(
            () =>
            {
                car4.localScale = new Vector3(-car4.localScale.x, car4.localScale.y, car4.localScale.z);
                Turn_Car_4();
            });
    }

    private void Turn_Car_4()
    {
        car4.DOLocalMove(moveCar4[0].localPosition, 4f).OnComplete(
            () =>
            {
                car4.localScale = new Vector3(-car4.localScale.x, car4.localScale.y, car4.localScale.z);
                Move_Car_4();
            });
    }
}
