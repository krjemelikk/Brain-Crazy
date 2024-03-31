using UnityEngine.UI;
using UnityEngine;

public class Level_148 : BaseLevel
{
    public Image imgTV;
    public Image imgBaby;

    public Sprite spTV1;
    public Sprite spTV2;

    public Sprite spBaby;

    private int indexTV = 0;

    protected override void Start()
    {
        base.Start();
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

    public void RemoteTV()
    {
        if (indexTV >= 2)
            return;
        indexTV++;
        if (indexTV == 1)
            imgTV.sprite = spTV1;
        if (indexTV == 2)
            imgTV.sprite = spTV2;
        if (indexTV >= 2)
        {
            imgBaby.sprite = spBaby;
            RightAnswer();
        }
    }
}
