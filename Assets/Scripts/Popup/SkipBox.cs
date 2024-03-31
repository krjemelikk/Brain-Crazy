using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class SkipBox : BaseBox
{
    private static GameObject instance;
    public UnityAction moreActionOff;

    public Button watchVideoBtn;
    public Button storeBtn;

    public Button skipBtn;
    public GameObject addHintObj;

    protected override void ActionDoOff()
    {
        base.ActionDoOff();
        if (moreActionOff != null)
            moreActionOff();

        mainPanel.localScale = Vector3.one;
        mainPanel.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).SetEase(Ease.InBack);
    }

    public static SkipBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load(PathPrefabs.SKIP_BOX) as GameObject);
            instance.GetComponent<SkipBox>().Init();
        }
        instance.SetActive(true);
        return instance.GetComponent<SkipBox>();
    }

    public void Init()
    {
        watchVideoBtn.onClick.RemoveAllListeners();
        watchVideoBtn.onClick.AddListener(() => { OnClickWatchVideo(); });

        storeBtn.onClick.RemoveAllListeners();
        storeBtn.onClick.AddListener(() => { OpenShop(); });
    }

    protected override void OnStart()
    {
        base.OnStart();
        if(DataManager.Hint >= 2)
        {
            skipBtn.gameObject.SetActive(true);
            addHintObj.gameObject.SetActive(false);

            skipBtn.onClick.RemoveAllListeners();
            skipBtn.onClick.AddListener(()=> { backObj.DoOff(); GameController.Instance.UseHint(2, ReasonUseHint.SkipLevel); });
        }
        else
        {
            skipBtn.gameObject.SetActive(false);
            addHintObj.gameObject.SetActive(true);
        }
    }

    public override void Show()
    {
        base.Show();
    }

   

    private void OnClickWatchVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.SkipPopup);
    }

    private void ActionReward()
    {
        // backObj.DoOff();
        DataManager.AddHint(1);
        RewardIAPBox.Setup().ShowByWatchVideo(1);
        OnStart();
    }

    private void ActionNotLoad()
    {
        //ConfirmBox.Setup().AddMessageYes("Fail", "Failed to load video", () => CloseCurrentBox());
    }

    private void ActionSkip()
    {
        Debug.Log("Skip video");
    }

    private void OpenShop()
    {
        backObj.DoOff();
       var shop =  InappPanel.Setup();
        shop.actionCloseBase = () => { shop.actionCloseBase = null;  SkipBox.Setup().Show(); };
    }
}
