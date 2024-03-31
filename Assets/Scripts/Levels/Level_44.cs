using UnityEngine;
using UnityEngine.UI;

public class Level_44 : BaseLevel
{
    public Image[] Cows;
    public Sprite[] sprCows;
    public GameObject[] Stars;

    protected override void Start()
    {
        base.Start();
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
    
    private Vector3 shakeDir;
    public void CheckShakeTrigger()
    {
        shakeDir = Input.acceleration;

        if (shakeDir.sqrMagnitude >= 12f)
        {
            Debug.Log(StringHelper.StringColor("Shakeeeeeeeeee", ColorString.red));

            for (int i = 0; i < Cows.Length; i++)
            {
                Cows[i].sprite = sprCows[1];
            }

            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i].SetActive(true);
            }

            RightAnswer();
        }
    }
}