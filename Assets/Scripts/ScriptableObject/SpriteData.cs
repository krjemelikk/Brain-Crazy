using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SpriteData")]
public class SpriteData : SerializedScriptableObject
{
    public Dictionary<TypeItem, Sprite> spriteItem;//Hình ảnh các Item trong Game

    public Sprite GetSpriteItem(TypeItem typeItem)
    {
        if (spriteItem.ContainsKey(typeItem))
            return spriteItem[typeItem];

        return null;
    }
}


