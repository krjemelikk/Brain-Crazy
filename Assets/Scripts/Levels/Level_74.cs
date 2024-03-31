using UnityEngine;
using UnityEngine.UI;

public class Level_74 : BaseLevel
{
    [Header("Answers")]
    public Button theButton;
    public Button theYellowButton;
    public Text theText;

    private int countCLick;
    private bool isEnd;

    protected override void Start()
    {
        base.Start();
        theButton.onClick.AddListener(OnClick);
        theYellowButton.onClick.AddListener(WrongAnswer);
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
        theYellowButton.gameObject.SetActive(false);
        theYellowButton.transform.position = theButton.transform.position;
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

    private void OnClick()
    {
        if (isEnd) return;

        countCLick -= -1;
        UpdateUI();

        if (countCLick >= 3 && !theYellowButton.gameObject.activeInHierarchy)
        {
            theYellowButton.gameObject.SetActive(true);
        }

        if(countCLick >= 5)
        {
            RightAnswer();
        }
    }

    private void UpdateUI()
    {
        theText.text = countCLick > 0 ? $"x{countCLick}" : string.Empty;
    }

    public void EndDrag()
    {
        var distance = Vector3.Distance(theYellowButton.transform.position, theButton.transform.position);

        if(distance <= 1f)
        {
            theYellowButton.transform.position = theButton.transform.position;
        }
    }
}