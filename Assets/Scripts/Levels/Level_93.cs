using UnityEngine;
using UnityEngine.UI;

public class Level_93 : BaseLevel
{
    [Header("Answers")]
    public Button catSleep;
    public Button nightMare;
    public GameObject catWakeUp;
    
    private bool isShake;
    private bool isEnd = false;

    protected override void Start()
    {
        base.Start();
        catSleep.onClick.AddListener(() => WrongAnswer());
        nightMare.onClick.AddListener(() => WrongAnswer());

        catWakeUp.gameObject.SetActive(false);
        nightMare.gameObject.SetActive(true);
        catSleep.gameObject.SetActive(true);
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

        if (shakeDir.sqrMagnitude >= 12f)
        {
            Debug.Log(StringHelper.StringColor("Shakeeeeeeeeee", ColorString.red));
            if (!isShake) isShake = true;

            if (isShake)
            {
                catWakeUp.gameObject.SetActive(true);
                nightMare.gameObject.SetActive(false);
                catSleep.gameObject.SetActive(false);
                RightAnswer();
            }
        }
    }
}
