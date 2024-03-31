using UnityEngine;
using UnityEngine.UI;

public class Level_15 : BaseLevel
{
    [Header("Answers")]
    public Button Pasta;
    public Button Plate;
    public Button Clock;
    public Button Brush;

    private RectTransform thePasta;
    private RectTransform thePlate;

    protected override void Start()
    {
        base.Start();
        Pasta.onClick.AddListener(() => WrongAnswer());
        Plate.onClick.AddListener(() => WrongAnswer());
        Clock.onClick.AddListener(() => WrongAnswer());
        Brush.onClick.AddListener(() => WrongAnswer());
        thePasta = Pasta.GetComponent<RectTransform>();
        thePlate = Plate.GetComponent<RectTransform>();
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

    public void CheckAnswer()
    {
        var maxX = thePlate.transform.localPosition.x + thePlate.rect.width / 2;
        var minX = thePlate.transform.localPosition.x - thePlate.rect.width / 2;
        var minY = thePlate.transform.localPosition.y - thePlate.rect.height / 2;
        var maxY = thePlate.transform.localPosition.y + thePlate.rect.height / 2;

        if (thePasta.transform.localPosition.x < maxX
            && thePasta.transform.localPosition.x > minX
            && thePasta.transform.localPosition.y > minY
            && thePasta.transform.localPosition.y < maxY)
        {
            thePasta.localPosition = thePlate.localPosition;
            RightAnswer();
        }
    }
}
