using UnityEngine;
using UnityEngine.UI;

public class Level_7 : BaseLevel
{
    protected override void Start()
    {
        base.Start();
        txtQuestion.transform.GetComponent<Button>().onClick.AddListener(() => RightAnswer());
        foreach (var item in GameObject.FindGameObjectsWithTag("Color_Level_7"))
        {
            int r = Random.Range(100, 255);
            item.GetComponent<Image>().color = new Color32((byte)r, (byte)(r / 2), (byte)(r / 3), 255);
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
