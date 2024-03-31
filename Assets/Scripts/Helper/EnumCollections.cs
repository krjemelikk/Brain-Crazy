
public enum InAppStatus
{
    NotAvailable,
    Owned,
    NotOwned
}

public enum ActionClick
{
    None = 0,
    Menu = 1,
    Rate = 2,
    Share = 3,
    Policy = 4,
    Feedback = 5,
    Term = 6,
    ChooseLevel = 7,
    Hint = 8,
    SkipLevel = 9,
    ResetLevel = 10,
    IAPItems = 11,
    Shop = 12,
    DailyLogin = 13,
    Settings = 14,
    Task = 15,
    Play = 16,
    WatchVideo = 17
}

public enum ReasonUseHint
{
    Suggest = 0,
    SkipLevel = 1
}

public enum Reason
{
    DailyReward = 0,
    Inapp = 1,
    Ads = 2
}

public enum TypeItem
{
    Hint = 0,
    RemoveADS = 1,
    AddHintVideo = 2,//Item +1 Hint khi xem Video thành công
}

public enum ActionWatchVideo
{
    AddHint_PopupMoreHint = 0,
    AddHint_InShop = 1,
    PassLevel = 2,
    LuckyReward_EndGame = 3,
    LuckyReward_InGame = 4,
    AddHint_PopupWin = 5,
    DoubleReward_DailyLogin = 6,
    SkipPopup = 7,
    BGPopup = 8,
    RewardAccumulate = 9,
}


