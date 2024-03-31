using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardItem : MonoBehaviour
{
    public int ID;

    [SerializeField] private Button btClaim;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtValue;
    [SerializeField] private Image imgBG;
    [SerializeField] private Sprite sprCanClaim;
    [SerializeField] private Sprite sprNotClaim;
    [SerializeField] private GameObject gojClaimed;
    [SerializeField] private DOTweenAnimation anim;
    [SerializeField] private GameObject canClaimObj;

    [Header("State")]
    public StateRewardItem state = StateRewardItem.NOT_CLAIM;
    protected int valueItem;
    private string nameItem;

    protected virtual void Start()
    {
        btClaim.onClick.AddListener(() => Claim());
    }

    public virtual void InitData(string _name, int _value, StateRewardItem _state)
    {
        nameItem = _name;
        valueItem = _value;
        state = _state;
        UpdateUI();
    }

    public virtual void Claim()
    {
        GameController.Instance.AddHint(valueItem, Reason.DailyReward);
        state = StateRewardItem.CLAIMED;
        UpdateUI();
    }

    public virtual int ClaimMulti(int multi)
    {
        int valueReturn = valueItem * multi;
        GameController.Instance.AddHint(valueReturn, Reason.DailyReward);
        state = StateRewardItem.CLAIMED;
        UpdateUI();
        return valueReturn;
    }

    private void UpdateUI()
    {
        txtValue.text = "x" + valueItem.ToString();
        txtName.text = nameItem;
        if (gojClaimed != null)
            gojClaimed.SetActive(state == StateRewardItem.CLAIMED);
        if (canClaimObj != null)
            canClaimObj.SetActive(false);

        switch (state)
        {
            case StateRewardItem.CAN_CLAIM:
                imgBG.sprite = sprCanClaim;
                if (canClaimObj != null)
                    canClaimObj.SetActive(true);
                //txtValue.color = Color.white;
                if (anim != null)
                {
                    anim.autoPlay = true;
                }
                break;
            case StateRewardItem.CLAIMED:
                imgBG.sprite = sprCanClaim;
                //txtValue.color = Color.white;
                if (anim != null)
                    anim.autoPlay = false;
                break;
            case StateRewardItem.NOT_CLAIM:
                imgBG.sprite = sprNotClaim;
                //txtValue.color = Color.gray;
                if (anim != null)
                    anim.autoPlay = false;
                break;
            default:
                break;
        }
    }

    public StateRewardItem GetStateRewardItem()
    {
        return state;
    }
}

public enum StateRewardItem
{
    NOT_CLAIM = 0,
    CAN_CLAIM = 1,
    CLAIMED = 2
}
