using System;
using UnityEngine;
using EventDispatcher;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;

public class BaseLevel : SerializedMonoBehaviour
{
    [HideInInspector] public int ID;
    public int IDQuestion;

    public Text txtName;
    public Text txtQuestion;
    protected IDisposable _completeCountDown;
    protected IDisposable _disposeHand;

    protected Action<object> actionChangeLanguage;

    protected virtual void Start()
    {
        StartLevel();
        actionChangeLanguage = (param) => UpdateText();
        this.RegisterListener(EventID.CHANGE_LANGUAGE, actionChangeLanguage);
    }

    protected virtual void Update()
    {
    }

    public virtual void StartLevel()
    {
        this.PostEvent(EventID.START_LEVEL);
        UpdateText();

        if (ID >= 4)
        {
            if (DataManager.SuggestedHint)
            {
                _disposeHand = Observable.Interval(TimeSpan.FromSeconds(30f)).Subscribe(_ =>
                {
                    GameController.Instance.ShowSuggestHint();
                }).AddTo(this);
            }
            else
            {
                _disposeHand = Observable.Interval(TimeSpan.FromSeconds(6f)).Subscribe(_ =>
                {
                    GameController.Instance.ShowSuggestHint();
                }).AddTo(this);
            }
        }
    }

    public virtual void CompleteLevel()
    {
        // MusicManager.Instance.PauseBGMusic();
        MusicManager.Instance.PlayWinSound();
        DataManager.SetLevelPassed(ID, true);
        this.PostEvent(EventID.COMPLETE_LEVEL);
        DisposeHand();

        if (DataManager.GetHighestLevelUnlocked == RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.LEVEL_SHOW_REWARD_END_GAME, 5))
        {
            RewardLuckyBox.Setup().ShowClaim();
        }
        else if (DataManager.GetHighestLevelUnlocked > RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.LEVEL_SHOW_REWARD_END_GAME, 5))
        {
            if (TimeManager.CaculateTime(UnbiasedTime.Instance.Now, DataManager.TimeOpenRewardLucky) > DataManager.TimeDelayShowRewardLucky * 60)
            {
                RewardLuckyBox.Setup().Show();
            }
        }

        if (DataManager.GetHighestLevelUnlocked == RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.LEVEL_SHOW_FIRST_TIME_DAILY_REWARD, 3))
        {
            GameController.Instance.ShowDailyReward();
        }

        GameController.Instance.HomeScene.CheckShowRate(DataManager.CurrentLevel);
    }

    public virtual void WrongAnswer()
    {
        GameController.Instance.HomeScene.VibrateDevice();
        WrongRightEffect.Instance.Wrong();
    }

    public virtual void RightAnswer()
    {
        WrongRightEffect.Instance.Right();

        _completeCountDown = Observable.Timer(TimeSpan.FromSeconds(1f))
            .Subscribe(_ => CompleteLevel())
            .AddTo(this);
    }

    public virtual void UseHint()
    {
        if (!DataManager.SuggestedHint)
        {
            DataManager.SuggestedHint = true;
            _disposeHand?.Dispose();
        }
        HintPanel.Setup(Localization.Get(KeyHint));
    }

    public virtual void Reset()
    {

    }

    public string KeyHint
    {
        get { return StringHelper.HintLevel(IDQuestion); }
    }

    public string KeyQuestion
    {
        get { return StringHelper.QuestionLevel(IDQuestion); }
    }

    public string KeyTips
    {
        get { return StringHelper.TipsLevel(IDQuestion); }
    }

    public void DisposeHand()
    {
        _disposeHand?.Dispose();
    }

    protected virtual void UpdateText()
    {
        if (txtQuestion != null) txtQuestion.text = Localization.Get(KeyQuestion);
        if (txtName != null) txtName.text = $"{Localization.Get("lb_level")} {ID}";
    }

    protected virtual void OnDestroy()
    {
        this.RemoveListener(EventID.CHANGE_LANGUAGE, actionChangeLanguage);
    }
}