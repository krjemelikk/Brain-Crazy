using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YaSDK.Source.SDK;

public class EntryPoint : MonoBehaviour
{
   private void Start()
   {
      YandexSDK.Instance.Initialize();
      YandexSDK.Instance.Console.Log("Initialized SDK");
      StartCoroutine(InitializeServices(LoadMainScene));
   }

   private IEnumerator InitializeServices(Action callback)
   {
      yield return YandexSDK.Instance.EnvironmentService.LoadEnvironmentData();
      yield return YandexSDK.Instance.ProgressService.LoadProgress();
      YandexSDK.Instance.Console.Log("Initialize Services");
      callback?.Invoke();
   }

   private void LoadMainScene()
   {
      YandexSDK.Instance.ProgressService.SetInt("HIGHEST_LEVEL_UNLOCKED", 150);
      YandexSDK.Instance.AdvertisementService.ShowInterstitialAd();
      SceneManager.LoadScene("MainHome");
   }
}