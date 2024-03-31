using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class Level_147 : BaseLevel
{
    public Image imgX;

    public Transform tfO;

    public Transform posODone;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isDone)
            return;

        if (Vector2.Distance(tfO.position, posODone.position) <= 0.5f)
        {
            isDone = true;
            imgX.gameObject.SetActive(true);
            tfO.DOMove(posODone.position, 0.5f);
            tfO.GetComponent<DragUI>().SetActiveDrag(false);
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

    public void OnclickDone()
    {
        if (isDone)
        {
            imgX.DOColor(new Color32(255, 255, 255, 255), 1f).OnComplete(() =>
                {
                    RightAnswer();
                });
        }
    }
}
