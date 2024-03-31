using UnityEngine;
using UnityEngine.Events;

public class RewardAccumulate : MonoBehaviour
{
    public float CurrentPercent
    {
        get
        {
            return PlayerPrefs.GetFloat("percent_reward_accumulate", 0);
        }
        set
        {
            PlayerPrefs.SetFloat("percent_reward_accumulate", value);
        }
    }

    public int CurrentValueReward
    {
        get
        {
            return PlayerPrefs.GetInt("Current_Value_RewardAccumulate", 2);
        }
        set
        {
            PlayerPrefs.SetInt("Current_Value_RewardAccumulate", value);
        }
    }

    public void AddCurrentPercent()
    {
        if (CurrentPercent >= 100)
            return;

        float perent = RemoteConfigController.GetFloatConfig(StringHelper.ConfigFirebase.PERCENT_ADD_REWARD_LEVEL, 10);
        CurrentPercent += perent;
    }

    public void RandomRewardValue()
    {
        //Random quà
        int rewardHint = RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.NUM_REWARD_ACCUMULATE, 1);
        int rewardHintPercent = Random.Range(0, 101);
        if (rewardHintPercent >= 0 && rewardHintPercent < RemoteConfigController.GetIntConfig(StringHelper.ConfigFirebase.PERCENT_REWARD_3_ACCUMULATE, 10))
        {
            rewardHint = 2;
        }

        CurrentValueReward = rewardHint;
    }
    
    public void ClaimReward(UnityAction actionClaimDone)
    {
        //Show Popup quà
        RewardShowBox.Setup().Show(CurrentValueReward, actionClaimDone);
    }
}
