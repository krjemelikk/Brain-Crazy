using UnityEngine;
using UnityEngine.UI;

public class Level_56 : BaseLevel
{
    [Header("Answers")]
    public Button btPig;
    public Button btWatchVideo;
    protected override void Start()
    {
        base.Start();
        btPig.onClick.AddListener(() => WrongAnswer());
        btWatchVideo.onClick.AddListener(() => OnClickWatchVideo());
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

    private void OnClickWatchVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.PassLevel);
    }

    private void ActionReward()
    {
        Debug.Log("Claim watch ad");
        RightAnswer();
    }

    private void ActionNotLoad()
    {
        //ConfirmBox.Setup().AddMessageYes("Fail", "Failed to load video", () => { });
    }

    private void ActionSkip()
    {
        Debug.Log("Skip video");
    }
}