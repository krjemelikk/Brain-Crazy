public static class StringHelper
{
    public const string HINT = "HINT";
    public const string LAST_TIME_OPEN_GAME = "LAST_TIME_OPEN_GAME";
    public const string ONOFF_SOUND = "ONOFF_SOUND";
    public const string ONOFF_MUSIC = "ONOFF_MUSIC";
    public const string REMOVE_ADS = "REMOVE_ADS";
    public const string DAILY_REWARD_ID = "DAILY_REWARD_ID";
    public const string CAN_CLAIM_DAILY_REWARD = "CAN_CLAIM_DAILY_REWARD";
    public const string CURRENT_LEVEL = "CURRENT_LEVEL";
    public const string LEVEL_PASSED = "LEVEL_PASSED_";
    public const string HIGHEST_LEVEL_UNLOCKED = "HIGHEST_LEVEL_UNLOCKED";
    public const string QUESTION_LEVEL = "ques_level_";
    public const string HINT_LEVEL = "hint_level_";
    public const string TIPS_LEVEL = "tips_level_";
    public const string SUGGESTED_HINT = "SUGGESTED_HINT";
    public const string ONOFF_VIBRATION = "ONOFF_VIBRATION";
    public const string FIRST_TIME_PLAY_GAME = "frist_time_play_game";
    public const string TIME_OPEN_REWARDLUCKY = "TIME_OPEN_REWARDLUCKY";
    public const string TIME_LAST_RANDOM_MAX_HINT = "TIME_LAST_RANDOM_MAX_HINT";
    public const string TIME_LAST_SHARE_ADD_HINT = "TIME_LAST_SHARE_ADD_HINT";
    public const string TIME_LAST_SHOW_POPUP_UPDATE = "TIME_LAST_SHOW_POPUP_UPDATE";
    public const string TIME_INSTALL_GAME = "TIME_INSTALL_GAME";

    public const string CAPING_INTER_CLICK = "CAPING_INTER_CLICK";
    public const string CAPING_VIDEOREWARD_CLICK = "CAPING_VIDEOREWARD_CLICK";
    public const string CAPING_BANER_CLICK = "CAPING_BANER_CLICK";

    public static string QuestionLevel(int id)
    {
        return $"{QUESTION_LEVEL}{id}";
    }

    public static string HintLevel(int id)
    {
        return $"{HINT_LEVEL}{id}";
    }

    public static string TipsLevel(int id)
    {
        return $"{TIPS_LEVEL}{id}";
    }

    public const string VERSION_FIRST_INSTALL = "first_install_version";

    public const string SALE_IAP = "saleIAP";

    public const string CAN_SHOW_RATE = "can_show_rate";

    public const string RATED_5_STAR = "rated_5_star";

    public static string StringColor(string str, ColorString color)
    {
        switch (color)
        {
            case ColorString.yellow:
                return $"<color=yellow>{str}</color>";
            case ColorString.green:
                return $"<color=green>{str}</color>";
            case ColorString.red:
                return $"<color=red>{str}</color>";
            default:
                return str;
        }
    }

    public static string StringColorHexan(string str, string hexan)
    {
        return $"<color=\"{hexan}\">{str}</color>";
    }

    public class LocalSave
    {
        public const string ACTION_CLICK = "actionClick_";
        public const string TIME_PLAY = "timePlay_";
        public const string HINT_LEVEL = "hint_level_";
        public const string ADD_HINT = "add_hint_";
    }

    public class ConfigFirebase
    {
        public const string LEVEL_SHOW_REWARD_GAME = "level_show_reward_in_level";
        public const string TIME_APPEAR_REWARD = "time_appear_reward_in_level";
        public const string DELAY_SHOW_REWARD = "delay_show_reward_in_level";
        public const string TIME_DELAY_SHOW_REWARD_LUCKY_END_GAME = "delay_show_reward_end_game";
        public const string DELAY_SHOW_INITSTIALL = "delay_show_initi_ads";
        public const string LEVEL_SHOW_REWARD_END_GAME = "level_show_reward_end_game";
        public const string TIME_RELOAD_BANNER = "time_reload_banner";

        public const string PERCENT_3_LUCKY_BOX = "percent_3_lucky_box";
        public const string PERCENT_2_LUCKY_BOX = "percent_2_lucky_box";

        public const string LEVEL_SHOW_FIRST_TIME_DAILY_REWARD = "level_show_first_time_daily_reward";

        public const string ON_OFF_SHARE_ADD_HINT = "on_share_add_hint";
        public const string TIME_DELAY_SHARE_ADD_HINT = "time_delay_share_add_hint";

        public const string CONFIG_PUSH_NOTI_OFFLINE = "config_push_noti_offline";

        public const string DEFAULT_HINT = "default_hint";
        public const string ON_OFF_RATE_LOW = "on_rate_low";

        public const string PERCENT_ADD_REWARD_LEVEL = "percent_add_reward_level";
        public const string PERCENT_REWARD_3_ACCUMULATE = "percent_reward_3_accumulate";
        public const string NUM_REWARD_ACCUMULATE = "num_reward_accumulate";

        public const string LEVELS_SHOW_RATE = "levels_show_rate";

        public const string ON_OFF_MIX_BUTTON_NEXT = "on_off_mix_button_next";
        public const string DELAY_LEVEL_MIX_BUTTON_NEXT = "delay_level_mix_button_next";

        public const string ON_OFF_TUT_RATE = "on_off_tut_rate";
        public const string ON_OFF_POPUP_UPDATE_NEW_VERSION = "on_off_update_new_version";

        public const string ON_OFF_DESTROY_REFRENCE_ADS = "on_off_reference_ads";
        public const string ON_OFF_ADAPTIVE_BANNERS = "on_off_adaptive_banners";
    }
}

public class PathPrefabs
{
    public const string PANEL_DAILY_REWARD = "UI/Panel_DailyReward";
    public const string PANEL_IAP = "UI/Panel_Shop";
    public const string PANEL_HINT = "UI/Panel_Hint";
    public const string PANEL_MORE_HINT = "UI/Panel_MoreHint";
    public const string PANEL_CONGRATULATION = "UI/Panel_Congratulation";
    public const string CONFIRM_POPUP = "UI/ConfirmBox";
    public const string WAITING_BOX = "UI/WaitingBox";
    public const string RECEVER_IAP = "UI/RewardIAPBox";
    public const string RATE_BOX = "UI/RateBox";
    public const string QUIT_BOX = "UI/QuitBox";
    public const string LANGUAGE_BOX = "UI/LanguageBox";
    public const string REWARDLUCKYBOX = "UI/RewardLuckyBox";
    public const string SKIP_BOX = "UI/SkipBox";
    public const string REWARD_SHOW_BOX = "UI/RewardShowBox";
    public const string UPDATE_NEW_VERSION_BOX = "UI/UpdateNewVersionBox";
}

public class Tag
{
    public const string EARTH_TAG = "Earth";
    public const string METEORITE = "Meteorite";
    public const string SHIELD_TAG = "shield";
}

public enum ColorString
{
    yellow,
    green,
    red
}



