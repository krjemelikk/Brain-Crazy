using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;
using YaSDK.Source.Data;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.YandexServices
{
   internal class YandexSDKProgress : SingletonBehaviour<YandexSDKProgress>, IProgressService
   {
      [DllImport("__Internal")]
      private static extern void SaveProgressExtern(string data);

      [DllImport("__Internal")]
      private static extern string LoadProgressExtern();

      private bool _isLoaded;

      public void Save()
      {
         Progress progress = YandexSDKData.Instance.Progress;
         var json = JsonConvert.SerializeObject(progress);
         SaveProgressExtern(json);
      }

      public IEnumerator LoadProgress()
      {
         LoadProgressExtern();
         yield return new WaitUntil(() => _isLoaded);
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

      private void OnProgressLoaded(string json)
      {
         YandexSDKData.Instance.Progress = String.IsNullOrEmpty(json) || json == "{}"
            ? new Progress()
            : JsonConvert.DeserializeObject<Progress>(json);

         _isLoaded = true;
      }
   }
}