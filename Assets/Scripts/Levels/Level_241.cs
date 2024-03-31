using UnityEngine;
using UnityEngine.UI;

public class Level_241 : BaseLevel
{
    public Text txtTime;

    public GameObject panelStart;

    private float timeReset;

    private bool isDone;

    private bool isStart;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone && isStart)
        {
            timeReset -= Time.deltaTime;
            txtTime.text = string.Format("{0:00}:{1:00}", 0, timeReset);
            if (timeReset <= 0)
            {
                RightAnswer();
                isDone = true;
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

    public override void WrongAnswer()
    {
        base.WrongAnswer();
        GameController.Instance.ResetLevel();
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void PlayGame()
    {
        timeReset = 30f;
        panelStart.SetActive(false);
        isStart = true;
    }
}
