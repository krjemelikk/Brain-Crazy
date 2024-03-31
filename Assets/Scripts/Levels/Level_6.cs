using UnityEngine;
using UnityEngine.UI;

public class Level_6 : BaseLevel
{
    [Header("Answers")]
    public Button theSquare;
    public Button theTriangle;
    public Button theCircle;
    public Button theStar;
    public Button theCapsual;

    private RectTransform tranformTheSquare;

    protected override void Start()
    {
        base.Start();
        theSquare.onClick.AddListener(() => WrongAnswer());
        theTriangle.onClick.AddListener(() => WrongAnswer());
        theCircle.onClick.AddListener(() => WrongAnswer());
        theCapsual.onClick.AddListener(() => WrongAnswer());
        theStar.onClick.AddListener(() => WrongAnswer());
        tranformTheSquare = theSquare.GetComponent<RectTransform>();

    }

    protected override void Update()
    {
        base.Update();
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

    public void ChechAnswer()
    {
        if (tranformTheSquare.localPosition.x - tranformTheSquare.rect.width / 2 < GameController.Instance.HomeScene.BoundLeft.localPosition.x
            || tranformTheSquare.localPosition.x + tranformTheSquare.rect.width / 2 > GameController.Instance.HomeScene.BoundRight.localPosition.x
            || tranformTheSquare.localPosition.y + tranformTheSquare.rect.height / 2 > GameController.Instance.HomeScene.BoundTop.localPosition.y
            || tranformTheSquare.localPosition.y - tranformTheSquare.rect.height / 2 < GameController.Instance.HomeScene.BoundBottom.localPosition.y)
        {
            RightAnswer();
        }
    }
}
