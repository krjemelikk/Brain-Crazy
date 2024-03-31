using System;
using System.Collections.Generic;

namespace YaSDK.Source.Data
{
   [Serializable]
   public class Progress
   {
      public Dictionary<string, int> IntData { get; private set; } = new();
      public Dictionary<string, float> FloatData { get; private set; } = new();
      public Dictionary<string, string> StringData { get; private set; } = new();

      public Progress()
      {
      }
   }
}