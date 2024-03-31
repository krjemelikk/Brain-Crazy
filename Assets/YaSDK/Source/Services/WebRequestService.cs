using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using YaSDK.Source.Enum;
using YaSDK.Source.SDK.Services.YandexServices;

namespace YaSDK.Source.Services
{
   public class WebRequestService : SingletonBehaviour<WebRequestService>
   {
      public void DownloadImage(string url, Action<Texture> callback) =>
         StartCoroutine(DownloadImageRoutine(url, callback));

      private IEnumerator DownloadImageRoutine(string url, Action<Texture> callback)
      {
         UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
         yield return request.SendWebRequest();

         if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
         {
            YandexSDKConsole.Instance.Log(request.error, MessageType.Error);
            yield break;
         }

         Texture texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
         callback?.Invoke(texture);
      }
   }
}