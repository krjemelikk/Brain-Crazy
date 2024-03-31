using UnityEngine;

public class Config
{
    public static string settingProductName = "Brain Test: IQ Challenge";

    public const string settingKeyStore = "BrainTest";
    public static string keyaliasPass = "com.monagame.braintest";
    public static string keystorePass = "com.monagame.braintest";
    public static string settingAliasName = "BrainTest";

    public const string settingLogo = "GAME_ICON";

    public static int versionCode = 1;//sua
    public static string versionName = "Version:1.0";//sua
    public static int settingVersionCode = 1;//sua
    public static string settingVersionName = "1.0";//sua
    

    public static int VersionCodeAll
    {
        get
        {
            return versionCode / 100;
        }
    }

    public static int VersionFirstInstall
    {
        get
        {
            int data = PlayerPrefs.GetInt(StringHelper.VERSION_FIRST_INSTALL, 0);
            if (data == 0)
            {
                PlayerPrefs.SetInt(StringHelper.VERSION_FIRST_INSTALL, versionCode);
                data = versionCode;
            }

            return data;
        }
    }

    public static string inappAndroidKeyHash
        = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgB2CXR5gyZu3nfRq0b7xd29OcG6H8rpeZs74Thg8ql0OAQAaVh7rh4EHHwh9/QWbl3euU/2JG450A3z7ReNA4QFH/8Loeyvuoe7sX8nmovua3rHJagvjWe5BcpXd8aG5xFFe+coAFJ/pEO5VIPCWpX02fbC2nkXSlT5f+UmusDp5Dvemahf7tLDmHQ5V2rX6VIX1RM5i7Az/5ek/s0J1C08EfBS8Nk6e7NAw7JsdP3AuEuJwexxWNLBrADlfqf3Cy50W01+D5Q3fO8b7KHp9sTKb1chBIuIawbxv04X4j3Lst+H49MKwN/8j3c7DSWPeLESsleoL6fJMq2IYOxyFBQIDAQAB";
#if UNITY_ANDROID
    public const string package_name = "com.monagame.braintest";
#else
    public const string package_name = "com.monagame.braintest";
#endif

#if UNITY_ANDROID && !TEST_BUILD
    public static string AdmobId = "ca-app-pub-7694563285759760~6237146263";
#elif UNITY_IOS && !TEST_BUILD
     public static string AdmobId = "ca-app-pub-7694563285759760~6237146263";
#elif TEST_BUILD
    public static string AdmobId = "unexpected_platform";
#endif

#if UNITY_ANDROID && !TEST_BUILD
    public static string Admob_Interstitial_ID = "ca-app-pub-7694563285759760/5958585293";
#elif UNITY_IOS && !TEST_BUILD
    public static string Admob_Interstitial_ID = "ca-app-pub-7694563285759760/5958585293";
#elif TEST_BUILD
    public static string Admob_Interstitial_ID = "ca-app-pub-7694563285759760/5958585293";
#endif

#if UNITY_ANDROID && !TEST_BUILD
    public static string Admob_Banner_ID = "";
#elif UNITY_IOS && !TEST_BUILD
     public static string Admob_Banner_ID = "";
#elif TEST_BUILD
    public static string Admob_Banner_ID = "";
#endif

#if UNITY_ANDROID && !TEST_BUILD
    public static string Admob_Reward_ID = "ca-app-pub-7694563285759760/4645503627";
#elif UNITY_IOS && !TEST_BUILD
    public static string Admob_Reward_ID = "ca-app-pub-7694563285759760/4645503627";
#elif TEST_BUILD
    public static string Admob_Reward_ID = "ca-app-pub-7694563285759760/4645503627";
#endif


#if UNITY_ANDROID && !TEST_BUILD
    public static string Admob_Interstitial_ID_LOW = "ca-app-pub-7694563285759760/5958585293";
#elif UNITY_IOS && !TEST_BUILD
    public static string Admob_Interstitial_ID_LOW = "ca-app-pub-7694563285759760/5958585293";
#elif TEST_BUILD
    public static string Admob_Interstitial_ID_LOW = "ca-app-pub-7694563285759760/5958585293";
#endif

#if UNITY_ANDROID && !TEST_BUILD
    public static string Admob_Banner_ID_LOW = "";
#elif UNITY_IOS && !TEST_BUILD
     public static string Admob_Banner_ID_LOW = "";
#elif TEST_BUILD
    public static string Admob_Banner_ID_LOW = "";
#endif
    

#if UNITY_ANDROID && !TEST_BUILD
    public static string Admob_Reward_ID_LOW = "ca-app-pub-7694563285759760/4645503627";
#elif UNITY_IOS && !TEST_BUILD
    public static string Admob_Reward_ID_LOW = "ca-app-pub-7694563285759760/4645503627";
#elif TEST_BUILD
    public static string Admob_Reward_ID_LOW = "ca-app-pub-7694563285759760/4645503627";
#endif


#if UNITY_ANDROID
    public static string OPEN_LINK_RATE = "market://details?id=" + package_name;
#else
    public static string OPEN_LINK_RATE = "itms-apps://itunes.apple.com/app/";
#endif

    public static string FanpageLinkWeb = "https://www.facebook.com/";
    public static string FanpageLinkApp = "https://www.facebook.com/";

    public static string LinkFeedback = "https://www.facebook.com/";
    public static string LinkPolicy = "https://sites.google.com/view/mini-game-puzzle-fun-policy/";
    public static string LinkTerm = "https://sites.google.com/view/mini-game-puzzle-fun-policy/";

    #region SEVER_EVENT
    public static int ID_GAME = 1;
    public static string LINK_SERVER = "http://172.104.176.117/api/log_event.php";
    public static string LINK_SERVER_USER_DATA = "http://172.104.176.117/api/register.php";
   // public static string LINK_SERVER_GET_VERSION_UPDATE = "http://sdk.hdvietpro.com/android/apps/control-new.php?code=78974&date_install=2017-05-15&version=20170511&deviceID=7203046252782852&country=VN&id_partner=1";

    public static string LINK_SERVER_GET_VERSION_UPDATE = "http://sdk.hdvietpro.com/android/apps/control-new.php?code=84129&date_install={0}&version={1}&deviceID={2}&country={3}&id_partner={4}&packageName={5}";

    public static string OS_TYPE
    {
        get
        {
#if UNITY_ANDROID
           return "android";
#elif UNITY_IPHONE
       return "ios";
#else
        return "other";
#endif
        }
    }

    public static string AdmobId { get; set; }
    public static string Admob_Interstitial_ID { get; }
    public static string Admob_Interstitial_ID_LOW { get; }
    public static string Admob_Reward_ID { get; set; }
    public static string Admob_Reward_ID_LOW { get; }
    public static string Admob_Banner_ID { get; }
    public static string Admob_Banner_ID_LOW { get; }

    #endregion
}
