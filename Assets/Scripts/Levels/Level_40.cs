using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_40 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public Button btNext;
    public Button btPre;
    public Text txtNumber;
    public DOTweenAnimation[] Coconuts;
    

    private bool isShake;
    private int answer, resultAnswer;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        btNext.onClick.AddListener(() => OnClickNext());
        btPre.onClick.AddListener(() => OnClickPre());
        resultAnswer = 4;
        UpdateUI();
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
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (answer == resultAnswer && isShake)
            RightAnswer();
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

    private Vector3 shakeDir;
    private void CheckShakeTrigger()
    {
        if(isShake) return;
        
        shakeDir = Input.acceleration;

        if (shakeDir.sqrMagnitude >= 12f)
        {
            Debug.Log(StringHelper.StringColor("Shakeeeeeeeeee", ColorString.red));
            if (!isShake) isShake = true;

            if (isShake)
            {
                for (int i = 0; i < Coconuts.Length; i++)
                {
                    if (Coconuts[i].gameObject.activeInHierarchy)
                    {
                        Coconuts[i].DOPlay();
                    }
                    else
                    {
                        Coconuts[i].gameObject.SetActive(true);
                    }
                }

                answer = 0;
                UpdateUI();
            }
        }
    }
}
