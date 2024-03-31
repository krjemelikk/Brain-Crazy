using UnityEngine;
using UnityEngine.UI;

public class Level_86 : BaseLevel
{
    [Header("Answers")]
    public Button num_1_Btn;
    public Button num_6_Btn;
    public Button num_11_Btn;
    public Button num_45_Btn;
    public Button num_60_Btn;
    public Button num_33_Btn;
    public Button num_101_Btn;
    public Button num_7_Btn;
    public Button num_81_Btn;

    public Text num_1_Txt;
    public Text num_6_Txt;
    public Text num_11_Txt;
    public Text num_45_Txt;
    public Text num_60_Txt;
    public Text num_33_Txt;
    public Text num_101_Txt;
    public Text num_7_Txt;
    public Text num_81_Txt;

    private int countClick;
    private int[] orderNums;//Thứ tự các số

    protected override void Start()
    {
        base.Start();

        num_1_Btn.onClick.RemoveAllListeners();
        num_1_Btn.onClick.AddListener(() => { num_1_Txt.text = (countClick + 1).ToString(); OnClickNumber(1); });
        num_6_Btn.onClick.RemoveAllListeners();
        num_6_Btn.onClick.AddListener(() => { num_6_Txt.text = (countClick + 1).ToString(); OnClickNumber(6); });
        num_11_Btn.onClick.RemoveAllListeners();
        num_11_Btn.onClick.AddListener(() => { num_11_Txt.text = (countClick + 1).ToString(); OnClickNumber(11); });
        num_45_Btn.onClick.RemoveAllListeners();
        num_45_Btn.onClick.AddListener(() => { num_45_Txt.text = (countClick + 1).ToString(); OnClickNumber(45); });
        num_60_Btn.onClick.RemoveAllListeners();
        num_60_Btn.onClick.AddListener(() => { num_60_Txt.text = (countClick + 1).ToString(); OnClickNumber(60); });
        num_33_Btn.onClick.RemoveAllListeners();
        num_33_Btn.onClick.AddListener(() => { num_33_Txt.text = (countClick + 1).ToString(); OnClickNumber(33); });
        num_101_Btn.onClick.RemoveAllListeners();
        num_101_Btn.onClick.AddListener(() => { num_101_Txt.text = (countClick + 1).ToString(); OnClickNumber(101); });
        num_7_Btn.onClick.RemoveAllListeners();
        num_7_Btn.onClick.AddListener(() => { num_7_Txt.text = (countClick + 1).ToString(); OnClickNumber(7); });
        num_81_Btn.onClick.RemoveAllListeners();
        num_81_Btn.onClick.AddListener(() => { num_81_Txt.text = (countClick + 1).ToString(); OnClickNumber(81); });
        countClick = 0;
        orderNums = new int[] { 1, 6, 11, 45, 60, 33, 101, 7, 81 };
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

    private void OnClickNumber(int value)
    {

        if (orderNums[countClick] == value)
        {
            countClick++;
            if (countClick >= 2)
                txtQuestion.gameObject.SetActive(false);

            if (countClick >= 9)
                RightAnswer();

        }
        else
        {
            WrongAnswer();
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        countClick = 0;

        num_1_Txt.text = "";
        num_6_Txt.text = "";
        num_11_Txt.text = "";
        num_45_Txt.text = "";
        num_60_Txt.text = "";
        num_33_Txt.text = "";
        num_101_Txt.text = "";
        num_7_Txt.text = "";
        num_81_Txt.text = "";

        txtQuestion.gameObject.SetActive(true);
    }
}
