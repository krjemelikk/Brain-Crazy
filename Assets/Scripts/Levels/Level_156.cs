using UnityEngine;
using UnityEngine.UI;

public class Level_156 : BaseLevel
{
    [SerializeField] private Image cageImg;
    [SerializeField] private Sprite cageOpen;

    [SerializeField] private Image chestImg;
    [SerializeField] private Sprite chestOpen;

    [SerializeField] private Image catImg;
    [SerializeField] private Sprite catRun;

    [SerializeField] private DragUI key;
    [SerializeField] private DragUI hammer;

    [SerializeField] private Image lockObj;

    private bool isChoiceHammer;
    private bool isEnd;
    private bool isChestOpend;
    private bool isDestroyCage;

    public void IsChoiceHammer(bool isChoice)
    {
        isChoiceHammer = isChoice;
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if (!isChestOpend && isChoiceHammer)
        {
            if (Vector2.Distance(hammer.transform.position, chestImg.transform.position) < 0.3f)
            {
                chestImg.sprite = chestOpen;
                key.gameObject.SetActive(true);
                isChestOpend = true;
            }
        }

        if (!isDestroyCage && isChestOpend)
        {
            if (Vector2.Distance(key.transform.position, lockObj.transform.position) < 0.3f)
            {
                cageImg.sprite = cageOpen;
                lockObj.gameObject.SetActive(false);
                key.gameObject.SetActive(false);
                isDestroyCage = true;
               
            }
        }

        CheckShakeTrigger();
    }

    private Vector3 shakeDir;
    private void CheckShakeTrigger()
    {
        if (!isDestroyCage)
            return;

        shakeDir = Input.acceleration;

        if (shakeDir.sqrMagnitude >= 10f)
        {
            catImg.sprite = catRun;
           
            catImg.transform.SetAsLastSibling();
            catImg.transform.localPosition = new Vector3(catImg.transform.localPosition.x + 50, catImg.transform.localPosition.y, catImg.transform.localPosition.z);
            //catImg.transform.DOLocalMoveX(localPos_X_Cat + 10f, 0.5f).OnComplete(() => { RightAnswer(); });
            RightAnswer();
            isEnd = true;
        }
    }
}
