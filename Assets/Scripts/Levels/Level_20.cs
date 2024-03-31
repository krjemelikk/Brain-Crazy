using UnityEngine;
using UnityEngine.UI;

public class Level_20 : BaseLevel
{
    [Header("Answers")]
    public Button no1;
    public Button no2;
    public Button no3;
    public Image cigarette;
    public Button checkBtn;

    [SerializeField] private Sprite[] clicks;

    private int numberClick;
    private int resultAnswer;

    protected override void Start()
    {
        base.Start();
        no1.onClick.AddListener(() => WrongAnswer());
        no2.onClick.AddListener(() => WrongAnswer());
        no3.onClick.AddListener(() => WrongAnswer());
        checkBtn.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 3;
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
        numberClick++;
        cigarette.GetComponent<Image>().sprite = clicks[Mathf.Clamp(numberClick, 0, clicks.Length - 1)];

        if (numberClick >= resultAnswer)
        {
            RightAnswer();
        }
    }
}
