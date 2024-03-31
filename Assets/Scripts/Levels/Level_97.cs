using UnityEngine;
using DG.Tweening;

public class Level_97 : BaseLevel
{
    [Header("Object")]
    public NumberScene[] numbers;

    [Header("Farme")]
    public FarmeScene[] farmes;

    public Transform parentNumberFirst;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].remberPos = numbers[i].objectNumber.transform.position;
        }
        
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

    public void OnEndDragObject(int indexNumber)
    {
        bool isAdded = false;
        int indexAdded = -1;
        for (int i = 0; i < farmes.Length; i++)
        {
            if (farmes[i].isHasNumberOn)
                continue;
            if (Vector2.Distance(numbers[indexNumber].objectNumber.transform.position, farmes[i].objectFarme.transform.position) <= 0.25f)
            {
                isAdded = true;
                indexAdded = i;
                break;
            }
        }

        if (isAdded)
        {
            if (numbers[indexNumber].remberIndexParent != -1)
            {
                farmes[numbers[indexNumber].remberIndexParent].isHasNumberOn = false;
                farmes[numbers[indexNumber].remberIndexParent].value = -1;
            }

            numbers[indexNumber].objectNumber.transform.transform.parent = farmes[indexAdded].objectFarme.transform;
            numbers[indexNumber].objectNumber.transform.DOKill();
            numbers[indexNumber].objectNumber.transform.DOMove(farmes[indexAdded].objectFarme.transform.position, 0.5f);
            farmes[indexAdded].value = numbers[indexNumber].value;
            farmes[indexAdded].isHasNumberOn = true;
            numbers[indexNumber].remberIndexParent = indexAdded;

            CheckAnswer();
        }
        else
        {
            numbers[indexNumber].objectNumber.transform.DOKill();
            numbers[indexNumber].objectNumber.transform.DOMove(numbers[indexNumber].remberPos, 0.5f);
            numbers[indexNumber].objectNumber.transform.transform.parent = parentNumberFirst;

            if (numbers[indexNumber].remberIndexParent != -1)
            {
                farmes[numbers[indexNumber].remberIndexParent].isHasNumberOn = false;
                farmes[numbers[indexNumber].remberIndexParent].value = -1;
                numbers[indexNumber].remberIndexParent = -1;
            }
        }
    }
    
    private void CheckAnswer()
    {
        bool isFullPos = true;
        for (int i = 0; i < farmes.Length; i++)
        {
            if (farmes[i].objectFarme.transform.childCount == 0)
            {
                isFullPos = false;
            }
        }

        if (isFullPos)
        {
            int cong_1 = farmes[0].value * 100 + farmes[1].value * 10 + farmes[2].value;
            int cong_2 = 90 + farmes[3].value;
            int kq = farmes[4].value * 100 + farmes[5].value * 10 + farmes[6].value;

            if (cong_1 + cong_2 == kq)
            {
                RightAnswer();
            }
            else
            {
                WrongAnswer();
            }
        }
    }
}

[System.Serializable]
public class NumberScene
{
    public int value;
    public GameObject objectNumber;
    [HideInInspector] public Vector3 remberPos;
   public int remberIndexParent = -1;
}

[System.Serializable]
public class FarmeScene
{
    public bool isHasNumberOn;
    public GameObject objectFarme;
    [HideInInspector] public int value;
}

