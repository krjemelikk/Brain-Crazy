using UnityEngine;
using UnityEngine.UI;

public class Level_30 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public Button btNext;
    public Button btPre;
    public Text txtNumber;

    private int resultAnswer;

    private int answer;

    public GameObject[] mushroom;
    public Button cloud;

    public Image imgThemeRain;

    public Sprite[] sprCloud;

    private int countTap;
    private int countNeedTap;

    private bool isHold = false;

    protected override void Start()
    {
        base.Start();
        countNeedTap = 2;
        countTap = answer = 0;
        resultAnswer = 4;
        imgThemeRain.gameObject.SetActive(false);
        cloud.GetComponent<Image>().sprite = sprCloud[countTap];
        btOK.onClick.AddListener(() => CheckAnswer());
        btNext.onClick.AddListener(() => OnClickNext());
        btPre.onClick.AddListener(() => OnClickPre());
        UpdateUI();
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

    public void OnClickCloud()
    {
        countTap++;
        cloud.GetComponent<Image>().sprite = sprCloud[Mathf.Clamp(countTap, 0, sprCloud.Length - 1)];
        if (countTap >= countNeedTap)
        {
            imgThemeRain.gameObject.SetActive(true);
            for (int i = 0; i < mushroom.Length; i++)
            {
                mushroom[i].SetActive(true);
            }
        }
    }

    private void CheckAnswer()
    {
        if (answer == resultAnswer && countTap >= countNeedTap) RightAnswer();
        else WrongAnswer();
    }

    private void OnClickNext()
    {
        answer++;
        UpdateUI();
    }

    private void OnClickPre()
    {
        answer--;
        if (answer < 0)
            answer = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        txtNumber.text = answer.ToString();
    }
}
