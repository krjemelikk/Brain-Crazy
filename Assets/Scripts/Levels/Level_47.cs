using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level_47 : BaseLevel
{
    [Header("Answers")]
    public Button theYellow;
    public Button theYellow_GreenFake;
    public Text theTextYellow;   
    public Button theGreen;
    public Text theTextGreen;

    private int countCLick_Yellow;
    private int countCLick_Green;
    private int countCLick_Yellow_Fake;
    private bool isCanClick, isEnd;

    protected override void Start()
    {
        base.Start();
        isCanClick = true;
        theYellow.onClick.AddListener(() => OnClickYellow());
        theGreen.onClick.AddListener(() => OnClickGreen());
        theYellow_GreenFake.onClick.AddListener(() => WrongAnswer());
        countCLick_Yellow_Fake = Random.Range(2, 5);
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
        countCLick_Yellow = countCLick_Green = 0;
        isCanClick = false;
        UpdateUI();
        StopAllCoroutines();
        theYellow_GreenFake.gameObject.SetActive(false);
        countCLick_Yellow_Fake = Random.Range(2, 5);
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

    private void OnClickYellow()//5
    {
        if (isEnd) return;

        countCLick_Yellow -= -1;
        UpdateUI();

        if (countCLick_Green < 3 || !isCanClick)
        {
            WrongAnswer();
            return;
        }

        
        if(countCLick_Yellow == countCLick_Yellow_Fake)
        {
            StartCoroutine(YellowFake());
            return;
        }

        if(countCLick_Yellow == 5)
        {
            RightAnswer();
        }
    }

    IEnumerator YellowFake()
    {
        theYellow_GreenFake.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        theYellow_GreenFake.gameObject.SetActive(false);
    }

    private void OnClickGreen()//3
    {
        if (isEnd) return;

        countCLick_Green -= -1;
        UpdateUI();

        if (countCLick_Green == 3)
        {
            isCanClick = true;
        }
        else if(countCLick_Green > 3)
        {
            WrongAnswer();
            return;
        }
    }

    private void UpdateUI()
    {
        theTextYellow.text = countCLick_Yellow > 0 ? $"x{countCLick_Yellow}" : string.Empty;
        theTextGreen.text = countCLick_Green > 0 ? $"x{countCLick_Green}" : string.Empty;
    }
}