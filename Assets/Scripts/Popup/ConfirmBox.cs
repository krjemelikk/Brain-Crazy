using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmBox : BaseBox
{
    private static GameObject instance;

    public UnityAction moreActionOff;


    private UnityAction actionCloseButton;
    public UnityAction actionHide;

    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;
    [SerializeField] private Button closeBtn;

    [SerializeField] private Text titleText;
    [SerializeField] private Text messegerText;

    protected override void OnStart()
    {

        base.OnStart();
        backObj.timeAnimClose = 0.5f;
    }

    protected override void ActionDoOff()
    {
        base.ActionDoOff();
        if (moreActionOff != null)
            moreActionOff();

        mainPanel.localScale = Vector3.one;
        mainPanel.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).SetEase(Ease.InBack).OnComplete(() => {
            if (actionHide != null)
            {
                actionHide();
                actionHide = null;
            }
        });

    }

    public static ConfirmBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load(PathPrefabs.CONFIRM_POPUP) as GameObject);
        }
        instance.SetActive(true);
        return instance.GetComponent<ConfirmBox>();
    }

    public void AddMessageYesNo(string str, string message, UnityAction actionYes, UnityAction actionNo)
    {
        titleText.text =  str;
        messegerText.text = message;

        yesBtn.gameObject.SetActive(true);
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => { actionYes(); backObj.DoOff(); });

        noBtn.gameObject.SetActive(true);
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => { actionNo(); backObj.DoOff(); });

        closeBtn.gameObject.SetActive(false);

    }

    public void AddMessageYes(string str, string message, UnityAction actionYes)
    {
        titleText.text = str;
        messegerText.text = message;

        yesBtn.gameObject.SetActive(true);
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => { actionYes(); backObj.DoOff(); });

        noBtn.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(false);
    }

    public void AddMessageYesHasCloseBtn(string str, string message, UnityAction actionYes)
    {
        titleText.text = str;
        messegerText.text = message;

        yesBtn.gameObject.SetActive(true);
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => { actionYes(); backObj.DoOff(); });

        noBtn.gameObject.SetActive(false);

        closeBtn.gameObject.SetActive(true);
    }
}
