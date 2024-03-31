using System;
using System.Collections.Generic;

namespace YaSDK.Source.Data.JSON
{
   [Serializable]
   public class ProductDataJson
   {
      public string CurrencyImageURL;
      
      public List<ProductData.Product> Products;
   }
}