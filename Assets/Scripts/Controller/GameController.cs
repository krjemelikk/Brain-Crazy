using System;
using UnityEngine;
using EventDispatcher;
using UniRx;
using System.Collections;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
        DontDestroyOnLoad(this);
    }

    [Sirenix.OdinInspector.ReadOnly] public StateGame stateGame;

    [Sirenix.OdinInspector.ReadOnly] public bool IsSpawnLevel;

    public HomeScene HomeScene;

    [Sirenix.OdinInspector.ReadOnly] public BaseLevel currentLevel;

    //Ð?m th?i gian choi 1 level
    private IDisposable CountTimePlay;
    private float timePlay;
    private TimeSpan timeSpan;

    [Sirenix.OdinInspector.ReadOnly] public float countdownAds;

    #region Controller Reference
    [Header("Controller")]
    //public IapController iapController;
    public DataContains dataContains;
    public AdmobAds admobAds;
    public RewardLuckyGame rewardLuckyGame;
    public RewardAccumulate rewardAccumulate;
    #endregion

    public const int ID_NOTIFY_PLAY_GAME1 = 1;
    public const int ID_NOTIFY_PLAY_GAME2 = 2;
    public const int ID_NOTIFY_PLAY_GAME3 = 3;
    public string[] keyLocalize;

    public BGData dataBG;

    [Sirenix.OdinInspector.ReadOnly]
    public int capingInter;
    [Sirenix.OdinInspector.ReadOnly]
    public int capingVideoReward;
    [Sirenix.OdinInspector.ReadOnly]
    public int capingBaner;

    public int indexBG
    {
        get
        {
            if (!PlayerPrefs.HasKey("BG_USED"))
                PlayerPrefs.SetInt("BG_USED", 0);
            return PlayerPrefs.GetInt("BG_USED", 0);
        }
        set
        {
            PlayerPrefs.SetInt("BG_USED", value);
        }
    }

    private void Start()
    {
        if (Config.VersionCodeAll > DataManager.RememberCurrentVersion)
        {
            DataManager.RememberCurrentVersion = Config.VersionCodeAll;
            DataManager.TimeInstallGame = UnbiasedTime.Instance.Now;
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.fullScreen = false;

        //if (Config.versionCode < 2020021801)
        //    dataContains.dataLevels = dataContains.dataLevelsOld;

        stateGame = StateGame.NONE;
        this.RegisterListener(EventID.START_LEVEL, (param) => OnHandleStartLevel());
        this.RegisterListener(EventID.COMPLETE_LEVEL, (param) => OnHandleCompleteLevel());

        InitControllers();

        //admobAds.ShowBanner();

        SetNotify();

        HomeScene.LoadLevelUI();

        StartGame();


    }

    public void SetNotify()
    {
        LocalNotification.ClearNotifications();
        DateTime now = DateTime.Now;
        TimeSpan time1 = new TimeSpan(12, 0, 0);
        TimeSpan time2 = new TimeSpan(20, 30, 0);
        var timeCurrent = now.TimeOfDay;
        long delay1 = timeCurrent > time1 ? (long)(time1.TotalSeconds - timeCurrent.TotalSeconds) + 24 * 60 * 60 : (long)(time1.TotalSeconds - timeCurrent.TotalSeconds);
        long delay2 = timeCurrent > time2 ? (long)(time2.TotalSeconds - timeCurrent.TotalSeconds) + 24 * 60 * 60 : (long)(time2.TotalSeconds - timeCurrent.TotalSeconds);
        float timedelay = RemoteConfigController.GetFloatConfig(StringHelper.ConfigFirebase.CONFIG_PUSH_NOTI_OFFLINE, 24f);
        LocalNotification.SendRepeatingNotification(ID_NOTIFY_PLAY_GAME1, (long)(delay1 * 1000), (long)(24 * 60 * 60 * 1000), "Brain Crazy IQ Challenge: Can you pass it ?", string.Format(Localization.Get(keyLocalize[UnityEngine.Random.Range(0, keyLocalize.Length)]), DataManager.CurrentLevel), new Color32(0xff, 0x44, 0x44, 255));
        LocalNotification.SendRepeatingNotification(ID_NOTIFY_PLAY_GAME2, (long)(delay2 * 1000), (long)(24 * 60 * 60 * 1000), "Brain Crazy IQ Challenge: Can you pass it ?", string.Format(Localization.Get(keyLocalize[UnityEngine.Random.Range(0, keyLocalize.Length)]), DataManager.CurrentLevel), new Color32(0xff, 0x44, 0x44, 255));
        LocalNotification.SendRepeatingNotification(ID_NOTIFY_PLAY_GAME3, (long)(timedelay * 60 * 60 * 1000), (long)(timedelay * 60 * 60 * 1000), "Brain Crazy IQ Challenge: Can you pass it ?", string.Format(Localization.Get(keyLocalize[UnityEngine.Random.Range(0, keyLocalize.Length)]), DataManager.CurrentLevel), new Color32(0xff, 0x44, 0x44, 255));
    }

    private void Update()
    {
        countdownAds += Time.unscaledDeltaTime;
    }

    private void InitControllers()
    {
        //iapController.Init();
        admobAds.Init();

        capingInter = RemoteConfigController.GetIntConfig(StringHelper.CAPING_INTER_CLICK, 1000);
        capingVideoReward = RemoteConfigController.GetIntConfig(StringHelper.CAPING_VIDEOREWARD_CLICK, 1000);
        capingBaner = RemoteConfigController.GetIntConfig(StringHelper.CAPING_BANER_CLICK, 1000);

        admobAds.ResetCaping();
        HomeScene.SetBG(indexBG);

        try
        {
            GetDataUpdateVersion();
        }
        catch
        {

        }
    }

    public void StartGame()
    {
        if (!DataManager.FirstTimePlayGame)
        {
            ShowDailyReward();
        }

        if (DataManager.FirstTimePlayGame) DataManager.FirstTimePlayGame = false;

        if (IsSpawnLevel)
        {
            StartLevel(DataManager.CurrentLevel);
        }
        else
        {
            currentLevel = FindObjectOfType<BaseLevel>();
        }

        HomeScene.UpdateUI();

    }

    public void ShowDailyReward()
    {
        if (DataManager.LastTimeOpenGame.DayOfYear < UnbiasedTime.Instance.Now.DayOfYear)
        {
            DataManager.LastTimeOpenGame = UnbiasedTime.Instance.Now;
            DataManager.DailyRewardID++;
            DataManager.CanClaimDailyRewardToday = true;
            DailyRewardPanel.Setup();
        }
    }

    public void StartLevel(int _level)
    {
        Time.timeScale = 1;
        stateGame = StateGame.PLAYING;
        HomeScene.DisableAllPanel();
        HomeScene.DisableHand();
        if (!DataManager.GetLevelUnlocked(_level))
        {
            //chua unlock level
        }
        else
        {
            DataManager.CurrentLevel = _level;
            if (_level > dataContains.dataLevels.lsLevel.Count)
            {
                DataManager.CurrentLevel = 1;
            }
            if (currentLevel != null) Destroy(currentLevel.gameObject);
            string nameLevel = dataContains.dataLevels.GetLevel(DataManager.CurrentLevel).NAME_LEVEL;
            Debug.Log("nameLevel " + nameLevel);
            currentLevel = Instantiate(Resources.Load<BaseLevel>($"Levels/" + nameLevel), HomeScene.tfLevelParent);
            currentLevel.ID = dataContains.dataLevels.GetLevel(DataManager.CurrentLevel).ID;
        }

        rewardLuckyGame.InitState();
    }

    public void UseHint(int value, ReasonUseHint _reasonUseHint)
    {
        if (DataManager.Hint - value < 0 || stateGame == StateGame.NONE || currentLevel == null)
        {
            MoreHintPanel.Setup();
            return;
        }

        DataManager.AddHint(-value);
        
        switch (_reasonUseHint)
        {
            case ReasonUseHint.Suggest:
                currentLevel?.UseHint();
                break;
            case ReasonUseHint.SkipLevel:
                NextLevel();
                break;
        }

        HomeScene.UpdateUI();
    }

    public void AddHint(int value, Reason _reason)
    {
        DataManager.AddHint(value);
        HomeScene.UpdateUI();
    }

    public void NextLevel()
    {
        var nextLevel = currentLevel.ID -= -1;
        if (nextLevel <= dataContains.dataLevels.lsLevel.Count)
        {
            if (currentLevel != null) Destroy(currentLevel.gameObject);
            if (nextLevel >= DataManager.GetHighestLevelUnlocked)
            {
                DataManager.SetLevelUnlocked(nextLevel);
            }
            StartLevel(nextLevel);
        }
        else
        {
            Debug.Log(StringHelper.StringColor("Out of max level ! Please Update !!!", ColorString.red));
            StartLevel(1);
        }
    }

    public void ResetLevel()
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);
        string nameLevel = dataContains.dataLevels.GetLevel(DataManager.CurrentLevel).NAME_LEVEL;
        currentLevel = Instantiate(Resources.Load<BaseLevel>($"Levels/" + nameLevel), HomeScene.tfLevelParent);
        currentLevel.ID = dataContains.dataLevels.GetLevel(DataManager.CurrentLevel).ID;
    }

    public void ShowTutorial(RectTransform rectTransform)
    {
        HomeScene.ShowHand(rectTransform);
    }

    public void ShowSuggestHint()
    {
        HomeScene.SuggestHint();
    }

    public void RemoveAds()
    {
        DataManager.RemoveAds = 1;
    }

    public void PauseGame()
    {
        stateGame = StateGame.NONE;
        Time.timeScale = 0;
    }

    #region === Handle Event ===

    private void OnHandleStartLevel()
    {
        Debug.Log(StringHelper.StringColor("OnHandleStartLevel", ColorString.yellow));
        
        CountTimePlay?.Dispose();
        CountTimePlay = Observable.Interval(TimeSpan.FromSeconds(1f))
            .Subscribe(totalTimeInSecond =>
            {
                timePlay = totalTimeInSecond;
                timeSpan = TimeSpan.FromSeconds(timePlay);
                HomeScene.SetTimePlayUI(timeSpan);
            }).AddTo(this);
    }


    private void OnHandleCompleteLevel()
    {
        Debug.Log(StringHelper.StringColor("OnHandleCompleteLevel", ColorString.yellow));

        stateGame = StateGame.END_LEVEL;
        CongratulationPanel.Setup(Localization.Get(currentLevel.KeyTips));
        CountTimePlay?.Dispose();
        HomeScene.DisableHand();
        ShowInterstitial();
    }

    public void ShowInterstitial()
    {
        if (countdownAds >= RemoteConfigController.GetFloatConfig(StringHelper.ConfigFirebase.DELAY_SHOW_INITSTIALL, 90))
        {
            admobAds.ShowInterstitial();
            countdownAds = 0;
        }
    }

    #endregion

    #region ===PAUSE OR KILL GAME===

    private void OnDestroy()
    {
        //Luu data ng choi
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (stateGame == StateGame.PLAYING)
            {
                //Luu data ng choi
                DataManager.LastTimeOpenGame = UnbiasedTime.Instance.Now;
            }
        }
        else
        {
            if (stateGame == StateGame.PLAYING)
            {
                //th?i gian offline khi pause game
                var totalTime = TimeManager.CaculateTime(DataManager.LastTimeOpenGame, UnbiasedTime.Instance.Now);
            }

            // Check the pauseStatus to see if we are in the foreground
            // or background
            //app resume
        }
    }

    #endregion

    public void HackLevel()
    {
        PlayerPrefs.SetInt("HIGHEST_LEVEL_UNLOCKED", 150);
    }

    #region Update version
    public void GetDataUpdateVersion()
    {
        // if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_POPUP_UPDATE_NEW_VERSION, false))
        StartCoroutine(GetDataUpdateVersionHandle());
    }

    public IEnumerator GetDataUpdateVersionHandle()
    {
        //DataPostUpdateNewVersion dataPost = new DataPostUpdateNewVersion();
        //dataPost.idGame = Config.ID_GAME.ToString();
        //dataPost.os = Config.OS_TYPE;
        //dataPost.current_version = Config.VersionCodeAll;

        //WWWForm post = new WWWForm();
        //var jsonPost = JsonUtility.ToJson(dataPost);
        //post.AddField("content", jsonPost);

        string linkUpdate = string.Format(Config.LINK_SERVER_GET_VERSION_UPDATE,
                DataManager.TimeInstallGame,
                Config.VersionCodeAll,
                SystemInfo.deviceUniqueIdentifier,
                 Application.systemLanguage,
                 Config.ID_GAME,
                 Config.package_name);

        //WWW Post = new WWW(linkUpdate);
        //    yield return Post;

        using (UnityWebRequest Post = UnityWebRequest.Get(linkUpdate))
        {
            yield return Post.SendWebRequest();

            Debug.Log(Post.error);
            if (Post.isNetworkError || Post.isHttpError)
            {
                Debug.Log(Post.error);
            }
            else
            {
                // Debug.Log("POST " +Post.text);
                Debug.Log("POST " + Post.downloadHandler.text);
                var data = SimpleJSON.JSON.Parse(Post.downloadHandler.text);
                //Debug.Log("POST " + Post.text);
                if (data != null)
                {
                    var updateData = data["update"];
                    if (updateData != null)
                    {
                        UpdateStatus updateStatus = new UpdateStatus();
                        updateStatus.status = updateData["status"].AsInt;
                        updateStatus.offset_show = updateData["offset_show"].AsInt;
                        updateStatus.title = updateData["title"];
                        updateStatus.description = updateData["description"];
                        updateStatus.url_store = updateData["url_store"];
                        updateStatus.url_store_ios = updateData["url_store_ios"];
                        updateStatus.version = updateData["version"].AsInt;

                       //updateStatus.status = 1;
                       // updateStatus.title = "s_title_update";
                       // updateStatus.description = "s_has_new_version";
                       // updateStatus.Debug();
                        SetUpdate(updateStatus);
                    }
                }
            }
        }
    }

    public void SetUpdate(UpdateStatus updateData)
    {
        int currentVersion = Config.VersionCodeAll;
        if (updateData.version > currentVersion)
        {
            if (updateData.status == 1)
            {
                if (!DataManager.IsDontShowAgainPopupUpdate(updateData.version) && TimeManager.CaculateTime(DataManager.LastTimeShowPopupUpdate, System.DateTime.Now) >= updateData.offset_show)
                {
                    //Show Popup
                    UpdateNewVersionBox.Setup().Show(updateData);
                }
            }
            else if (updateData.status == 2)
            {
                //Show Popup luôn
                UpdateNewVersionBox.Setup().Show(updateData);
            }
        }

    }

    #endregion
}

[SerializeField]
public class DataPostUpdateNewVersion
{
    public string idGame;
    public string os;
    public int current_version;
}

[SerializeField]
public class UpdateStatus
{
    public int status; //Trạng thái update 0: ko update, 1: Khuyến khích update, 2: Bắt buộc update
    public int offset_show;//Bao nhiêu lâu hiện 1 lần
    public string title;
    public string description;//Mô tả
    public string url_store;//Link store Android
    public string url_store_ios;//Link store ios
    public int version;//Version hiện tại trên store

    public void Debug()
    {
        UnityEngine.Debug.Log("status " + status +
            " offset_show " + offset_show +
            " title " + title +
            " description " + description +
            " url_store_android " + url_store +
            " url_store_ios " + url_store_ios +
            " version " + version);
    }
}

public enum StateGame
{
    NONE,
    PLAYING,
    END_LEVEL
}