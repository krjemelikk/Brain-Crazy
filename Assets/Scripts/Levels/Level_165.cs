using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_165 : BaseLevel
{
    [Header("Answers")]
    public List<Image> lsHouse = new List<Image>();
    public List<Sprite> lsSprite = new List<Sprite>();
    private int coutDestroy;

    public GameObject map;
    private Vector3 posStart;

    protected override void Start()
    {
        base.Start();
        coutDestroy = 0;
        posStart = map.transform.position;
    }

    public void ShakeBG()
    {
        //if(Input.GetMouseButtonDown(0))
        {
            map.transform.DOKill();
            map.transform.position = posStart;
            map.transform.DOPunchPosition(new Vector3(30, 30, 0), 0.2f).SetLoops(2);

            if (coutDestroy < 3)
            {
                lsHouse[coutDestroy].sprite = lsSprite[coutDestroy];
                // WrongAnswer();
            }
            else
            {
                return;
            }
            coutDestroy++;
        }
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

    public void CheckAnswer()
    {
        if (coutDestroy >= 3)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }

    }


}
