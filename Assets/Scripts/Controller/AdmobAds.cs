using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;
using YaSDK.Source.SDK;


//  ----------------------------------------------
//  Author:     CuongCT <caothecuong91@gmail.com> 
//  Copyright (c) 2016 OneSoft JSC
// ----------------------------------------------

public class AdmobAds : MonoBehaviour
{
   private RewardBasedVideoAd rewardBasedVideo;

   public void Init()
   {
      InitBanner();
      InitRewardVideo();
   }

   #region Interstitial

   public void ShowInterstitial() =>
      YandexSDK.Instance.AdvertisementService.ShowInterstitialAd();

   #endregion

   #region Video Reward

   private UnityAction _actionClose;
   private UnityAction _actionRewardVideo;
   private UnityAction _actionNotLoadedVideo;

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
      if (_actionNotLoadedVideo != null)
         _actionNotLoadedVideo();

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
      if (_actionClose != null)
         _actionClose();

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
      if (_actionRewardVideo != null)
         _actionRewardVideo();
   }

   public int amountVideoRewardClick
   {
      get { return PlayerPrefs.GetInt("Amount_VideoReward_Click", 0); }
      set { PlayerPrefs.SetInt("Amount_VideoReward_Click", value); }
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
   public void ShowVideoReward(
      UnityAction actionReward, 
      UnityAction actionNotLoadedVideo, 
      UnityAction actionClose,
      ActionWatchVideo actionType)
   {
      YandexSDK.Instance.AdvertisementService.RewardedAdShown += Action;
      YandexSDK.Instance.AdvertisementService.ShowRewardedAd();

      void Action()
      {
         actionReward?.Invoke();
         actionClose?.Invoke();
      }
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
      get { return PlayerPrefs.GetInt("Amount_Baner_Click", 0); }
      set { PlayerPrefs.SetInt("Amount_Baner_Click", value); }
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
}