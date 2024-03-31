using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_114 : BaseLevel
{
    public Transform thunder;
    public DragUI cloud;
    public DragUI cloud1;
    public Image lion;
    public Image human;
    public Sprite spLionRun;

    private Vector3 posCloudStart;
    private Vector3 posCloudStart1;
    private Vector3 posHumanStart;
    private Vector3 posLionStart;

    public bool isComplete;

    protected override void Start()
    {
        base.Start();
        posCloudStart = cloud.transform.position;
        posCloudStart1 = cloud1.transform.position;
        posHumanStart = human.gameObject.transform.position;
        posLionStart = lion.gameObject.transform.position;
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
        cloud.transform.position = posCloudStart;
        cloud1.transform.position = posCloudStart1;
        human.gameObject.transform.position = posHumanStart;
        lion.gameObject.transform.position = posLionStart;
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
        cloud.SetActiveDrag(false);
        thunder.DOScale(1.1f, 0.1f).OnComplete(()=> 
        {
            lion.sprite = spLionRun;
            lion.SetNativeSize();
            RightAnswer();
        });
    }
}
