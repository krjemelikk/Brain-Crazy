using UnityEngine;
using UnityEngine.UI;

public class Level_130 : BaseLevel
{
    public GameObject key;
    public GameObject checkOpenChest;
    private bool isFoundKey;
    private bool isComplete;
    public Image chestImg;
    public Sprite openChestSprt;

    public void OnFoundKey()
    {
        isFoundKey = true;
        key.transform.SetAsLastSibling();
    }

    public void DragKey()
    {
        if (isComplete)
            return;

        if (!isFoundKey)
            return;

        if(Vector2.Distance(key.transform.position, checkOpenChest.transform.position) < 0.2f)
        {
            //key.gameObject.SetActive(false);
            chestImg.sprite = openChestSprt;
            isComplete = true;
            RightAnswer();
        }
    }
}
