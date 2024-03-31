using UnityEngine;
using UnityEngine.UI;

public class ElementReceverIAP : MonoBehaviour
{

    [SerializeField] protected Image iconReward_Img;
    [SerializeField] protected Text valueReward_Txt;

    public void Init(TypeItem typeItem, int value)
    {
        valueReward_Txt.text = "";
       if (typeItem == TypeItem.Hint)
        {
            iconReward_Img.sprite = GameController.Instance.dataContains.spriteData.GetSpriteItem(TypeItem.Hint);
            valueReward_Txt.text = value.ToString();
        }
       else if(typeItem == TypeItem.RemoveADS)
        {
            iconReward_Img.sprite = GameController.Instance.dataContains.spriteData.GetSpriteItem(TypeItem.RemoveADS);
        }
       else if(typeItem == TypeItem.AddHintVideo)
        {
            iconReward_Img.sprite = GameController.Instance.dataContains.spriteData.GetSpriteItem(TypeItem.AddHintVideo);
        }
    }
}
