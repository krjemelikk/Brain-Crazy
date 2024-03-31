using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using YaSDK.Source.Data;
using YaSDK.Source.Data.JSON;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.YandexServices
{
   internal class YandexSDKProducts : SingletonBehaviour<YandexSDKProducts>, IProductDataService
   {
      [DllImport("__Internal")]
      private static extern string LoadProductDataExtern();

      private bool _isLoaded;

      public IEnumerator LoadProductData()
      {
         LoadProductDataExtern();
         yield return new WaitUntil(() => _isLoaded);
      }

      private void OnProductDataLoaded(string data)
      {
         var dataJson = JsonUtility.FromJson<ProductDataJson>(data);
         YandexSDKData.Instance.ProductData = new ProductData(
            dataJson.Products.ToDictionary(x => x.Id, x => x),
            dataJson.CurrencyImageURL);

         _isLoaded = true;
      }
   }
}