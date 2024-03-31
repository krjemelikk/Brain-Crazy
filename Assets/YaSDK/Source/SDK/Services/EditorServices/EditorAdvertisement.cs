using System;
using UnityEngine;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.EditorServices
{
   internal class EditorAdvertisement : IAdvertisementService
   {
      public event Action RewardedAdShown;

      public void ShowRewardedAd()
      {
         Debug.Log("<color=blue>Advertisement: </color>Rewarded ad shown");
         RewardedAdShown?.Invoke();
      }

      public void ShowInterstitialAd() =>
         Debug.Log("<color=blue>Advertisement: </color>Interstitial ad shown");

      public void ShowAdBanner() =>
         Debug.Log("<color=blue>Advertisement: </color>Banner shown");

      public void HideAdBanner() =>
         Debug.Log("<color=blue>Advertisement: </color>Banner hidden");
   }
}