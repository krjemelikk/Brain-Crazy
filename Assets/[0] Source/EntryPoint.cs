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
      StartCoroutine(InitializeServices(LoadMainScene));
   }

   private IEnumerator InitializeServices(Action callback)
   {
      yield return YandexSDK.Instance.EnvironmentService.LoadEnvironmentData();
      yield return YandexSDK.Instance.ProgressService.LoadProgress();
   }

   private void LoadMainScene() =>
      SceneManager.LoadScene("MainHome");
}