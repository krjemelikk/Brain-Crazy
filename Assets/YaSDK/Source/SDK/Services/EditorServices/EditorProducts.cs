using System.Collections;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.EditorServices
{
   internal class EditorProducts : IProductDataService
   {
      public IEnumerator LoadProductData()
      {
#if UNITY_EDITOR
         YandexSDKData.Instance.ProductData.Products =
            YandexSDKData.Instance.ProductData.EditorProducts.ToDictionary();
#endif
         yield return null;
      }
   }
}