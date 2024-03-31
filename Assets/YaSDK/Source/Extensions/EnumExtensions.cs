using UnityEngine;
using YaSDK.Source.Enum;

namespace YaSDK.Source.Extensions
{
   public static class EnumExtensions
   {
      public static Color ToColor(this MessageType messageType)
      {
         return messageType switch
         {
            MessageType.Info => Color.blue * 256,
            MessageType.Success => Color.green * 256,
            MessageType.Warning => Color.yellow * 256,
            MessageType.Error => Color.red * 256,
            _ => Color.blue * 256
         };
      }
   }
}