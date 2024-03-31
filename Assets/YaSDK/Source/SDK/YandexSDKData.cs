using UnityEngine;
using YaSDK.Source.Data;

namespace YaSDK.Source.SDK
{
   public class YandexSDKData : SingletonBehaviour<YandexSDKData>
   {
      [field: SerializeField] public EnvironmentData EnvironmentData { get; set; }
      [field: SerializeField] public ProductData ProductData { get; set; }
      [field: SerializeField] public Progress Progress { get; set; }
   }
}