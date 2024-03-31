using UnityEngine;
using UnityEngine.UI;

public class Level_159 : BaseLevel
{
    public Image voi;

    public InputField txtWeight;

    public Transform end;

    private bool isEnd;
    private bool isChangeWeight;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if (isChangeWeight)
        {
            if(voi.transform.position.y >= end.position.y)
            {
                RightAnswer();
                isEnd = true;
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

    public void CheckDone()
    {
        int weight;
        if( int.TryParse(txtWeight.text,out weight))
        {
            if(weight <= 1)
            {
                voi.raycastTarget = true;
                txtWeight.readOnly = true;
                txtWeight.GetComponent<Image>().raycastTarget = false;
                //txtWeight.interactable = false;
                isChangeWeight = true;
            }
        }
    }
}
