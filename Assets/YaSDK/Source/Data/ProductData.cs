using System;
using System.Collections.Generic;
using YaSDK.Source.SDK.Services.EditorServices.Data;

namespace YaSDK.Source.Data
{
   [Serializable]
   public class ProductData
   {
      public string CurrencyImageURL;

      public Dictionary<string, Product> Products;

#if UNITY_EDITOR
      public SerializedDictionary<string, Product> EditorProducts;
#endif

      public ProductData(Dictionary<string, Product> products, string currencyImageURL)
      {
         Products = products;
         CurrencyImageURL = currencyImageURL;
      }

      public class Product
      {
         public string Id;
         public string Tittle;
         public string Price;
         public string ProductImageURL;
      }
   }
}