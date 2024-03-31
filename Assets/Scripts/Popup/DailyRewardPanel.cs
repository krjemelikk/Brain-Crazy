using UnityEngine;
using UnityEngine.UI;
using EventDispatcher;

public class DailyRewardPanel : BaseBox
{
    private static DailyRewardPanel instance;

    public static DailyRewardPanel Setup()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<DailyRewardPanel>(PathPrefabs.PANEL_DAILY_REWARD), GameController.Instance.HomeScene.tfPopupParent);
        }
        instance.Show();
        return instance;
    }

    [SerializeField] private DailyRewardItem[] lsDailyRewardItems;
    [SerializeField] private Button claimNowBtn;
    [SerializeField] private Button claimVideoBtn;
    [SerializeField] private Button closeBtn;

    protected override void OnStart()
    {
        base.OnStart();

        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => { CloseCurrentBox(); });
        claimNowBtn.gameObject.SetActive(false);
        claimVideoBtn.gameObject.SetActive(false);
        this.RegisterListener(EventID.CLAIM_DAILY_REWARD, (param) => OnHandleClaim());
        for (int i = 0; i < lsDailyRewardItems.Length; i++)
        {
            StateRewardItem state = StateRewardItem.NOT_CLAIM;
            if (lsDailyRewardItems[i].ID < DataManager.DailyRewardID)
            {
                state = StateRewardItem.CLAIMED;
            }
            else if (i == DataManager.DailyRewardID)
            {
                state = DataManager.CanClaimDailyRewardToday ? StateRewardItem.CAN_CLAIM : StateRewardItem.CLAIMED;
            }
            lsDailyRewardItems[i].InitData(string.Format("Day {0}", i + 1), GameController.Instance.dataContains.GameConfig.lsDailyRewardWood[i], state);

            if(state == StateRewardItem.CAN_CLAIM)
            {
                int index = i;
                claimNowBtn.gameObject.SetActive(true);
                claimNowBtn.onClick.RemoveAllListeners();
                claimNowBtn.onClick.AddListener(() => { lsDailyRewardItems[index].Claim(); });

                claimVideoBtn.gameObject.SetActive(true);
                claimVideoBtn.onClick.RemoveAllListeners();
                claimVideoBtn.onClick.AddListener(() => 
                {
                    GameController.Instance.admobAds.ShowVideoReward(()=> 
                    {
                        claimNowBtn.gameObject.SetActive(false);
                        int valueReward = lsDailyRewardItems[index].ClaimMulti(multi: 2);
                    }, ActionNotLoad, ActionSkip, ActionWatchVideo.DoubleReward_DailyLogin);
                   
                });

                closeBtn.onClick.AddListener(() => { lsDailyRewardItems[index].Claim(); ; });
            }
        }
    }
    
    private void ActionNotLoad()
    {
       // ConfirmBox.Setup().AddMessageYes("Fail", "Failed to load video", () => CloseCurrentBox());
    }

    private void ActionSkip()
    {
        Debug.Log("Skip video");
    }

    private void OnHandleClaim()
    {
        CloseCurrentBox();
    }
}
