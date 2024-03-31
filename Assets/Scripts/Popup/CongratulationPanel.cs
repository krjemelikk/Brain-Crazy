using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CongratulationPanel : BaseBox
{
    private static CongratulationPanel instance;

    public static CongratulationPanel Setup(string _tips)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<CongratulationPanel>(PathPrefabs.PANEL_CONGRATULATION), GameController.Instance.HomeScene.tfPopupParent);
            instance.SetUpFirst();
        }
        instance.Show();
        instance.InitData(_tips);
        return instance;
    }

    [SerializeField] private Text txtTips;
    [SerializeField] private Button btNextLevel;
    [SerializeField] private Button btWatchAds;
    [SerializeField] private Button shareBtn;
    [SerializeField] private Button shareNoHintBtn;

    [SerializeField] private GameObject[] humansObj;

    [Header("Reward")]
    [SerializeField] private Text valuePercentReward_Txt;
    [SerializeField] private Image valueBarPercentReward_Img;
    [SerializeField] private Button rewardBtn;
    [SerializeField] private GameObject effectFullPercentReward;
    [SerializeField] private GameObject boxRewardObj;
    [SerializeField] private GameObject handObj;
    private Tween rewardBardTween;

    private bool isSetedPos;
    private Vector3 posFirstNextLevel;
    private Vector3 posFirstWatchAds;

    public void SetUpFirst()
    {
        actionBoxAppearDone = () =>
        {
            if (!isSetedPos)
            {
                posFirstNextLevel = btNextLevel.transform.localPosition;
                posFirstWatchAds = btWatchAds.transform.localPosition;

                isSetedPos = true;
            }
        };
    }

    private void Start()
    {
        btNextLevel.onClick.AddListener(() => OnClickNextLevel());
        btWatchAds.onClick.AddListener(() => OnClickWatchVideo());
    }

    protected override void OnStart()
    {
        base.OnStart();

        if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_SHARE_ADD_HINT, false) && TimeManager.CaculateTime(UnbiasedTime.Instance.Now, DataManager.TimeLastShareAddHint) >= DataManager.TimeDelayShareAddHint)
        {
            shareBtn.gameObject.SetActive(true);
            shareNoHintBtn.gameObject.SetActive(false);
            shareBtn.onClick.RemoveAllListeners();
            shareBtn.onClick.AddListener(() =>
            {
                GameController.Instance.HomeScene.OnClickShare(() =>
                {
                    DataManager.AddHint(1);
                    RewardIAPBox.Setup().ShowByWatchVideo(1);
                    shareBtn.gameObject.SetActive(false);
                    DataManager.TimeLastShareAddHint = UnbiasedTime.Instance.Now;
                });
            });
        }
        else
        {
            shareBtn.gameObject.SetActive(false);
            shareNoHintBtn.gameObject.SetActive(true);
            shareNoHintBtn.onClick.RemoveAllListeners();
            shareNoHintBtn.onClick.AddListener(() =>
            {
                GameController.Instance.HomeScene.OnClickShare(() => { Debug.Log("Share Success"); });
            });
        }

        int randomHuman = Random.Range(0, humansObj.Length);
        for (int i = 0; i < humansObj.Length; i++)
        {
            if (i == randomHuman)
            {
                humansObj[i].gameObject.SetActive(true);
            }
            else
            {
                humansObj[i].gameObject.SetActive(false);
            }
        }

        //Refresh obj
        effectFullPercentReward.SetActive(false);
        boxRewardObj.transform.DOKill();
        boxRewardObj.transform.localScale = 1.25f * Vector3.one;
        if (rewardBardTween != null)
            rewardBardTween.Kill();
        handObj.SetActive(false);

        if (DataManager.GetHighestLevelUnlocked <= DataManager.CurrentLevel)
        {
            float remeberPercent = GameController.Instance.rewardAccumulate.CurrentPercent;
            bool isFullPercent = remeberPercent >= 100;

            GameController.Instance.rewardAccumulate.AddCurrentPercent();

            float currentPercent = GameController.Instance.rewardAccumulate.CurrentPercent;
            if (currentPercent > 100)
                currentPercent = 100;

            if (!isFullPercent)
            {

                valueBarPercentReward_Img.fillAmount = remeberPercent / 100f;

                rewardBardTween = DOTween.To(() => remeberPercent, x => remeberPercent = x, currentPercent, 1f)
                    .OnUpdate(() =>
                    {
                        valuePercentReward_Txt.text = (int)remeberPercent + "%";
                        valueBarPercentReward_Img.fillAmount = remeberPercent / 100f;
                    })
                    .OnComplete(() =>
                    {
                        if (GameController.Instance.rewardAccumulate.CurrentPercent >= 100)
                        {
                            effectFullPercentReward.SetActive(true);
                            //Random qua lan dau tien
                            GameController.Instance.rewardAccumulate.RandomRewardValue();
                        }

                        CheckCanClaimRewardAccumulate();
                    });
            }
            else
            {
                CheckCanClaimRewardAccumulate();
            }
        }
        else
        {
            CheckCanClaimRewardAccumulate();
        }

        if (isSetedPos)
        {
            if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_MIX_BUTTON_NEXT, true))
            {
                if (DataManager.CurrentLevel % RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.DELAY_LEVEL_MIX_BUTTON_NEXT, 5) == 0)
                {
                    btWatchAds.transform.localPosition = posFirstNextLevel;
                    btNextLevel.transform.localPosition = posFirstWatchAds;
                }
                else
                {
                    btWatchAds.transform.localPosition = posFirstWatchAds;
                    btNextLevel.transform.localPosition = posFirstNextLevel;
                }
            }
            else
            {
                btWatchAds.transform.localPosition = posFirstWatchAds;
                btNextLevel.transform.localPosition = posFirstNextLevel;
            }
        }
    }

    private void CheckCanClaimRewardAccumulate()
    {
        if (GameController.Instance.rewardAccumulate.CurrentPercent >= 100)
        {
            valuePercentReward_Txt.text = "100%";
            valueBarPercentReward_Img.fillAmount = 1;

            boxRewardObj.transform.localScale = 1.25f * Vector3.one;
            boxRewardObj.transform.DOScale(1.4f, 0.25f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

            handObj.SetActive(true);

            rewardBtn.onClick.RemoveAllListeners();
            rewardBtn.onClick.AddListener(() =>
            {
                GameController.Instance.rewardAccumulate.ClaimReward(
                    () =>
                    {
                        GameController.Instance.rewardAccumulate.CurrentPercent = 0;
                        valuePercentReward_Txt.text = "0%";
                        valueBarPercentReward_Img.fillAmount = 0;
                        rewardBtn.onClick.RemoveAllListeners();
                        handObj.SetActive(false);

                        boxRewardObj.transform.DOKill();
                        boxRewardObj.transform.localScale = 1.25f * Vector3.one;
                    });

                if (rewardBardTween != null)
                    rewardBardTween.Kill();


            });
        }
        else
        {
            float remeberPercent = GameController.Instance.rewardAccumulate.CurrentPercent;
            valuePercentReward_Txt.text = (int)remeberPercent + "%";
            valueBarPercentReward_Img.fillAmount = remeberPercent / 100f;
        }
    }

    public void InitData(string _tips)
    {
        txtTips.text = _tips;
    }

    private void OnClickNextLevel()
    {
        GameController.Instance.NextLevel();
        //MusicManager.Instance.PlayBGMusic();
        CloseCurrentBox();
    }

    private void OnClickWatchVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.AddHint_PopupWin);
    }

    private void ActionReward()
    {
        Debug.Log("Claim watch ad");
        DataManager.AddHint(1);
        RewardIAPBox.Setup().ShowByWatchVideo(1);
    }

    private void ActionNotLoad()
    {
        //ConfirmBox.Setup().AddMessageYes(Localization.Get("s_Noti"), Localization.Get("s_TryAgain"), () => CloseCurrentBox());
    }

    private void ActionSkip()
    {
        Debug.Log("Skip video");
    }
}
