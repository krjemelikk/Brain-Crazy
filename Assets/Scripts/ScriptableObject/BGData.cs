using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGData", menuName = "GameData/BGData")]
public class BGData : SerializedScriptableObject
{
    public List<DataBG> dataBGs;
}

public class DataBG
{
    public int IDBG;
    public Sprite imgBG;
    public bool isInapp;
    [ShowIf("isInapp")] public string IDInapp;
    [HideIf("isInapp")] public int numVideo;

    public int numVideoWatch
    {
        get
        {
            if (!PlayerPrefs.HasKey("BG_" + IDBG))
                PlayerPrefs.SetInt("BG_" + IDBG, 0);
            return PlayerPrefs.GetInt("BG_" + IDBG, 0);
        }
        set
        {
            PlayerPrefs.SetInt("BG_" + IDBG, value);
        }
    }


    public bool isUnlocked
    {
        get
        {
            if (IDBG != 0)
            {
                if (!PlayerPrefs.HasKey("BG_Unlock_" + IDBG))
                    PlayerPrefs.SetInt("BG_Unlock_" + IDBG, 0);
                return PlayerPrefs.GetInt("BG_Unlock_" + IDBG, 0) == 0 ? false : true;
            }
            else
                return true;
        }
        set
        {
            PlayerPrefs.SetInt("BG_Unlock_" + IDBG, 1);
        }
    }
}
