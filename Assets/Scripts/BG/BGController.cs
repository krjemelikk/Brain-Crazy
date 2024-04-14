using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class BGController : MonoBehaviour
{
    //public Button btnInapp;
    public Button btnShowVideo;
    public Button btnUse;
    public Text txtUse;
    public Text txtNumVideoWatch;

    [ReadOnly] public int indexBGSelect;

    public void SetUIBG(int index)
    {
        if (GameController.Instance.dataBG.dataBGs[index].isUnlocked)
        {
            btnShowVideo.gameObject.SetActive(false);
            btnUse.gameObject.SetActive(true);
        }
        else
        {
            btnShowVideo.gameObject.SetActive(true);
            btnUse.gameObject.SetActive(false);

            txtNumVideoWatch.text = GameController.Instance.dataBG.dataBGs[index].numVideoWatch + "/" + GameController.Instance.dataBG.dataBGs[index].numVideo;
        }

        indexBGSelect = index;
    }

    public void OnClickWatchVideo()
    {
        GameController.Instance.admobAds.ShowVideoReward(ActionReward, ActionNotLoad, ActionSkip, ActionWatchVideo.BGPopup);
    }

    private void ActionReward()
    {
        Debug.Log("Claim watch ad");
        GameController.Instance.dataBG.dataBGs[indexBGSelect].numVideoWatch++;
        if(GameController.Instance.dataBG.dataBGs[indexBGSelect].numVideoWatch >= GameController.Instance.dataBG.dataBGs[indexBGSelect].numVideo)
        {
            GameController.Instance.dataBG.dataBGs[indexBGSelect].isUnlocked = true;
        }
        SetUIBG(indexBGSelect);
    }

    private void ActionNotLoad()
    {
        //ConfirmBox.Setup().AddMessageYes("Fail", "Failed to load video", () => CloseCurrentBox());
    }

    private void ActionSkip()
    {
        Debug.Log("Skip video");
    }

    public void OnlickSetBGIndex()
    {
        SetUIBG(indexBGSelect);
        GameController.Instance.HomeScene.SetBG(indexBGSelect);
        Debug.Log("================123===============");
        this.gameObject.SetActive(false);
    }
}
