using DG.Tweening;
using UnityEngine.UI;
public class Level_89 : BaseLevel
{
    public Button btX;
    public Image successImg;
    public Text theTextInput;

    private string answer;
    private bool isEnd;

    protected override void Start()
    {
        base.Start();
        btX.onClick.AddListener(() =>
        {
            successImg.DOKill();
            successImg.fillAmount = 0;
            successImg.DOFillAmount(1, 0.5f).OnComplete(() => { RightAnswer(); });
        });
        answer = string.Empty;
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
        answer = string.Empty;
        UpdateUI();
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

    private void UpdateUI()
    {
        theTextInput.text = answer;
    }

    public void OnClickNumber(int number)
    {
        if (isEnd) return;

        answer += number.ToString();
        UpdateUI();
    }

    public void OnClickOK()
    {
        WrongAnswer();
    }
}
