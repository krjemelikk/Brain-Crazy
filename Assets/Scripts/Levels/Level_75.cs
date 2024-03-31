using UnityEngine.UI;

public class Level_75 : BaseLevel
{
    public Button btOK;
    public Text txtNumber;
    private string answer;
    private int result = 67;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(CheckAnswer);
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
        answer = string.Empty;
        UpdateUI();
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

    private void CheckAnswer()
    {
        int intAnswer = 0;
        if(int.TryParse(answer, out intAnswer))
        {
            if(intAnswer == result)
            {
                RightAnswer();
                return;
            }
        }

        WrongAnswer();
    }

    public void OnClickNumber(int number)
    {
        answer += number.ToString();
        UpdateUI();
    }

    private void UpdateUI()
    {
        txtNumber.text = answer;
    }
}
