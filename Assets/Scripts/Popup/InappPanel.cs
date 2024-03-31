using UnityEngine;
using UnityEngine.UI;

public class InappPanel : BaseBox
{
    private static InappPanel instance;

    public static InappPanel Setup()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<InappPanel>(PathPrefabs.PANEL_IAP));
        }
        instance.Show();
        return instance;
    }

    [SerializeField] private IAPDatabase IAPData;

   // public List<IAPItem> lsIAPItem;
    public Button watchVideoBtn;

    protected override void OnStart()
    {
        base.OnStart();

        InitData();
    }

    public void InitData()
    {
        // for (int i = 0; i < lsIAPItem.Count; i++)
        // {
        //     if (!lsIAPItem[i].gameObject.activeInHierarchy)
        //         continue;
        //
        //     lsIAPItem[i].InitData();
        // }

        watchVideoBtn.onClick.RemoveAllListeners();
        watchVideoBtn.onClick.AddListener(OnClickWatchVideo);
    }

    private void OnClickWatchVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.AddHint_InShop);
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
