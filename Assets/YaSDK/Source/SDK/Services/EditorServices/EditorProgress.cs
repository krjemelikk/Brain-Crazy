using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using YaSDK.Source.SDK.Services.Interfaces;
using Progress = YaSDK.Source.Data.Progress;

namespace YaSDK.Source.SDK.Services.EditorServices
{
   internal class EditorProgress : IProgressService
   {
      public void Save()
      {
         Progress progress = YandexSDKData.Instance.Progress;
         var json = JsonConvert.SerializeObject(progress);
         PlayerPrefs.SetString("Progress", json);
      }

      public IEnumerator LoadProgress()
      {
         if (PlayerPrefs.HasKey("Progress"))
         {
            YandexSDKData.Instance.Progress =
               JsonConvert.DeserializeObject<Progress>(PlayerPrefs.GetString("Progress"));
            yield return null;
         }

         YandexSDKData.Instance.Progress = new Progress();
         yield return null;
      }

      public bool HasKey(string key)
      {
         return YandexSDKData.Instance.Progress.IntData.ContainsKey(key) ||
                YandexSDKData.Instance.Progress.FloatData.ContainsKey(key) ||
                YandexSDKData.Instance.Progress.StringData.ContainsKey(key);
      }

      public int GetInt(string key, int defaultValue) =>
         YandexSDKData.Instance.Progress.IntData.GetValueOrDefault(key, defaultValue);

      public float GetFloat(string key, float defaultValue) =>
         YandexSDKData.Instance.Progress.FloatData.GetValueOrDefault(key, defaultValue);

      public string GetString(string key, string defaultValue) =>
         YandexSDKData.Instance.Progress.StringData.GetValueOrDefault(key, defaultValue);

      public void SetInt(string key, int value) =>
         YandexSDKData.Instance.Progress.IntData[key] = value;

      public void SetFloat(string key, float value) =>
         YandexSDKData.Instance.Progress.FloatData[key] = value;

      public void SetString(string key, string value) =>
         YandexSDKData.Instance.Progress.StringData[key] = value;

#if UNITY_EDITOR
      [MenuItem("YandexSDK/Clean editor progress")]
      public static void CleanUp()
      {
         PlayerPrefs.DeleteAll();
      }
#endif
   }
}