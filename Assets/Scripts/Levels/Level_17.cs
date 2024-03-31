using DG.Tweening;
using UnityEngine;

public class Level_17 : BaseLevel
{
    public RectTransform theBox;
    public AnimationCurve curve;

    private int countItems;
    private Transform currentTransform;
    float maxX, minX, maxY, minY;
    protected override void Start()
    {
        base.Start();
        countItems = 0;
        maxX = theBox.transform.localPosition.x + theBox.rect.width / 2;
        minX = theBox.transform.localPosition.x - theBox.rect.width / 2;
        minY = theBox.transform.localPosition.y - theBox.rect.height / 2;
        maxY = theBox.transform.localPosition.y + theBox.rect.height / 2;
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
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (countItems >= 5)
        {
            RightAnswer();
        }
    }

    public void BeginDrag(Transform tran)
    {
        currentTransform = tran;
    }

    public void EndDrag(RectTransform tran)
    {
        if (tran.transform.localPosition.x < maxX
            && tran.transform.localPosition.x > minX
            && tran.transform.localPosition.y > minY
            && tran.transform.localPosition.y < maxY)
        {
            countItems++;
            tran.gameObject.SetActive(false);
            theBox.DOScale(1.3f, 0.5f).SetEase(curve);
            CheckAnswer();
        }
    }
}
