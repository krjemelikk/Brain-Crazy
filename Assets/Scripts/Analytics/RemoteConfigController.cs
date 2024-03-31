using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class RemoteConfigController : MonoBehaviour
{
   #region Events and Delegates

   #endregion

   #region Variables

   private static bool LoadedConfig;
   private static bool LoadingConfig;
   private static bool isInit;

   #endregion

   #region Properties

   #endregion

   #region Unity Method

   #endregion

   #region Public Methods

   public static void FetchData()
   {
   }

   #endregion

   #region Private Methods

   static void FetchComplete(Task fetchTask)
   {
      if (fetchTask.IsCanceled)
      {
         DebugLog("Fetch canceled.");
      }
      else if (fetchTask.IsFaulted)
      {
         DebugLog("Fetch encountered an error.");
      }
      else if (fetchTask.IsCompleted)
      {
         DebugLog("Fetch completed successfully!");
      }
   }

   public static void RemoteConfigFirebaseInit(Dictionary<string, object> defaults)
   {
      if (isInit) return;
      isInit = true;
   }

   private static void DebugLog(string s)
   {
      // DebugCustom.LogEditor(s);
   }

   #endregion

   #region Get Config

   public static string GetStringConfig(string key, string defaultValue)
   {
      return defaultValue;
   }


   public static bool GetBoolConfig(string key, bool defaultValue)
   {
      return defaultValue;
   }


   public static float GetFloatConfig(string key, float defaultValue)
   {
      return defaultValue;
   }


   public static int GetIntConfig(string key, int defaultValue)
   {
      return defaultValue;
   }

   public static bool GetJsonConfig<T>(string key, out T result)
   {
      string input = String.Empty;

      try
      {
         result = JsonUtility.FromJson<T>(input);
         return true;
      }
      catch (Exception ex)
      {
         Debug.LogError($"GetJsonConfig {typeof(T)} , key {key}, exception: {ex.Message}");
         result = default;
         return false;
      }
   }

   #endregion
}

public class ReloadConfig
{
}