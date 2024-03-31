using UnityEngine;
using UnityEngine.UI;

public class Level_100 : BaseLevel
{
    [Header("Answers")]
    public Button[] emojjis;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < emojjis.Length; i++)
        {
            emojjis[i].onClick.RemoveAllListeners();
            emojjis[i].onClick.AddListener(RightAnswer);
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
