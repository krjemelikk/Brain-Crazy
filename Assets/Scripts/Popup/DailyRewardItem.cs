using EventDispatcher;

public class DailyRewardItem : RewardItem
{
    protected override void Start()
    {
       // btClaim.onClick.AddListener(() => Claim());
    }

    public override void InitData(string _name, int _value, StateRewardItem _state)
    {
        base.InitData(_name, _value, _state);
    }

    public override void Claim()
    {
        if (state != StateRewardItem.CAN_CLAIM || !DataManager.CanClaimDailyRewardToday) return;

        base.Claim();

        DataManager.DailyRewardID = ID;
        DataManager.CanClaimDailyRewardToday = false;
        this.PostEvent(EventID.CLAIM_DAILY_REWARD);
        RewardIAPBox.Setup().ShowByWatchVideo(valueItem);
    }

    public override int ClaimMulti(int multi)
    {
        if (state != StateRewardItem.CAN_CLAIM || !DataManager.CanClaimDailyRewardToday)
            return 0;

        int valueReturn = base.ClaimMulti(multi);

        DataManager.DailyRewardID = ID;
        DataManager.CanClaimDailyRewardToday = false;
        this.PostEvent(EventID.CLAIM_DAILY_REWARD);
        RewardIAPBox.Setup().ShowByWatchVideo(valueReturn);
        return valueReturn;
    }
}
