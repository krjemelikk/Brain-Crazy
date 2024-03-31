using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_113 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public InputField inputField;

    public DragUI player;

    private int resultAnswer;

    private bool isComplete;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 7;
    }

    protected override void Update()
    {
        base.Update();
        if (player.transform.localPosition.y > 70f && !isComplete)
        {
            player.SetActiveDrag(false);
            player.transform.DOLocalMoveY(300f, 1f);
            isComplete = true;
        }
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

    private void CheckAnswer()
    {
        if (player.transform.localPosition.y > 70f)
        {
            int _result = 0;
            if (string.IsNullOrEmpty(inputField.text) || !int.TryParse(inputField.text, System.Globalization.NumberStyles.Integer, null, out _result))
            {
                WrongAnswer();
                return;
            }

            if (_result == resultAnswer) RightAnswer();
            else
                WrongAnswer();
        }
        else
            WrongAnswer();
    }
}
