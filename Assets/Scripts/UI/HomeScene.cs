using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using EventDispatcher;
using UnityEngine.Events;
using DG.Tweening;

public class HomeScene : BaseScenes
{
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject chooseLevelPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Container Parent")] public Transform tfCanvasParent;
    public Transform tfLevelParent;
    public Transform tfPopupParent;

    [Header("Top")] [SerializeField] private Text txtHint;
    [SerializeField] private Text txtHintHome;
    [SerializeField] private Text txtTotalTimePlay;

    [Header("Tutorial")] [SerializeField] private RectTransform hand;

    [Header("Bound")]
    public RectTransform BoundTop;
    public RectTransform BoundBottom;
    public RectTransform BoundLeft;
    public RectTransform BoundRight;

    [Header("Music and Sound")]
    [SerializeField] private Image imgSound;
    [SerializeField] private Sprite[] sprSound;
    [SerializeField] private Image imgMusic;
    [SerializeField] private Sprite[] sprMusic;
    [SerializeField] private Image imgVibration;
    [SerializeField] private Sprite[] sprVibration;
    [SerializeField] private Image imgMusic_Home;
    [SerializeField] private Sprite[] sprMusic_Home;

    [Header("Language")]
    [SerializeField] private LanguagePanel languagePanel;
    [SerializeField] private Text languageText;
    [SerializeField] private LanguageSpriteDictionary languageDict;

    [Header("UILevelBtn")]
    [SerializeField] private LevelButton levelBtnPrefab;
    [SerializeField] private Transform contentLevelUI;

    [Header("BG")]
    [SerializeField] private Image bg;

    private IDisposable _disposeHand;
    private UnityAction actionShareDone;

    [SerializeField] private Button shareChooseLevelBtn;

    private void Start()
    {
        MusicManager.Instance.PlayBGMusic();
        this.RegisterListener(EventID.HINT_CHANGE, (sender) => UpdateUI());
        if (languagePanel != null) languagePanel.Init(languageDict, ReloadFlag, () => { languagePanel.gameObject.SetActive(false); });
    }

    private void OnEnable()
    {
        ReloadFlag();
    }

    protected override void OnBaseBack()
    {
        base.OnBaseBack();
        Debug.Log("OnBaseBack");

        if (!settingsPanel.activeSelf && !chooseLevelPanel.activeSelf && !homePanel.activeSelf)
        {
            chooseLevelPanel.SetActive(true);
            return;
        }

        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            return;
        }

        if (chooseLevelPanel.activeSelf)
        {
            //chooseLevelPanel.SetActive(false);
            //homePanel.SetActive(true);
            QuitBox.Setup();
            return;
        }

        if (homePanel.activeSelf)
        {
            QuitBox.Setup();
            return;
        }
    }

    public void DisableAllPanel()
    {
        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
        }

        if (chooseLevelPanel.activeInHierarchy)
        {
            chooseLevelPanel.SetActive(false);
        }

        if (homePanel.activeInHierarchy)
        {
            homePanel.SetActive(false);
        }
    }

    #region === Settings ===
    [SerializeField] private Button shareBtn;
    [SerializeField] private GameObject addHintShareObj;
    public void InitSetting()
    {
        shareBtn.onClick.RemoveAllListeners();

        if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_SHARE_ADD_HINT, false) && TimeManager.CaculateTime(UnbiasedTime.Instance.Now, DataManager.TimeLastShareAddHint) >= DataManager.TimeDelayShareAddHint)
        {
            addHintShareObj.SetActive(true);

            shareBtn.gameObject.transform.DOKill();
            shareBtn.gameObject.transform.localScale = Vector3.one;
            shareBtn.gameObject.transform.DOScale(1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

            shareBtn.onClick.AddListener(() =>
            {
                OnClickShare(() =>
                {
                    DataManager.AddHint(1);
                    RewardIAPBox.Setup().ShowByWatchVideo(1);
                    DataManager.TimeLastShareAddHint = UnbiasedTime.Instance.Now;
                    addHintShareObj.SetActive(false);
                    shareBtn.gameObject.transform.DOKill();
                    shareBtn.gameObject.transform.localScale = Vector3.one;
                });
            });
        }
        else
        {
            addHintShareObj.SetActive(false);

            shareBtn.gameObject.transform.DOKill();
            shareBtn.gameObject.transform.localScale = Vector3.one;
            shareBtn.onClick.AddListener(() =>
            {
                OnClickShare(null);
            });
        }
    }

    public void SetOnOffVibration()
    {
        DataManager.OnVibration = !DataManager.OnVibration;
        UpdateUISoundMusic();
    }

    public void VibrateDevice()
    {
        
    }

    public void SetOnOffMusic()
    {
        MusicManager.Instance.MusicVolume = MusicManager.Instance.MusicVolume == 0 ? 1 : 0;
        if (MusicManager.Instance.MusicVolume == 0)
        {
            MusicManager.Instance.PauseMusic();
        }
        else
        {
            MusicManager.Instance.UnPauseMusic();
        }
        UpdateUISoundMusic();
    }

    public void SetOnOffSound()
    {
        MusicManager.Instance.SoundVolume = MusicManager.Instance.SoundVolume == 0 ? 1 : 0;
        UpdateUISoundMusic();
    }

    private void UpdateUISoundMusic()
    {
        if (imgSound != null) imgSound.sprite = MusicManager.Instance.SoundVolume == 0 ? sprSound[0] : sprSound[1];
        if (imgMusic != null) imgMusic.sprite = MusicManager.Instance.MusicVolume == 0 ? sprMusic[0] : sprMusic[1];
        //if (imgMusic_Home != null) imgMusic_Home.sprite = MusicManager.Instance.MusicVolume == 0 ? sprMusic_Home[0] : sprMusic_Home[1];
        if (imgVibration != null) imgVibration.sprite = DataManager.OnVibration ? sprVibration[1] : sprVibration[0];
    }

    public void OnClickLanguage()
    {
        languagePanel.gameObject.SetActive(true);
    }

    private void ReloadFlag()
    {
        languageText.text = Localization.Get(Localization.language);
        this.PostEvent(EventID.CHANGE_LANGUAGE);
    }

    public void OnClickFeedback()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ConfirmBox.Setup().AddMessageYes(Localization.Get("s_Noti"), Localization.Get("s_NoConnect"), () => { });
            return;
        }
        if (!string.IsNullOrEmpty(Config.LinkFeedback))
            Application.OpenURL(Config.LinkFeedback);
    }

    public void OnClickPolicy()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ConfirmBox.Setup().AddMessageYes(Localization.Get("s_Noti"), Localization.Get("s_NoConnect"), () => { });
            return;
        }
        if (!string.IsNullOrEmpty(Config.LinkPolicy))
            Application.OpenURL(Config.LinkPolicy);
    }

    public void OnClickTerm()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ConfirmBox.Setup().AddMessageYes(Localization.Get("s_Noti"), Localization.Get("s_NoConnect"), () => { });
            return;
        }
        if (!string.IsNullOrEmpty(Config.LinkTerm))
            Application.OpenURL(Config.LinkTerm);
    }

    public void OnClickShare(UnityAction actionShareDone)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ConfirmBox.Setup().AddMessageYes(Localization.Get("s_Noti"), Localization.Get("s_NoConnect"), () => { });
            return;
        }
        string nameGame = Localization.Get("name_Game");
        string titleShare = Localization.Get("lb_share_title_1");
        this.actionShareDone = actionShareDone;
#if UNITY_EDITOR
        if (this.actionShareDone != null)
            this.actionShareDone();
#else
        NativeShare_Text();
        //FeedShare();
#endif
    }

    private void NativeShare_Text()
    {
        string subject = Localization.Get("lb_share_title_1");
        string url = "https://play.google.com/store/apps/details?id=" + Config.package_name;
        // Share
        new NativeShare().SetSubject(subject).SetText(url).Share();
    }

    void nativeShare()
    {
        //yield return null;
        string body = Localization.Get("lb_share_title_1");
        new NativeShare().SetTarget(Config.package_name).SetSubject(body).SetText("https://play.google.com/store/apps/details?id=" + Config.package_name).Share();
    }

    private void ShareButtonChooseLevel()
    {
        if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_SHARE_ADD_HINT, false) && TimeManager.CaculateTime(UnbiasedTime.Instance.Now, DataManager.TimeLastShareAddHint) >= DataManager.TimeDelayShareAddHint)
        {
            shareChooseLevelBtn.gameObject.SetActive(true);

            shareChooseLevelBtn.gameObject.transform.DOKill();
            shareChooseLevelBtn.gameObject.transform.localScale = Vector3.one * 0.9f;
            shareChooseLevelBtn.gameObject.transform.DOScale(1f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

            shareChooseLevelBtn.onClick.RemoveAllListeners();
            shareChooseLevelBtn.onClick.AddListener(() =>
            {
                OnClickShare(() =>
                {
                    DataManager.AddHint(1);
                    RewardIAPBox.Setup().ShowByWatchVideo(1);
                    DataManager.TimeLastShareAddHint = UnbiasedTime.Instance.Now;
                    shareChooseLevelBtn.gameObject.transform.DOKill();
                    shareChooseLevelBtn.gameObject.transform.localScale = Vector3.one;
                    shareChooseLevelBtn.gameObject.SetActive(false);
                });
            });
        }
        else
        {
            shareChooseLevelBtn.gameObject.SetActive(false);
        }

    }
    #endregion

    #region === OnClick Scene Home ===

    public void OnClickPlay()
    {
        homePanel.SetActive(false);
        chooseLevelPanel.SetActive(false);

        GameController.Instance.StartGame();
    }

    public void OnClickLevels()
    {
        homePanel.SetActive(false);
        chooseLevelPanel.SetActive(true);
        ShareButtonChooseLevel();
    }

    public void OnClickShop()
    {
        InappPanel.Setup();
    }

    public void OnClickMoreHint()
    {
        InappPanel.Setup();
    }

    public void CheckShowRate(int levelPass)
    {
        try
        {
            string levels_show_str = RemoteConfigController.GetStringConfig(StringHelper.ConfigFirebase.LEVELS_SHOW_RATE, "7.15.20.30.40.50.60.70.80.90.100.110.120.130");
            string[] levels_show_rate_s = levels_show_str.Split('.');
            for (int i = 0; i < levels_show_rate_s.Length; i++)
            {
                int level = System.Int32.Parse(levels_show_rate_s[i]);

                if (levelPass == level)
                {
                    Rate();
                    return;
                }
            }
        }
        catch
        {

        }
    }

    public void Rate()
    {
        Debug.Log(StringHelper.StringColor("Show Rate", ColorString.yellow));
        if (!DataManager.IsRated_5_Star)
            RateBox.Setup().Show();
    }

    public void Share()
    {
        Debug.Log(StringHelper.StringColor("Show Share", ColorString.yellow));
    }

    public void OnClickTask()
    {
        DailyRewardPanel.Setup();
    }

    #endregion

    #region === OnClick Scene Level ===

    public void OnClickHint()
    {
        if (GameController.Instance.stateGame == StateGame.PLAYING)
        {
            GameController.Instance.UseHint(1, ReasonUseHint.Suggest);
        }
        else
        {
            MoreHintPanel.Setup();
        }
    }

    public void OnClickUseHintSkipLevel()
    {
        //ConfirmBox.Setup().AddMessageYesHasCloseBtn(Localization.Get("s_Noti"), Localization.Get("s_doYouWantSkip"),
        //    () => { GameController.Instance.UseHint(2, ReasonUseHint.SkipLevel); });

        SkipBox.Setup().Show();
    }

    public void OnClickBackToChooseLevel()
    {
        homePanel.SetActive(false);
        chooseLevelPanel.SetActive(true);
        ShareButtonChooseLevel();
        this.PostEvent(EventID.GO_SELECT_LEVEL);
        GameController.Instance.ShowInterstitial();
    }

    public void OnClickHome()
    {
        GameController.Instance.stateGame = StateGame.NONE;

        homePanel.SetActive(true);
        chooseLevelPanel.SetActive(false);
    }

    public void OnClickSettings()
    {
        settingsPanel.SetActive(true);
        InitSetting();
        this.PostEvent(EventID.GO_SETTING);
        GameController.Instance.ShowInterstitial();
    }

    public void OnClickReset()
    {
        GameController.Instance.ResetLevel();
    }

    #endregion

    public void OnClickChooseLevel(int index)
    {
        GameController.Instance.StartLevel(index);
    }

    public void UpdateUI()
    {
        txtHint.text = $"{DataManager.Hint}";
        txtHintHome.text = $"{DataManager.Hint}";
    }

    /// <summary>
    /// Show hand tutorial
    /// </summary>
    /// <param name="rectTransform"></param>
    public void ShowHand(RectTransform rectTransform)
    {
        hand.gameObject.SetActive(true);
        hand.position = rectTransform.position;

        _disposeHand = Observable.Timer(TimeSpan.FromSeconds(2f)).Subscribe(_ => { hand.gameObject.SetActive(false); })
            .AddTo(this);
    }

    /// <summary>
    /// Show hand near Hint Button
    /// </summary>
    public void SuggestHint()
    {
        hand.gameObject.SetActive(true);
        hand.position = txtHint.GetComponent<RectTransform>().position;

        _disposeHand = Observable.Timer(TimeSpan.FromSeconds(2f)).Subscribe(_ => { hand.gameObject.SetActive(false); })
            .AddTo(this);
    }

    public void DisableHand()
    {
        hand.gameObject.SetActive(false);
        _disposeHand?.Dispose();
    }

    /// <summary>
    /// Set time play UI
    /// </summary>
    /// <param name="timeSpan"></param>
    public void SetTimePlayUI(TimeSpan timeSpan)
    {
        if (txtTotalTimePlay != null)
            txtTotalTimePlay.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void LoadLevelUI()
    {
        foreach (DataLevel l in GameController.Instance.dataContains.dataLevels.lsLevel)
        {
            LevelButton levelBtn = Instantiate<LevelButton>(levelBtnPrefab, contentLevelUI);
            levelBtn.ID = l.ID;
        }

        LevelButton levelComing = Instantiate<LevelButton>(levelBtnPrefab, contentLevelUI);
        levelComing.CommingSoonLevel();
    }

    public void SetBG(int index)
    {
        bg.sprite = GameController.Instance.dataBG.dataBGs[index].imgBG;
    }
}