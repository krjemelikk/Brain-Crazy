using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardLuckyBox : BaseBox
{
    private static GameObject instance;

    public UnityAction moreActionOff;

    private UnityAction actionCloseButton;
    public UnityAction actionHide;
    public UnityAction actionClaim;

    public RewardLuckyData luckyData;
    public Button btnClaim;
    public Button btnWatchVideo;
    //public Button btnClaimNow;

    public GameObject hintRandomObj;

    private enum LuckyPosition
    {
        InGame, EndGame
    }
    private LuckyPosition luckyPosition;

    protected override void OnStart()
    {
        base.OnStart();
        backObj.timeAnimClose = 0.5f;
        btnClaim.onClick.RemoveAllListeners();
        btnClaim.onClick.AddListener(() => OnClickClaim());
        btnWatchVideo.onClick.RemoveAllListeners();
        btnWatchVideo.onClick.AddListener(() => OnClickWatchVideo());
    }

    protected override void ActionDoOff()
    {
        base.ActionDoOff();
        if (moreActionOff != null)
            moreActionOff();

        mainPanel.localScale = Vector3.one;
        mainPanel.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).SetEase(Ease.InBack).OnComplete(() =>
        {
            if (actionHide != null)
            {
                actionHide();
                actionHide = null;
            }
        });

    }

    public static RewardLuckyBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load(PathPrefabs.REWARDLUCKYBOX) as GameObject);
        }
        instance.SetActive(true);
        return instance.GetComponent<RewardLuckyBox>();
    }

    public override void Show()
    {
        base.Show();
        ItemRewardLucky itemReward = luckyData.GetItem();
        btnClaim.gameObject.SetActive(!itemReward.isWatchVideo);
        btnWatchVideo.gameObject.SetActive(itemReward.isWatchVideo);
        //btnClaimNow.onClick.RemoveAllListeners();
        //if (itemReward.isWatchVideo)
        //    btnClaimNow.onClick.AddListener(OnClickWatchVideo);
        //else
        //    btnClaimNow.onClick.AddListener(OnClickClaim);
        DataManager.TimeOpenRewardLucky = UnbiasedTime.Instance.Now;
        actionClaim = null;
        luckyPosition = LuckyPosition.EndGame;
        hintRandomObj.SetActive(true);
    }

    /// <summary>
    /// Show Reward không phải ở cuối game
    /// </summary>
    public void ShowRewardNotEndGame(UnityAction actionClaim = null)
    {
        ItemRewardLucky itemReward = luckyData.GetItem();
        btnClaim.gameObject.SetActive(!itemReward.isWatchVideo);
        btnWatchVideo.gameObject.SetActive(itemReward.isWatchVideo);
        //btnClaimNow.onClick.RemoveAllListeners();
        //if (itemReward.isWatchVideo)
        //    btnClaimNow.onClick.AddListener(OnClickWatchVideo);
        //else
        //    btnClaimNow.onClick.AddListener(OnClickClaim);

        this.actionClaim = actionClaim;
        luckyPosition = LuckyPosition.InGame;
        hintRandomObj.SetActive(true);
    }


    public void ShowClaim()
    {
        ItemRewardLucky itemReward = luckyData.GetItem();
        btnClaim.gameObject.SetActive(true);
        btnWatchVideo.gameObject.SetActive(false);
        //btnClaimNow.onClick.RemoveAllListeners();
        //btnClaimNow.onClick.AddListener(OnClickClaim);
        DataManager.TimeOpenRewardLucky = UnbiasedTime.Instance.Now;
        actionClaim = null;
        hintRandomObj.SetActive(false);
    }


    private void OnClickClaim()
    {
        backObj.DoOff();
        DataManager.AddHint(1);
        RewardIAPBox.Setup().ShowByWatchVideo(1);
        if (actionClaim != null)
        {
            actionClaim();
        }
        //MoreHintPanel.Setup();
    }

    private void OnClickWatchVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, luckyPosition == LuckyPosition.EndGame ? ActionWatchVideo.LuckyReward_EndGame : ActionWatchVideo.LuckyReward_InGame);
    }

    private void ActionReward()
    {
        backObj.DoOff();
        Debug.Log("Claim watch ad");
        float rewardHintPercent = Random.Range(0, 100);
        int rewardHint = 1;

        if (TimeManager.CaculateTime(UnbiasedTime.Instance.Now, DataManager.TimeLastRandomMaxHint) >= 86400)
        {
            rewardHint = 3;
            DataManager.AddHint(3);
            DataManager.TimeLastRandomMaxHint = UnbiasedTime.Instance.Now;
        }
        else
        {
            if (rewardHintPercent >= 0 && rewardHintPercent < RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.PERCENT_3_LUCKY_BOX, 10))
            {
                DataManager.AddHint(3);
                rewardHint = 3;
            }
            else if (rewardHintPercent >= RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.PERCENT_3_LUCKY_BOX, 10) && rewardHintPercent <= RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.PERCENT_2_LUCKY_BOX, 45))
            {
                DataManager.AddHint(2);
                rewardHint = 2;
            }
            else
                DataManager.AddHint(1);
        }
        
        RewardIAPBox.Setup().ShowByWatchVideo(rewardHint);
        if (actionClaim != null)
        {
            actionClaim();
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
}
