using System;
using UnityEngine;
using YaSDK.Source.Enum;
using DeviceType = YaSDK.Source.Enum.DeviceType;

namespace YaSDK.Source.Data
{
   [Serializable]
   public class EnvironmentData
   {
      [field: SerializeField] public DeviceType DeviceType { get; private set; }
      [field: SerializeField] public Language Language { get; private set; }
      [field: SerializeField] public string TopLevelDomain { get; private set; }

      public EnvironmentData(DeviceType deviceType, Language language, string topLevelDomain)
      {
         DeviceType = deviceType;
         Language = language;
         TopLevelDomain = topLevelDomain;
      }
   }
}