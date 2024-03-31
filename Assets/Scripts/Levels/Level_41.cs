using UnityEngine;

public class Level_41 : BaseLevel
{
    public RectTransform[] theWaters;
    public int currentChoice;

    protected override void Start()
    {
        base.Start();
        currentChoice = -1;
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
        int leftActive = 0;
        for (int i = 0; i < theWaters.Length; i++)
        {
            if (theWaters[i].gameObject.activeSelf)
                leftActive++;
        }
        if (leftActive <= 1)
            RightAnswer();
    }

    public void BeginDrag(int currentChoice)
    {
        this.currentChoice = currentChoice;
    }

    public void EndDrag(RectTransform tran)
    {
        this.currentChoice = -1;
        //if (tran.transform.localPosition.x < maxX
        //    && tran.transform.localPosition.x > minX
        //    && tran.transform.localPosition.y > minY
        //    && tran.transform.localPosition.y < maxY)
        //{
        //    countItems++;
        //    tran.gameObject.SetActive(false);
        //    Vector3 scale = theWater.localScale + new Vector3(0.3f, 0.3f, 0);
        //    theWater.DOScale(scale, 0.25f).SetEase(Ease.InBack);
        //    CheckAnswer();
        //}
    }

    public void WaterUnify()
    {
        CheckAnswer();
    }
}
