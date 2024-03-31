using UnityEngine;
using UnityEngine.UI;

public class Level_95 : BaseLevel
{
    [Header("Answers")]
    public Toggle[] toggles;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].onValueChanged.AddListener((v) => { RightAnswer(); }); ;
        }
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
}
