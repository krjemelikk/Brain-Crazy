using System;
using System.Runtime.InteropServices;
using UnityEngine;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.YandexServices
{
   internal class YandexSDKPurchases : SingletonBehaviour<YandexSDKPurchases>, IPurchaseService
   {
      [DllImport("__Internal")]
      private static extern void PurchaseExtern(string id, bool withConsume);

      [DllImport("__Internal")]
      private static extern void CheckPurchaseExtern(string id, bool withConsume);

      [DllImport("__Internal")]
      private static extern void ConsumePurchaseExtern(string token);

      public event Action<string> PurchaseComplete;

      public void Purchase(string id, bool withConsume) =>
         PurchaseExtern(id, withConsume);

      public void CheckPurchase(string id, bool withConsume) =>
         CheckPurchaseExtern(id, withConsume);

      public void ConsumePurchase(string token) =>
         ConsumePurchaseExtern(token);

      private void OnPurchaseComplete(string purchaseData)
      {
         var data = JsonUtility.FromJson<PurchaseData>(purchaseData);
         PurchaseComplete?.Invoke(data.Id);
      }

      private void OnPurchaseWithConsumeComplete(string purchaseData)
      {
         var data = JsonUtility.FromJson<PurchaseData>(purchaseData);
         PurchaseComplete?.Invoke(data.Id);
         ConsumePurchaseExtern(data.Token);
      }
   }

   [Serializable]
   public class PurchaseData
   {
      public string Id;
      public string Token;
   }
}