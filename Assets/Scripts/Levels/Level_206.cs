using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_206 : BaseLevel
{
    public DragUI dragSun;

    public Image bgHide;
    public Image viewSun;
    public Sprite spMoon;

    public Transform tfCheckDone;

    private bool isDone;

    private Vector3 posSun;
    protected override void Start()
    {
        base.Start();
        posSun = dragSun.transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (isDone) return;
        if (Vector2.Distance(dragSun.transform.position, tfCheckDone.position) <= 0.15f)
        {
            isDone = true;
            dragSun.SetActiveDrag(false);
            viewSun.sprite = spMoon;
            viewSun.SetNativeSize();
            dragSun.transform.DOMove(posSun, 1f).OnComplete(()=> 
            {
                bgHide.color = new Color(0f, 0f, 0f, 0.5f);
                RightAnswer(); 
            });
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

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }
}