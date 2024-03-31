using UnityEngine;

public class Level_21 : BaseLevel
{
    public RectTransform theBox1;
    public RectTransform theBox2;
    public RectTransform Can;

    public WeightObject[] arrTransform;

    private int countItems;
    private Transform currentTransform;
    float maxX_1, minX_1, maxY_1, minY_1;
    float maxX_2, minX_2, maxY_2, minY_2;
    protected override void Start()
    {
        base.Start();
        countItems = 0;
        maxX_1 = theBox1.transform.localPosition.x + theBox1.rect.width / 2;
        minX_1 = theBox1.transform.localPosition.x - theBox1.rect.width / 2;
        minY_1 = theBox1.transform.localPosition.y - theBox1.rect.height / 2;
        maxY_1 = theBox1.transform.localPosition.y + theBox1.rect.height / 2;

        maxX_2 = theBox2.transform.localPosition.x + theBox2.rect.width / 2;
        minX_2 = theBox2.transform.localPosition.x - theBox2.rect.width / 2;
        minY_2 = theBox2.transform.localPosition.y - theBox2.rect.height / 2;
        maxY_2 = theBox2.transform.localPosition.y + theBox2.rect.height / 2;
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

    protected virtual void CheckAnswer()
    {
        bool isEnd = false;

        float weightBound_1 = 0;//Số cân nặng trên cân 1
        float weightBound_2 = 0;//Số cân nặng trên cân 2

        for (int i = 0; i < arrTransform.Length; i++)
        {
            if(CheckInBound_1(arrTransform[i].rectTransform))
            {
                isEnd = true;
                weightBound_1 += arrTransform[i].Weight;
            }

            if (CheckInBound_2(arrTransform[i].rectTransform))
            {
                isEnd = true;
                weightBound_2 += arrTransform[i].Weight;
            }

        }

        if (!isEnd)
        {
            Can.localRotation = Quaternion.Euler(0, 0, 0);
            RightAnswer();
        }
        else
        {
            if (weightBound_1 > weightBound_2)
            {
                Can.localRotation = Quaternion.Euler(0, 0, 5);
            }
            else
            {
                Can.localRotation = Quaternion.Euler(0, 0, -5);
            }
        }
    }

    public void BeginDrag(Transform tran)
    {
        currentTransform = tran;
    }

    public void EndDrag(RectTransform tran)
    {
        if (CheckInBound_1(tran))
        {
            tran.transform.localPosition = theBox1.transform.localPosition;
        }
        else if (CheckInBound_2(tran))
        {
            tran.transform.localPosition = theBox2.transform.localPosition;
        }

        CheckAnswer();
    }

    private bool CheckInBound_1(RectTransform transform)
    {
        return transform.transform.localPosition.x < maxX_1
            && transform.transform.localPosition.x > minX_1
            && transform.transform.localPosition.y > minY_1
            && transform.transform.localPosition.y < maxY_1;
    }

    private bool CheckInBound_2(RectTransform transform)
    {
        return transform.transform.localPosition.x < maxX_2
            && transform.transform.localPosition.x > minX_2
            && transform.transform.localPosition.y > minY_2
            && transform.transform.localPosition.y < maxY_2;
    }

    private void CheckCan()
    {
        int abc;
        for (int i = 0; i < arrTransform.Length; i++)
        {
            if(CheckInBound_1(arrTransform[i].rectTransform) || CheckInBound_2(arrTransform[i].rectTransform))
            { 
                abc = 1;
                break;
            }
            else
            {
                continue;
            }
        }
    }
}
