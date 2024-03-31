using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum TypeWaterPipe
{
    Curved = 0,
    Straight = 1,
    Vertical = 2
}

[System.Serializable]
public class WaterPipe
{
    public TypeWaterPipe type;
    public float valueAlpha;
    public Button pipeBtn;
    public float currentAngle;
    [HideInInspector] public bool isRotating;
    public bool isRight;
}

public class Level_60 : BaseLevel
{
    public WaterPipe[] WaterPipes;
    private bool isEnd;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < WaterPipes.Length; i++)
        {
            int index = i;
            WaterPipes[i].pipeBtn.onClick.AddListener(() => { ChangeWaterPipe(index); });
        }
        WaterPipes[0].isRight = true;
        WaterPipes[3].isRight = true;
        WaterPipes[6].isRight = true;
        WaterPipes[7].isRight = true;
        WaterPipes[9].isRight = true;
        CheckAnswer();
    }

    public void ChangeWaterPipe(int index)
    {
        if (isEnd)
            return;
        if (WaterPipes[index].isRotating)
            return;

        WaterPipes[index].isRotating = true;
        WaterPipes[index].currentAngle += 90;
        if (WaterPipes[index].currentAngle >= 360)
            WaterPipes[index].currentAngle = 0;
        WaterPipes[index].pipeBtn.gameObject.transform.parent.DOKill();
        WaterPipes[index].pipeBtn.gameObject.transform.parent.DORotate(new Vector3(0, 0, WaterPipes[index].currentAngle), 0.5f).OnComplete(() =>
         {
             WaterPipes[index].pipeBtn.gameObject.transform.parent.DOKill();
             if (WaterPipes[index].currentAngle < 0)
                 WaterPipes[index].currentAngle += 360;
             //Debug.Log("Curr " + WaterPipes[index].currentAngle);
             WaterPipes[index].pipeBtn.gameObject.transform.parent.rotation = Quaternion.Euler(0, 0, WaterPipes[index].currentAngle);
             WaterPipes[index].isRotating = false;

             if (WaterPipes[index].type == TypeWaterPipe.Straight)//Ngang
             {
                 if ((WaterPipes[index].currentAngle <= 90.1f && WaterPipes[index].currentAngle >= 89.9f) || (WaterPipes[index].currentAngle <= 270.1f && WaterPipes[index].currentAngle >= 269.9f))
                 {
                     WaterPipes[index].isRight = true;
                 }
                 else
                 {
                     WaterPipes[index].isRight = false;
                 }
             }
             else if (WaterPipes[index].type == TypeWaterPipe.Vertical)
             {
                 if ((WaterPipes[index].currentAngle <= 0.1f && WaterPipes[index].currentAngle >= 0f) || (WaterPipes[index].currentAngle <= 180.1f && WaterPipes[index].currentAngle >= 179.9f))
                 {
                     WaterPipes[index].isRight = true;
                 }
                 else
                 {
                     WaterPipes[index].isRight = false;
                 }
             }
             else
             {
                 if (WaterPipes[index].currentAngle <= WaterPipes[index].valueAlpha + 0.1f && WaterPipes[index].currentAngle >= WaterPipes[index].valueAlpha - 0.1f)
                 {
                     WaterPipes[index].isRight = true;
                 }
                 else
                 {
                     WaterPipes[index].isRight = false;
                 }
             }
             CheckAnswer();
         });

    }

    private void CheckAnswer(int index = 0)
    {
        bool isRight = true;

        for (int i = 0; i < WaterPipes.Length; i++)
        {
            if(!WaterPipes[i].isRight)
            {
                isRight = false;
                break;
            }
        }

        if(isRight)
        {
            RightAnswer();
            isEnd = true;
        }
    }
}
