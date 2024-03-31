using UnityEngine;
using UnityEngine.UI;

public class Level_31 : BaseLevel
{
    [Header("Answers")]
    public Button theBolt;
    public Button theBolt_2;
    public Button theBolt_3;
    public Button theBolt_4;

    public Image bot;

    private bool isShake;
    private bool isEnd = false;

    protected override void Start()
    {
        base.Start();
        theBolt_3.onClick.AddListener(() => WrongAnswer());
        theBolt_2.onClick.AddListener(() => WrongAnswer());
        theBolt_4.onClick.AddListener(() => WrongAnswer());
        theBolt.onClick.AddListener(() => CheckAnswer());
    }

    protected override void Update()
    {
        base.Update();

        CheckShakeTrigger();
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
        isEnd = true;
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (!isShake)
        {
            WrongAnswer();
            return;
        }

        RightAnswer();
    }

    private Vector3 shakeDir;
    private void CheckShakeTrigger()
    {
        if (isEnd)
            return;

        shakeDir = Input.acceleration;

        if (shakeDir.sqrMagnitude >= 10f)
        {
            Debug.Log(StringHelper.StringColor("Shakeeeeeeeeee", ColorString.red));
            if (!isShake) isShake = true;

            if (isShake)
            {
                bot.gameObject.SetActive(true);
            }
        }
    }
}
