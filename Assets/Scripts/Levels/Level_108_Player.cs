using System;
using UnityEngine;
using UnityEngine.UI;

public class Level_108_Player : MonoBehaviour
{
    public int ID;
    public Level_108 level_Current;
    public bool isObjectCheckDone;

    [HideInInspector] public bool isCheckDone;
    [HideInInspector] public int ID_Trigger = -1;

    private Vector3 posStart;
    private int[] arrIDDone = new int[] { 9, 12, 20 };
    private int[] arrIDSwap = new int[] { 4, 8, 9, 12, 13, 14, 16, 17, 18, 19, 20, 21, 22 };

    public void Start()
    {
        posStart = transform.localPosition;
    }

    public void ResetPlayer()
    {
        transform.localPosition = posStart;
        ID_Trigger = -1;
        isCheckDone = false;
    }

    public void EndDrag()
    {
        if (ID_Trigger == -1)
        {
            transform.localPosition = posStart;
            ID_Trigger = -1;
        }
        else
        {
            transform.GetComponent<Image>().raycastTarget = false;
            transform.localPosition = level_Current.GetPosPlayer(ID_Trigger);
            if (isObjectCheckDone)
            {
                if (Array.Find(arrIDDone, x => x == ID_Trigger) != 0)
                {
                    isCheckDone = true;
                }
            }


            level_Current.amountSwap++;
            level_Current.CheckDone();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Level_108_Player player = other.GetComponent<Level_108_Player>();
            if (Array.Find(arrIDSwap, x => x == player.ID) != 0)
            {
                ID_Trigger = player.ID;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ID_Trigger = -1;
        }
    }
}
