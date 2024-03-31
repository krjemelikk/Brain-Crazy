using System;

namespace YaSDK.Source.SDK.Services.Interfaces
{
   public interface IAdvertisementService
   {
      event Action RewardedAdShown;
      void ShowRewardedAd();

      void ShowInterstitialAd();

      void ShowAdBanner();

      void HideAdBanner();
   }
}