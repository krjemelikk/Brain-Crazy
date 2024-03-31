using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_119 : BaseLevel
{
    public Image man;
    public DragUI vay;
    public Transform posCheck;
    public Sprite spMan;
    public Transform posManGoWC;
    public Transform posFixDoor;
    private bool isComplete;

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
        if (isComplete)
            return;
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public void CheckAnswer()
    {
        if(Vector2.Distance(vay.transform.position,posCheck.position) > 0.05f)
        {
            vay.SetActiveDrag(false);
            man.sprite = spMan;
            vay.transform.DOMove(posFixDoor.transform.position, 0.2f).OnComplete(() =>
            {
                man.transform.DOMove(posManGoWC.transform.position, 1f).OnComplete(() =>
                {
                    man.gameObject.SetActive(false);
                    RightAnswer();
                });
            });
            isComplete = true;
        }
        else
        {
            WrongAnswer();
        }
    }
}
