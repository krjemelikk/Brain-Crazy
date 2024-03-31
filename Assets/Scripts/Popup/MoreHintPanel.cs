using EventDispatcher;
using UnityEngine;
using UnityEngine.UI;

public class MoreHintPanel : BaseBox
{
    private static MoreHintPanel instance;

    public static MoreHintPanel Setup()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<MoreHintPanel>(PathPrefabs.PANEL_MORE_HINT));
        }

        instance.Show();
        return instance;
    }

    [SerializeField] private Button btShop;
    [SerializeField] private Button btWatchAds;
    [SerializeField] private Button shareBtn;

    private void Start()
    {
        btShop.onClick.AddListener(() => OnClickShop());
        btWatchAds.onClick.AddListener(() => OnClickWatchVideo());
    }

    protected override void OnStart()
    {
        base.OnStart();
        
        if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_SHARE_ADD_HINT, false) && TimeManager.CaculateTime(UnbiasedTime.Instance.Now, DataManager.TimeLastShareAddHint) >= DataManager.TimeDelayShareAddHint)
        {
            shareBtn.gameObject.SetActive(true);
            shareBtn.onClick.RemoveAllListeners();
            shareBtn.onClick.AddListener(() =>
            {
                GameController.Instance.HomeScene.OnClickShare(() =>
                {
                    DataManager.AddHint(1);
                    RewardIAPBox.Setup().ShowByWatchVideo(1);
                    shareBtn.gameObject.SetActive(false);
                    DataManager.TimeLastShareAddHint = UnbiasedTime.Instance.Now;
                });
            });
        }
        else
        {
            shareBtn.gameObject.SetActive(false);
        }
    }

    private void OnClickShop()
    {
        this.PostEvent(EventID.GO_SHOP);
        InappPanel.Setup();
    }

    private void OnClickWatchVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.AddHint_PopupMoreHint);
    }

    private void ActionReward()
    {
        Debug.Log("Claim watch ad");
        DataManager.AddHint(1);
        RewardIAPBox.Setup().ShowByWatchVideo(1);
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