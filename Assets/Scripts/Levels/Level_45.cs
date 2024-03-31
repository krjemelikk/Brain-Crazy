using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_45 : BaseLevel
{
    public List<GameObject> lsWater = new List<GameObject>();
    public Image waterMain;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CheckShakeTrigger();
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

    private Vector3 dir;
    private bool doneLevel;
    public void CheckShakeTrigger()
    {
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        if (dir.y >= 0.9f && waterMain.fillAmount != 0 && !doneLevel)
        {
            waterMain.DOFillAmount(0, 1f).OnComplete(() => RightAnswer()).OnUpdate(() =>
            {
                if (waterMain.fillAmount < 1)
                    lsWater[2].SetActive(false);
                else if (waterMain.fillAmount < 0.646)
                    lsWater[1].SetActive(false);
                else if (waterMain.fillAmount < 0.329)
                    lsWater[0].SetActive(false);
            }) ;
            doneLevel = true;
        }
    }
}