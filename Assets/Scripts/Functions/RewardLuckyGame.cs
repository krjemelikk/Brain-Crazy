using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardLuckyGame : MonoBehaviour
{
    //****
    //Sau 3s vào level, sẽ hiển thị quà ở góc màn hình cho người chơi. Người chơi ấn vào để xem quà, nếu người chơi xử dụng quà thì:
    //- Quà biến mất ở góc màn hình
    //- 3p sau quà mới reset thành quà mới và lại hiện lại ở góc màn hình
    //****

    //Quà random trong khoảng từ 1 - 2 hint
    [SerializeField] private Button rewardBtn;

    [Header("Anim Obj")]
    [SerializeField] private GameObject rewardObj;
    [SerializeField] private GameObject boxObj;
    [SerializeField] private GameObject handObj;
    private Vector3 remeberPosHandObj;

    public static float LEVEL_SHOW_REWARD
    {
        get
        {
            return RemoteConfigController.GetFloatConfig(StringHelper.ConfigFirebase.LEVEL_SHOW_REWARD_GAME, 8);
        }
    }
    public static float TIME_APPEAR_REWARD
    {
        get
        {
            return RemoteConfigController.GetFloatConfig(StringHelper.ConfigFirebase.TIME_APPEAR_REWARD, 3.5f);
        }
    }
    public static float DELAY_SHOW_REWARD
    {
        get
        {
            return RemoteConfigController.GetFloatConfig(StringHelper.ConfigFirebase.DELAY_SHOW_REWARD, 210f);
        }
    }//3p rưỡi lại có Lucky reward

    public System.DateTime LAST_TIME_REWARDED
    {
        get
        {
            return System.DateTime.Parse(PlayerPrefs.GetString("last_time_claimed_lucky_reward", UnbiasedTime.Instance.Now.AddDays(-1).ToString()));
        }
        set
        {
            PlayerPrefs.SetString("last_time_claimed_lucky_reward", value.ToString());
        }
    }

    public void Awake()
    {
        remeberPosHandObj = handObj.transform.position;
    }

    public void InitState()
    {
        rewardObj.gameObject.SetActive(false);
        StopAllCoroutines();

        if (GameController.Instance.currentLevel != null && GameController.Instance.currentLevel.IDQuestion == 88)//Quà che mất Text câu hỏi mà level cần tương tác với Text câu hỏi
            return;

        if (DataManager.GetHighestLevelUnlocked >= LEVEL_SHOW_REWARD)
        {
            if (TimeManager.CaculateTime(LAST_TIME_REWARDED, UnbiasedTime.Instance.Now) >= DELAY_SHOW_REWARD)
            {
                StartCoroutine(Helper.StartAction(ShowReward, TIME_APPEAR_REWARD));
            }
        }
    }

    public void ShowReward()
    {
        this.transform.SetAsLastSibling();

        rewardObj.gameObject.SetActive(true);
        rewardBtn.onClick.RemoveAllListeners();
        rewardBtn.onClick.AddListener(() => { RewardLuckyBox.Setup().ShowRewardNotEndGame(ClaimReward); });

        //Anim
        StopAllCoroutines();
        rewardObj.transform.DOKill();
        handObj.transform.DOKill();
        // boxObj.transform.DOKill();
        //rewardObj.transform.localPosition = new Vector3(408, 473, 0);
        handObj.transform.localPosition = new Vector3(65, -32, 0);
       // boxObj.transform.localScale = Vector3.one;
        rewardObj.transform.DOLocalMove(new Vector3(600, rewardObj.transform.localPosition.y, 0), 0.6f).From().SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(Helper.StartAction(() =>
            {
                handObj.transform.DOLocalMove(new Vector3(400, -32, handObj.transform.position.z), 1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                   // boxObj.transform.DOScale(Vector3.one * 1.2f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
                });
            }, 0.5f));

        });
    }

    private void ClaimReward()
    {
        LAST_TIME_REWARDED = UnbiasedTime.Instance.Now;
        rewardObj.gameObject.SetActive(false);
    }

    public void LeaveLevel()
    {
        StopAllCoroutines();
    }
}
