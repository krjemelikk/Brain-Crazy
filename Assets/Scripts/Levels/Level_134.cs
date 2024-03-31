using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_134 : BaseLevel
{
    public Image cleanObj;
    public Transform box;
    public Image glass;
    public Transform posEnd;
    public Image shadow;
    private bool isDoneClean;
    private int amountClean = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDoneClean)
        {
            if (Vector3.Distance(cleanObj.transform.position, box.transform.position) > 0.5f)
            {
                cleanObj.raycastTarget = false;
                cleanObj.transform.DOScale(1f, 1f);
                cleanObj.transform.DOLocalMove(posEnd.localPosition, 1f).OnComplete(() =>
                {
                    isDoneClean = true;
                    cleanObj.raycastTarget = true;
                    cleanObj.GetComponent<BoxCollider2D>().enabled = true;
                });
            }
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
        if(isDoneClean && amountClean < 3)
        {
            amountClean++;
            if (amountClean >= 3)
            {
                cleanObj.raycastTarget = false;
                glass.DOFade(0f, 0.5f);
                shadow.DOFade(0f, 0.5f).OnComplete(()=> 
                {
                    RightAnswer();
                });
            }
        }
    }
}
