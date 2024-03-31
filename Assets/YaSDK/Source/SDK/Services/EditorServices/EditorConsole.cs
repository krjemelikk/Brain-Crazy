using UnityEngine;
using YaSDK.Source.Enum;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.EditorServices
{
   internal class EditorConsole : IConsole
   {
      public void Log(string message, MessageType messageType = MessageType.Info, string filePath = "", int lineNumber = 0)
      {
         Debug.Log($"<color=yellow>Console Message: {messageType.ToString()} </color> {message} " +
                   $"\n File: {filePath} " +
                   $"\n Line: {lineNumber}");
      }
   }
}