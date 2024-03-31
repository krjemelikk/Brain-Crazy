using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;



//  ----------------------------------------------
//  Author:     CuongCT <caothecuong91@gmail.com> 
//  Copyright (c) 2016 OneSoft JSC
// ----------------------------------------------

public class AdmobAds : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

    public void Init()
    {
        // Initialize the Google Mobile Ads SDK.
        // Debug.Log("AdmobId " + Config.AdmobId);
        MobileAds.Initialize(Config.AdmobId);

        InitBanner();
        InitInterstitial();
        InitRewardVideo();
    }

    #region Interstitial
    private bool isInited_Interstitial;
    private void InitInterstitial()
    {
        RequestInterstitial(Config.Admob_Interstitial_ID);
        isInited_Interstitial = true;

    }

    public int amountInterClick
    {
        get
        {
            return PlayerPrefs.GetInt("Amount_Inter_Click", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Amount_Inter_Click", value);
        }
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        //inter click
        Debug.Log("Click inter !!!");
        amountInterClick++;
    }

    public bool ShowInterstitial()
    {
        if (amountInterClick > GameController.Instance.capingInter)
            return false;

        if (DataManager.RemoveAds != 0)
            return false;

        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            return true;
        }

        return false;
    }

    private void RequestInterstitial(string id)
    {
        if (DataManager.RemoveAds != 0)
            return;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        try
        {
            if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_DESTROY_REFRENCE_ADS, false))
            {
                if (this.interstitial != null)
                    this.interstitial.Destroy();
            }
        }
        catch
        {

        }

        this.interstitial = new InterstitialAd(id);
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial(Config.Admob_Interstitial_ID_LOW);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial(Config.Admob_Interstitial_ID);
    }
    #endregion

    #region Video Reward
    private UnityAction actionClose;
    private UnityAction actionRewardVideo;
    private UnityAction actionNotLoadedVideo;

    private void InitRewardVideo()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        RequestRewardBasedVideo(Config.Admob_Reward_ID);
    }

    private void RequestRewardBasedVideo(string id)
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, id);
    }

    /// <summary>
    /// Load thành công
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {

    }

    /// <summary>
    /// Load Fail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (actionNotLoadedVideo != null)
            actionNotLoadedVideo();

        RequestRewardBasedVideo(Config.Admob_Reward_ID_LOW);
    }

    /// <summary>
    /// Video đã mở
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {

    }

    /// <summary>
    /// Video bắt đầu được chạy
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        //Time.timeScale = 0;
    }

    /// <summary>
    /// Video bị đóng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        if (actionClose != null)
            actionClose();

        //Time.timeScale = 1;
        RequestRewardBasedVideo(Config.Admob_Reward_ID);
    }

    /// <summary>
    /// Video đã chạy xong
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        if (actionRewardVideo != null)
            actionRewardVideo();
    }

    public int amountVideoRewardClick
    {
        get
        {
            return PlayerPrefs.GetInt("Amount_VideoReward_Click", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Amount_VideoReward_Click", value);
        }
    }

    /// <summary>
    /// Thoát App
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        // video click
        Debug.Log("Click video !!!");
        amountVideoRewardClick++;
    }

    public bool IsLoadedVideoReward()
    {
        if (rewardBasedVideo.IsLoaded())
            return true;

        return false;
    }

    /// <summary>
    /// Xử lý Show Video
    /// </summary>
    /// <param name="actionReward">Hành động khi xem xong Video và nhận thưởng </param>
    /// <param name="actionNotLoadedVideo"> Hành động báo lỗi không có video để xem </param>
    /// <param name="actionClose"> Hành động khi đóng video (Đóng lúc đang xem dở hoặc đã xem hết) </param>
    public void ShowVideoReward(UnityAction actionReward, UnityAction actionNotLoadedVideo, UnityAction actionClose, ActionWatchVideo actionType)
    {
        //gioi han click
        //if (amountVideoRewardClick > GameController.Instance.capingVideoReward)
        //    return;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ConfirmBox.Setup().AddMessageYes(Localization.Get("s_Noti"), Localization.Get("s_NoConnect"), () => { });
            return;
        }
        this.actionNotLoadedVideo = actionNotLoadedVideo;
        this.actionClose = actionClose;


#if UNITY_EDITOR
        this.actionRewardVideo = actionReward;
        if (actionRewardVideo != null)
            actionRewardVideo();
#else
        if (rewardBasedVideo.IsLoaded())
        {
            this.actionRewardVideo = actionReward;
            rewardBasedVideo.Show();
        }
        else
        {       
            if (this.interstitial.IsLoaded())
            {
                if (actionRewardVideo != null)
                    actionRewardVideo();
                ShowInterstitial();
            }
            else
            {
                ConfirmBox.Setup().AddMessageYes(Localization.Get("s_Noti"), Localization.Get("s_TryAgain"), () => { });
                return;
            }
        }
#endif
        
    }
    #endregion

    #region Banner
    private BannerView bannerView;

    private void InitBanner()
    {
        if (DataManager.RemoveAds != 0)
            return;


        RequestBanner(Config.Admob_Banner_ID);
    }

    public void OnBannerLoadSuccess(object sender, EventArgs args)
    {
        Debug.Log("Request success");
        ShowBanner();
    }

    private bool isLoadedBannerLow;
    public void HandleOnBannerFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Load Fail" + args.Message);
        if (!isLoadedBannerLow)
        {
            RequestBanner(Config.Admob_Banner_ID_LOW);
            isLoadedBannerLow = true;
        }
    }

    private void RequestBanner(string id)
    {
        if (DataManager.RemoveAds != 0)
            return;
        // Create a 320x50 banner at the top of the screen.
        Debug.Log("Banner_ID " + Config.Admob_Banner_ID);

        try
        {
            if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_DESTROY_REFRENCE_ADS, false))
            {
                if (this.bannerView != null)
                    this.bannerView.Destroy();
            }
        }
        catch
        {

        }

        if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_ADAPTIVE_BANNERS, false))
        {
            AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            bannerView = new BannerView(id, adaptiveSize, AdPosition.Bottom);
        }
        else
        {
            bannerView = new BannerView(id, AdSize.SmartBanner, AdPosition.Bottom);
        }

        
        bannerView.OnAdFailedToLoad += HandleOnBannerFailedToLoad;
        bannerView.OnAdLoaded += OnBannerLoadSuccess;
        bannerView.OnAdLeavingApplication += HandleBanerLeftApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
        Debug.Log("Request banner");
    }

    public int amountBanerClick
    {
        get
        {
            return PlayerPrefs.GetInt("Amount_Baner_Click", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Amount_Baner_Click", value);
        }
    }

    public void HandleBanerLeftApplication(object sender, EventArgs args)
    {
        //inter click
        Debug.Log("Click Baner !!!");
        amountBanerClick++;
        if (amountBanerClick > GameController.Instance.capingBaner)
        {
            bannerView.Hide();
        }
    }

    public void ShowBanner()
    {
        if (amountBanerClick > GameController.Instance.capingBaner)
        {
            bannerView.Hide();
            return;
        }

        if (DataManager.RemoveAds != 0)
            return;
        if (bannerView == null)
            return;
        bannerView.Show();
    }

    public void DestroyBanner()
    {
        if (DataManager.RemoveAds != 0)
            return;
        if (bannerView == null)
            return;
        bannerView.Destroy();
    }
    #endregion

    public DateTime toDayAds
    {
        get
        {
            if (!PlayerPrefs.HasKey("TODAY_ADS"))
                PlayerPrefs.SetString("TODAY_ADS", DateTime.Now.AddDays(-1).ToString());
            return DateTime.Parse(PlayerPrefs.GetString("TODAY_ADS"));
        }
        set
        {
            PlayerPrefs.SetString("TODAY_ADS", value.ToString());
        }
    }

    public void ResetCaping()
    {
        bool isPassday = TimeManager.IsPassTheDay(toDayAds, DateTime.Now);
        if (isPassday)
        {
            amountInterClick = 0;
            amountVideoRewardClick = 0;
            amountVideoRewardClick = 0;
            toDayAds = DateTime.Now;
        }
    }
}