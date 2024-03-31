using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/RewardLuckyData")]
public class RewardLuckyData : SerializedScriptableObject
{
    public List<ItemRewardLucky> lsItem = new List<ItemRewardLucky>();

    private int percentBonus = 0;
    public ItemRewardLucky GetItem()
    {
        int random = Random.Range(0, 100);
        if(random <= lsItem[0].chance)
        {
            percentBonus += 10;
            return lsItem[0];
        }
        else
        {
            percentBonus = 0;
            return lsItem[1];
        }
    }
}

public class ItemRewardLucky
{
    public int valueKey;
    public bool isWatchVideo;
    public int chance;
}


