using System;
using UnityEngine;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.EditorServices
{
   internal class EditorPurchases : IPurchaseService
   {
      public event Action<string> PurchaseComplete;

      public void Purchase(string id, bool withConsume)
      {
         Debug.Log($"<color=blue>Purchase: </color>Purchase");
         PurchaseComplete?.Invoke(id);
      }

      public void CheckPurchase(string id, bool withConsume)
      {
         Debug.Log($"<color=blue>Purchase: </color>Check Purchase");
      }

      public void ConsumePurchase(string token)
      {
         Debug.Log($"<color=blue>Purchase: </color>Consume Purchase with token {token}");
      }
   }
}