using UnityEngine;
using UnityEngine.UI;

public class UpdateNewVersionBox : BaseBox
{
    [SerializeField] private Toggle dontShowAgainToggle;
    [SerializeField] private Text titleTxt;
    [SerializeField] private Text descriptionTxt;
    [SerializeField] private Button lateBtn;
    [SerializeField] private Button updateBtn;
    [SerializeField] private Button updateNowBtn;
    [SerializeField] private Button closeBtn;

    private static UpdateNewVersionBox instance;
    public static UpdateNewVersionBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load<UpdateNewVersionBox>(PathPrefabs.UPDATE_NEW_VERSION_BOX));
        }

        return instance.GetComponent<UpdateNewVersionBox>();
    }

    public void Show(UpdateStatus updateData)
    {
        base.Show();

       
        if (updateData.status == 1)
        {
            closeBtn.gameObject.SetActive(true);

            //dontShowAgainToggle.gameObject.SetActive(true);
            //dontShowAgainToggle.onValueChanged.RemoveAllListeners();
            //dontShowAgainToggle.onValueChanged.AddListener(OnToggleDontShowChange);

            //lateBtn.gameObject.SetActive(true);
            //lateBtn.onClick.RemoveAllListeners();
            //lateBtn.onClick.AddListener(() => Close());

            updateBtn.gameObject.SetActive(true);
            updateBtn.onClick.RemoveAllListeners();
#if UNITY_ANDROID
            updateBtn.onClick.AddListener(() => { Close(); Application.OpenURL(updateData.url_store); });
#elif UNITY_IPHONE
       updateBtn.onClick.AddListener(() =>{ Close(); Application.OpenURL(updateData.url_store_ios);});
#endif           
            updateNowBtn.gameObject.SetActive(false);

            DataManager.LastTimeShowPopupUpdate = System.DateTime.Now;

            descriptionTxt.text = Localization.Get(updateData.description);
        }
        else if (updateData.status == 2)
        {
            //Show Popup luôn
            BaseScenes.isLockEscape = true;//Không cho đóng Popup

            closeBtn.gameObject.SetActive(false);

            //dontShowAgainToggle.gameObject.SetActive(false);
            //lateBtn.gameObject.SetActive(false);
            updateBtn.gameObject.SetActive(false);

            updateNowBtn.gameObject.SetActive(true);
            updateNowBtn.onClick.RemoveAllListeners();
#if UNITY_ANDROID
            updateNowBtn.onClick.AddListener(() => { Application.OpenURL(updateData.url_store); });
#elif UNITY_IPHONE
       updateNowBtn.onClick.AddListener(() =>{ Application.OpenURL(updateData.url_store_ios);});
# endif

            DataManager.LastTimeShowPopupUpdate = System.DateTime.Now;

            descriptionTxt.text = Localization.Get(updateData.description);
        }

        titleTxt.text = Localization.Get(updateData.title);
    }

    private void OnToggleDontShowChange(bool isChoice)
    {
        Debug.Log("====== isChoice ========" + isChoice);
        DataManager.IsDontShowAgain_PopupUpdate = isChoice;
    }
}
