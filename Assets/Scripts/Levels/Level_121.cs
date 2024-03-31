using UnityEngine;
using UnityEngine.UI;

public class Level_121 : BaseLevel
{
    public Button btnVideo;
    public Image iconChest;
    public Sprite iconOpenChest;
    public Image iconChest2;
    public Sprite iconOpenChest2;

    public GameObject watchVideo_1;
    public GameObject watchVideo_2;

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

    public void ShowVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.PassLevel);
    }


    private void ActionReward()
    {
        Debug.Log("Claim watch ad");
        btnVideo.gameObject.SetActive(false);
        iconChest.sprite = iconOpenChest;
        Helper.StartActionNotUseCorutines(RightAnswer, 1f);
        watchVideo_1.SetActive(false);
    }

    private void ActionNotLoad()
    {
        //ConfirmBox.Setup().AddMessageYes("Fail", "Failed to load video", () => { });
    }

    private void ActionSkip()
    {
        Debug.Log("Skip video");
    }

    public void ShowVideo2()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward2, ActionNotLoad2, ActionSkip2, ActionWatchVideo.PassLevel);
    }


    private void ActionReward2()
    {
        Debug.Log("Claim watch ad");
        btnVideo.gameObject.SetActive(false);
        iconChest.sprite = iconOpenChest;
        Helper.StartActionNotUseCorutines(RightAnswer, 1f);
        watchVideo_2.SetActive(false);
    }

    private void ActionNotLoad2()
    {
        //ConfirmBox.Setup().AddMessageYes("Fail", "Failed to load video", () => { });
    }

    private void ActionSkip2()
    {
        Debug.Log("Skip video");
    }
}
