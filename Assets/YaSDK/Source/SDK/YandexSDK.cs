using UnityEngine;
using YaSDK.Source.SDK.Services.EditorServices;
using YaSDK.Source.SDK.Services.Interfaces;
using YaSDK.Source.SDK.Services.YandexServices;

namespace YaSDK.Source.SDK
{
   public class YandexSDK : SingletonBehaviour<YandexSDK>
   {
      public IConsole Console;
      public IGameReadyAPIService GameReadyService;
      public IAdvertisementService AdvertisementService;
      public IEnvironmentService EnvironmentService;
      public ILeaderboardService LeaderboardService;
      public IPurchaseService PurchaseService;
      public IProgressService ProgressService;
      public IProductDataService ProductsService;

      private void Awake() =>
         DontDestroyOnLoad(this);

      public void Initialize()
      {
#if UNITY_EDITOR
         Console = new EditorConsole();
         GameReadyService = new EditorGameReadyAPI();
         AdvertisementService = new EditorAdvertisement();
         EnvironmentService = new EditorEnvironment();
         LeaderboardService = new EditorLeaderboard();
         PurchaseService = new EditorPurchases();
         ProgressService = new EditorProgress();
         ProductsService = new EditorProducts();
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
         Console = gameObject.AddComponent<YandexSDKConsole>();
         GameReadyService = gameObject.AddComponent<YandexSDKGameReadyAPI>();
         AdvertisementService = gameObject.AddComponent<YandexSDKAdvertisement>();
         EnvironmentService = gameObject.AddComponent<YandexSDKEnvironment>();
         LeaderboardService = gameObject.AddComponent<YandexSDKLeaderboard>();
         PurchaseService = gameObject.AddComponent<YandexSDKPurchases>();
         ProgressService = gameObject.AddComponent<YandexSDKProgress>();
         ProductsService = gameObject.AddComponent<YandexSDKProducts>();
#endif
      }

      public void PauseGame()
      {
         Time.timeScale = 0;
         AudioListener.pause = true;
      }

      public void UnPauseGame()
      {
         Time.timeScale = 1;
         AudioListener.pause = false;
      }
   }
}