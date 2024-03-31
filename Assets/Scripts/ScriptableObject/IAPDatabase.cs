using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class IAPDatabase : SerializedScriptableObject
{
    public Dictionary<TypeIAP, IAPPack> dictPackItem = new Dictionary<TypeIAP, IAPPack>();

    public IAPPack GetItem(TypeIAP typePack)
    {
        IAPPack pack;
        dictPackItem.TryGetValue(typePack, out pack);
        return pack;
    }
}

public enum TypeIAP
{
    HINT_1 = 0,
    HINT_2 = 1,
    HINT_3 = 3,
    HINT_4 = 4,
    ROMOVE_ADS = 5,
    PREMIUM = 6,
}

public class IAPPack
{
    public TypeIAP type;
    //public ProductType productType;
    public string shortID;
    public string ProductID
    {
        get
        {
            return string.Format("{0}.{1}", Config.package_name, shortID);
        }
    }
    
    public Dictionary<TypeItem,int> itemsResult;//Các Item nhận được sau khi mua Pack
    //                Kiểu     Số lượng

    public string defaultPrice;
    public string tittle;
    public Sprite icon;

    public bool isSale;
    [ShowIf("isSale",true)]  public string idSale;
    [ShowIf("isSale", true)] public float percentSale;



    public void Claim()
    {
        switch (type)
        {
            case TypeIAP.HINT_1:
            case TypeIAP.HINT_2:
            case TypeIAP.HINT_3:
            case TypeIAP.HINT_4:
                GameController.Instance.AddHint(itemsResult[TypeItem.Hint], Reason.Inapp);
                break;
            case TypeIAP.ROMOVE_ADS:
                GameController.Instance.RemoveAds();
                break;
            case TypeIAP.PREMIUM:
                GameController.Instance.AddHint(itemsResult[TypeItem.Hint], Reason.Inapp);
                //+1 Hint khi xem video
                break;
        }

        RewardIAPBox.Setup().Show(this);
    }
}