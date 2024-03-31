using System;

namespace YaSDK.Source.SDK.Services.Interfaces
{
   public interface IPurchaseService
   {
      event Action<string> PurchaseComplete;

      void Purchase(string id, bool withConsume);

      public void CheckPurchase(string id, bool withConsume);

      public void ConsumePurchase(string token);
   }
}