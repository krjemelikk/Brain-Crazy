using System;
using UnityEngine;
using YaSDK.Source.SDK;

public static class DataManager
{
   public static int Hint =>
      YandexSDK.Instance.ProgressService.GetInt(StringHelper.HINT,
         RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.DEFAULT_HINT, 2));

   public static void AddHint(int value)
   {
      var current = Hint + value;
      YandexSDK.Instance.ProgressService.SetInt(StringHelper.HINT, Mathf.Clamp(current, 0, int.MaxValue));
      YandexSDK.Instance.ProgressService.Save();
      EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.HINT_CHANGE);
   }

   public static DateTime LastTimeOpenGame
   {
      get
      {
         if (YandexSDK.Instance.ProgressService.HasKey(StringHelper.LAST_TIME_OPEN_GAME))
         {
            var temp = Convert.ToInt64(YandexSDK.Instance.ProgressService.GetString(StringHelper.LAST_TIME_OPEN_GAME));
            return DateTime.FromBinary(temp);
         }
         else
         {
            var newDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            YandexSDK.Instance.ProgressService.SetString(StringHelper.LAST_TIME_OPEN_GAME,
               newDateTime.ToBinary().ToString());
            YandexSDK.Instance.ProgressService.Save();
            return newDateTime;
         }
      }

      set
      {
         YandexSDK.Instance.ProgressService.SetString(StringHelper.LAST_TIME_OPEN_GAME, value.ToBinary().ToString());
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   /// <summary>
   /// Lần đầu chơi game
   /// </summary>
   public static bool FirstTimePlayGame
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.FIRST_TIME_PLAY_GAME, 1) == 1 ? true : false;
      set => YandexSDK.Instance.ProgressService.SetInt(StringHelper.FIRST_TIME_PLAY_GAME, value == true ? 1 : 0);
   }

   public static DateTime TimeInstallGame
   {
      get
      {
         if (YandexSDK.Instance.ProgressService.HasKey(StringHelper.TIME_INSTALL_GAME))
         {
            var temp = Convert.ToInt64(YandexSDK.Instance.ProgressService.GetString(StringHelper.TIME_INSTALL_GAME));
            return DateTime.FromBinary(temp);
         }
         else
         {
            var newDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            YandexSDK.Instance.ProgressService.SetString(StringHelper.TIME_INSTALL_GAME,
               newDateTime.ToBinary().ToString());
            YandexSDK.Instance.ProgressService.Save();
            return newDateTime;
         }
      }
      set
      {
         YandexSDK.Instance.ProgressService.SetString(StringHelper.TIME_INSTALL_GAME, value.ToBinary().ToString());
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   #region === Level ===

   public static int CurrentLevel
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.CURRENT_LEVEL, 1);
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.CURRENT_LEVEL, value);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static int GetHighestLevelUnlocked =>
      YandexSDK.Instance.ProgressService.GetInt(StringHelper.HIGHEST_LEVEL_UNLOCKED, 1);

   public static bool GetLevelUnlocked(int levelIndex)
   {
      return YandexSDK.Instance.ProgressService.GetInt(StringHelper.HIGHEST_LEVEL_UNLOCKED, 1) >= levelIndex;
   }

   public static void SetLevelUnlocked(int levelIndex)
   {
      YandexSDK.Instance.ProgressService.SetInt(StringHelper.HIGHEST_LEVEL_UNLOCKED, levelIndex);
      YandexSDK.Instance.ProgressService.Save();
   }

   public static bool GetLevelPassed(int levelIndex)
   {
      var key = $"{StringHelper.LEVEL_PASSED}{levelIndex}";
      return YandexSDK.Instance.ProgressService.GetInt(key, 0) == 1;
   }

   public static void SetLevelPassed(int levelIndex, bool isPassed)
   {
      var key = $"{StringHelper.LEVEL_PASSED}{levelIndex}";
      YandexSDK.Instance.ProgressService.SetInt(key, isPassed ? 1 : 0);
      YandexSDK.Instance.ProgressService.Save();
   }

   #endregion

   #region === On/Off Sound ===

   public static int OnOffMusic
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.ONOFF_MUSIC, 1);
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.ONOFF_MUSIC, value);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static int OnOffSound
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.ONOFF_SOUND, 1);
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.ONOFF_SOUND, value);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   #endregion

   #region === Daily Reward ===

   public static int DailyRewardID
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.DAILY_REWARD_ID, -1);
      set
      {
         if (YandexSDK.Instance.ProgressService.GetInt(StringHelper.DAILY_REWARD_ID, -1) >= 6)
         {
            YandexSDK.Instance.ProgressService.SetInt(StringHelper.DAILY_REWARD_ID, -1);
         }
         else
         {
            YandexSDK.Instance.ProgressService.SetInt(StringHelper.DAILY_REWARD_ID, value);
         }

         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static bool CanClaimDailyRewardToday
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.CAN_CLAIM_DAILY_REWARD, 0) == 0;
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.CAN_CLAIM_DAILY_REWARD, value == false ? 1 : 0);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   #endregion

   #region === Remove Ads ===

   public static int RemoveAds
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.REMOVE_ADS, 0);
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.REMOVE_ADS, value);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static bool IsCanShowAds()
   {
      return YandexSDK.Instance.ProgressService.GetInt(StringHelper.REMOVE_ADS, 0) == 0;
   }

   #endregion

   #region === Tutorial ===

   public static bool SuggestedHint
   {
      get { return YandexSDK.Instance.ProgressService.GetInt(StringHelper.SUGGESTED_HINT, 0) == 1; }
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.SUGGESTED_HINT, value ? 1 : 0);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   #endregion

   #region === Settings ===

   public static bool OnVibration
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.ONOFF_VIBRATION, 1) == 1;
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.ONOFF_VIBRATION, value ? 1 : 0);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static bool OnSound
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.ONOFF_SOUND, 1) == 1;
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.ONOFF_SOUND, value ? 1 : 0);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static bool OnMusic
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.ONOFF_MUSIC, 1) == 1;
      set
      {
         YandexSDK.Instance.ProgressService.SetInt(StringHelper.ONOFF_MUSIC, value ? 1 : 0);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   #endregion

   #region === Show Rate ===

   public static bool CanShowRate
   {
      get => YandexSDK.Instance.ProgressService.GetInt(StringHelper.CAN_SHOW_RATE, 1) == 1 ? true : false;
      set => YandexSDK.Instance.ProgressService.SetInt(StringHelper.CAN_SHOW_RATE, value == true ? 1 : 0);
   }

   #endregion

   #region === Show RewardLucky ===

   public static DateTime TimeLastRandomMaxHint
   {
      get =>
         System.DateTime.Parse(YandexSDK.Instance.ProgressService.GetString(
            StringHelper.TIME_LAST_RANDOM_MAX_HINT,
            UnbiasedTime.Instance.Now.AddDays(-1).ToString()));
      set
      {
         YandexSDK.Instance.ProgressService.SetString(StringHelper.TIME_LAST_RANDOM_MAX_HINT, value.ToString());
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static DateTime TimeOpenRewardLucky
   {
      get =>
         System.DateTime.Parse(YandexSDK.Instance.ProgressService.GetString(StringHelper.TIME_OPEN_REWARDLUCKY,
            UnbiasedTime.Instance.Now.AddDays(-1).ToString()));
      set
      {
         YandexSDK.Instance.ProgressService.SetString(StringHelper.TIME_OPEN_REWARDLUCKY, value.ToString());
         YandexSDK.Instance.ProgressService.Save();
      }
   }


   public static float TimeDelayShowRewardLucky =>
      RemoteConfigController.GetFloatConfig(StringHelper.ConfigFirebase.TIME_DELAY_SHOW_REWARD_LUCKY_END_GAME,
         3.5f); //3.5 phút mặc định

   #endregion

   #region === Share ====

   public static int TimeDelayShareAddHint =>
      RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.TIME_DELAY_SHARE_ADD_HINT, 86400);

   public static DateTime TimeLastShareAddHint
   {
      get =>
         System.DateTime.Parse(YandexSDK.Instance.ProgressService.GetString(
            StringHelper.TIME_LAST_SHARE_ADD_HINT,
            UnbiasedTime.Instance.Now.AddDays(-1).ToString()));
      set
      {
         YandexSDK.Instance.ProgressService.SetString(StringHelper.TIME_LAST_SHARE_ADD_HINT, value.ToString());
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   #endregion

   public static bool IsRated_5_Star
   {
      get { return YandexSDK.Instance.ProgressService.GetInt(StringHelper.RATED_5_STAR, 0) == 1 ? true : false; }
      set { YandexSDK.Instance.ProgressService.SetInt(StringHelper.RATED_5_STAR, value == true ? 1 : 0); }
   }

   #region Update version

   public static int RememberCurrentVersion
   {
      get => YandexSDK.Instance.ProgressService.GetInt("Remember_Current_Version", 0);
      set
      {
         YandexSDK.Instance.ProgressService.SetInt("Remember_Current_Version", value);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static DateTime LastTimeShowPopupUpdate
   {
      get =>
         System.DateTime.Parse(YandexSDK.Instance.ProgressService.GetString(
            StringHelper.TIME_LAST_SHOW_POPUP_UPDATE,
            UnbiasedTime.Instance.Now.AddDays(-1).ToString()));
      set
      {
         YandexSDK.Instance.ProgressService.SetString(StringHelper.TIME_LAST_SHOW_POPUP_UPDATE, value.ToString());
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static int NewestVersionInStoreCache
   {
      get => YandexSDK.Instance.ProgressService.GetInt("newest_version_in_store", 0);
      set
      {
         YandexSDK.Instance.ProgressService.SetInt("newest_version_in_store", value);
         YandexSDK.Instance.ProgressService.Save();
      }
   }

   public static bool IsDontShowAgain_PopupUpdate
   {
      get => YandexSDK.Instance.ProgressService.GetInt("IsDontShowAgain_PopupUpdate", 0) == 1 ? true : false;
      set => YandexSDK.Instance.ProgressService.SetInt("IsDontShowAgain_PopupUpdate", value == true ? 1 : 0);
   }

   public static bool IsDontShowAgainPopupUpdate(int NewVersion)
   {
      if (NewVersion <= NewestVersionInStoreCache)
         return IsDontShowAgain_PopupUpdate;

      IsDontShowAgain_PopupUpdate = false;
      NewestVersionInStoreCache = NewVersion;
      return false;
   }

   #endregion
}