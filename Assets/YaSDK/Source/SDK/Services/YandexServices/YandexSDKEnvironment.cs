using System.Collections;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;
using YaSDK.Source.Data;
using YaSDK.Source.Data.JSON;
using YaSDK.Source.Enum;
using YaSDK.Source.SDK.Services.Interfaces;
using DeviceType = YaSDK.Source.Enum.DeviceType;

namespace YaSDK.Source.SDK.Services.YandexServices
{
   internal class YandexSDKEnvironment : SingletonBehaviour<YandexSDKEnvironment>, IEnvironmentService
   {
      [DllImport("__Internal")]
      private static extern string LoadEnvironmentDataExtern();

      private bool _isLoaded;

      public IEnumerator LoadEnvironmentData()
      {
         LoadEnvironmentDataExtern();
         yield return new WaitUntil(() => _isLoaded);
      }

      private void OnEnvironmentDataLoaded(string data)
      {
         var dataJson = JsonConvert.DeserializeObject<EnvironmentDataJson>(data);

         YandexSDKData.Instance.EnvironmentData = new EnvironmentData(
            dataJson.Device switch
            {
               "desktop" => DeviceType.Desktop,
               "mobile" => DeviceType.Mobile,
               "tablet" => DeviceType.Tablet,
               "tv" => DeviceType.TV,
               _ => DeviceType.Desktop
            },
            dataJson.Language is "ru" or "be" or "uk" or "kk" or "az" or "hy"
               ? Language.Russian
               : Language.English,
            dataJson.TopLevelDomain
         );

         _isLoaded = true;
      }
   }
}