using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardShowBox : BaseBox
{
    private static GameObject instance;

    [SerializeField] private Text valueRewardTxt;
    [SerializeField] private Button claimNowBtn;
    [SerializeField] private Button claimVideoBtn;
    private int valueReward;
    private UnityAction actionClaimDone;

    public static RewardShowBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load(PathPrefabs.REWARD_SHOW_BOX) as GameObject);
            instance.GetComponent<RewardShowBox>().Init();
        }
        instance.SetActive(true);
        return instance.GetComponent<RewardShowBox>();
    }

    public void Init()
    {
        claimNowBtn.onClick.RemoveAllListeners();
        claimNowBtn.onClick.AddListener(ClaimNow);

        claimVideoBtn.onClick.RemoveAllListeners();
        claimVideoBtn.onClick.AddListener(ClaimVideo);
    }

    public void Show(int valueReward, UnityAction actionClaimDone)
    {
        valueRewardTxt.text = valueReward.ToString();
        this.valueReward = valueReward;
        this.actionClaimDone = actionClaimDone;
    }

    public void ClaimNow()
    {
        backObj.DoOff();
        DataManager.AddHint(this.valueReward);
        RewardIAPBox.Setup().ShowByWatchVideo(this.valueReward);

        if (actionClaimDone != null)
            actionClaimDone();
    }

    public void ClaimVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.RewardAccumulate);
    }

    private void ActionReward()
    {
        backObj.DoOff();
        this.valueReward = this.valueReward * 2;
        DataManager.AddHint(this.valueReward);
        RewardIAPBox.Setup().ShowByWatchVideo(this.valueReward);

        if (actionClaimDone != null)
            actionClaimDone();
    }

    private void ActionNotLoad()
    {
        //ConfirmBox.Setup().AddMessageYes("Fail", "Failed to load video", () => CloseCurrentBox());
    }

    private void ActionSkip()
    {
        Debug.Log("Skip video");
    }
}
