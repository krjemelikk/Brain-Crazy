using UnityEngine;
using UnityEngine.UI;

public class Level_205 : BaseLevel
{
    public Image bgHide;
    private float time = 0f;

    private bool isDown;
    private bool isEnd;
    public void OnClickDown()
    {
        isDown = true;
    }

    public void OnClickUp()
    {
        isDown = false;
        time = 0f;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isEnd) return;
        if (isDown)
        {
            time += Time.deltaTime;
            if(time >= 2f)
            {
                isEnd = true;
                bgHide.color = new Color(0f, 0f, 0f, 0.5f);
                RightAnswer();
            }
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

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }
}