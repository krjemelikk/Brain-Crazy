using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Level_108 : BaseLevel
{
    [ReadOnly] public int amountSwap = 0;
    public List<Level_108_Player> lsPlayerMain = new List<Level_108_Player>();
    public List<Level_108_Player> lsPlayer = new List<Level_108_Player>();

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
        base.WrongAnswer();
        amountSwap = 0;
        ResetAllPos();
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        ActiveRay(false);
        lsPlayerMain[0].transform.DOLocalMove(lsPlayer[9].transform.localPosition, 1f);
        lsPlayerMain[1].transform.DOLocalMove(lsPlayer[12].transform.localPosition, 1f);
        lsPlayerMain[2].transform.DOLocalMove(lsPlayer[20].transform.localPosition, 1f).OnComplete(() =>
        {
            StartCoroutine(ResetPlayer());
        });
    }

    public IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        ResetAllPos();
        ActiveRay(true);
    }

    private bool CheckAnswer()
    {
        foreach (Level_108_Player level in lsPlayerMain)
        {
            if (!level.isCheckDone)
                return false;
        }
        return true;
    }

    public void CheckDone()
    {
        if (amountSwap >= 3)
        {
            if (!CheckAnswer())
            {
                WrongAnswer();
                ActiveRay(true);
            }
            else
            {
                RightAnswer();
                ActiveRay(false);
            }
        }
    }

    public Vector3 GetPosPlayer(int ID)
    {
        return lsPlayer[ID].transform.localPosition;


    }

    public void ActiveRay(bool isActive)
    {
        foreach (Level_108_Player ball in lsPlayer)
        {
            var img = ball.GetComponent<Image>();
            if (img != null)
                img.raycastTarget = isActive;
        }
    }

    public void ResetAllPos()
    {
        foreach (Level_108_Player ball in lsPlayer)
        {
            ball.ResetPlayer();
        }
    }
}
