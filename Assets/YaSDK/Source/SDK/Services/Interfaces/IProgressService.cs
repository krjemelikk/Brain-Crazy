using System.Collections;

namespace YaSDK.Source.SDK.Services.Interfaces
{
   public interface IProgressService
   {
      void Save();
      IEnumerator LoadProgress();

      void SetInt(string key, int value);
      void SetFloat(string key, float value);
      void SetString(string key, string value);

      int GetInt(string key, int defaultValue = 0);
      float GetFloat(string key, float defaultValue = 0);
      string GetString(string key, string defaultValue = null);

      bool HasKey(string key);
   }
}