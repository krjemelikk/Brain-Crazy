using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_164 : BaseLevel
{
    [Header("Answers")]
    public Button thief;
    public Button thief_2;

    public Image ring;

    private bool isShake;
    private bool isEnd = false;


    protected override void Start()
    {
        base.Start();
        thief_2.onClick.AddListener(() => WrongAnswer());
        thief.onClick.AddListener(() => CheckAnswer());
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
                ring.transform.DOLocalMoveY(-360f, 1f);
                ring.transform.DOScale(1f, 1f);
            }
        }
    }
}
