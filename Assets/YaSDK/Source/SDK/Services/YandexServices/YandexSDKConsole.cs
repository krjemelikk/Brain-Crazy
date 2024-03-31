using System.Linq;
using System.Runtime.InteropServices;
using YaSDK.Source.Enum;
using YaSDK.Source.Extensions;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.YandexServices
{
   internal class YandexSDKConsole : SingletonBehaviour<YandexSDKConsole>, IConsole
   {
      [DllImport("__Internal")]
      private static extern void ConsoleLogExtern(
         string message,
         string messageType,
         string messageColor,
         string callerInfo);

      public void Log(
         string message,
         MessageType messageType = MessageType.Info,
         [System.Runtime.CompilerServices.CallerFilePath]
         string filePath = "",
         [System.Runtime.CompilerServices.CallerLineNumber]
         int lineNumber = 0)
      {
         ConsoleLogExtern(
            message,
            messageType.ToString(),
            messageType.ToColor().ToString(),
            $"({filePath.Split('\\').LastOrDefault()}:{lineNumber})".ToString()
         );
      }
   }
}