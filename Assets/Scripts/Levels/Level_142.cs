using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_142 : BaseLevel
{
    private int countBoom = 0;

    public Image imgBoom;
    public Sprite spBoom;

    public bool isDoneBoom;
    public GameObject thief;
    public Transform rightDoor;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StartLevel()
    {
        base.StartLevel();
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();
    }

    public override void WrongAnswer()
    {
        countBoom = 0;
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void Boom()
    {
        countBoom++;
        if(countBoom >= 3)
        {
            imgBoom.sprite = spBoom;
            imgBoom.transform.localScale = 1.5f * Vector3.one;
            Helper.StartActionNotUseCorutines(() => 
            {
                imgBoom.gameObject.SetActive(false);
                isDoneBoom = true;

                thief.transform.DOMove(rightDoor.transform.position, 0.7f).OnComplete(() => { thief.gameObject.SetActive(false); RightAnswer(); }).SetUpdate(true);
            }, 1f);
        }
    }

    public void OnclickDone()
    {
        if (isDoneBoom)
        {
            
        }
        else
            WrongAnswer();
    }
}