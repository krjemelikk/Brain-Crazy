using DG.Tweening;
using System.Collections.Generic;
using _0__Source;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardIAPBox : BaseBox
{
    public static GameObject instance;
    public UnityAction moreActionOff;

    [SerializeField] protected Transform contentPool;
    [SerializeField] protected ElementReceverIAP rewardPrefab;
    [SerializeField] private Button _button;

    protected List<ElementReceverIAP> rewardsPool;

    protected override void OnStart()
    {
        base.OnStart();
        backObj.timeAnimClose = 0.5f;
    }

    protected override void ActionDoOff()
    {
        base.ActionDoOff();
        if (this.moreActionOff != null)
            this.moreActionOff();
        moreActionOff = null;

        mainPanel.localScale = Vector3.one;
        mainPanel.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).SetEase(Ease.InBack);
    }

    public static RewardIAPBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI;
            instance = Instantiate(Resources.Load(PathPrefabs.RECEVER_IAP) as GameObject); 
        }

        instance.SetActive(true);
        return instance.GetComponent<RewardIAPBox>();
    }

    public RewardIAPBox Show(IAPPack iapReward)
    {
        for (int i = 0; i < contentPool.transform.childCount; i++)
        {
            contentPool.transform.GetChild(i).gameObject.SetActive(false);
        }

        //Show Reward
        if (iapReward != null)
        {
            foreach (var item in iapReward.itemsResult)
            {
                ElementReceverIAP reward = GetElement();
                reward.Init(item.Key, item.Value);
            }
        }


        return this;
    }

    public RewardIAPBox ShowByWatchVideo(int value)
    {
        for (int i = 0; i < contentPool.transform.childCount; i++)
        {
            contentPool.transform.GetChild(i).gameObject.SetActive(false);
        }

        //Show Reward
        ElementReceverIAP reward = GetElement();
        reward.Init(TypeItem.Hint, value);

        return this;
    }

    private ElementReceverIAP GetElement()
    {
        if (rewardsPool == null)
            rewardsPool = new List<ElementReceverIAP>();
        for (int i = 0; i < rewardsPool.Count; i++)
        {
            if (!rewardsPool[i].gameObject.activeSelf)
            {
                rewardsPool[i].gameObject.SetActive(true);
                return rewardsPool[i];
            }
        }

        ElementReceverIAP prefab = Instantiate(rewardPrefab.gameObject, contentPool).GetComponent<ElementReceverIAP>();
        rewardsPool.Add(prefab);
        return prefab;
    }
}