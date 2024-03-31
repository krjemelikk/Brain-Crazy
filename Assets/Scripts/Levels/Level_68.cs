using UnityEngine;
using UnityEngine.UI;

public class Level_68 : BaseLevel
{
    [Header("Answers")]
    public Button theButton;
    public Text theText;

    private int countCLick;
    private bool isEnd;
    private int maxShowText = 1;

    protected override void Start()
    {
        base.Start();
        maxShowText = Random.Range(6, 10);
        theButton.onClick.AddListener(OnClick);
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
        countCLick = 0;
        numShow = 0;
        maxShowText = Random.Range(6, 10);
        theText.text = string.Empty;
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        isEnd = true;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void OnClick()
    {
        if (isEnd) return;

        countCLick -= -1;
        UpdateUI();
    }

    int numShow;
    private void UpdateUI()
    {
        //if(countCLick < maxShowText)
        //{
        //    theText.text = countCLick > 0 ? $"x{countCLick}" : string.Empty;
        //}
        //else
        //{
        //    theText.text = string.Empty;
        //}
        if (countCLick != maxShowText)
        {
            numShow++;
        }
        theText.text = "x" +numShow.ToString();
        
    }


    public void CheckAnswer()
    {
        if(countCLick == 10)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}