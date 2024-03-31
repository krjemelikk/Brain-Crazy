using UnityEngine;
using UnityEngine.UI;

public class Level_185 : BaseLevel
{
    public Image imgPlayer;
    public Image imgWood;

    public Sprite spPlayer;
    public Sprite spWood;

    public Button btnPower;

    public Scrollbar scrollbar;

    private bool isDone;
    private bool isEnd;
    protected override void Start()
    {
        base.Start();
        btnPower.onClick.RemoveAllListeners();
        btnPower.onClick.AddListener(OnclickDone);
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
        StartCoroutine(Helper.StartAction(() =>
        {
            GameController.Instance.ResetLevel();
        }, 1f));
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void CheckDone()
    {
        if(scrollbar.value == 1f)
        {
            isDone = true;
        }
        else
        {
            isDone = false;
        }
    }

    public void OnclickDone()
    {
        if(!isEnd)
        isEnd = true;
        if (isDone)
        {
            imgPlayer.sprite = spPlayer;
            imgWood.sprite = spWood;
            imgPlayer.SetNativeSize();
            imgWood.SetNativeSize();

            RightAnswer();
        }
        else
        {
            imgPlayer.sprite = spPlayer;
            imgPlayer.SetNativeSize();

            WrongAnswer();
        }
    }
}
