using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_244 : BaseLevel
{
    public DragUI dragSon;
    public DragUI dragCloud1;
    public DragUI dragCloud2;
    public DragUI dragTK;

    public Image viewRain;
    public Image viewCo;
    public Image viewTK;

    public Sprite spCo;
    public Sprite spTK;

    public bool isDone;
    public bool isDone2;
    public bool isDone3;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (!isDone)
        {
            if (Vector2.Distance(dragCloud1.transform.position, dragCloud2.transform.position) <= 0.25f)
            {
                viewRain.DOFillAmount(1f, 1f).OnComplete(() =>
                {
                    viewCo.sprite = spCo;
                    viewRain.gameObject.SetActive(false);
                });
                isDone = true;
            }
        }

        if (isDone && !isDone2)
        {
            if (Vector2.Distance(dragTK.transform.position, viewCo.transform.position) <= 0.25f)
            {
                viewTK.sprite = spTK;
                isDone2 = true;
            }
        }

        if (isDone && isDone2 && !isDone3)
        {
            if ((Vector2.Distance(dragSon.transform.position, txtQuestion.transform.position) <= 0.25f))
            {
                txtQuestion.color = Color.green;
                isDone3 = true;
            }
        }
    }

    public void CheckAnswer()
    {
        if (isDone && isDone2 && isDone3)
        {
            if (Mathf.Abs(dragCloud1.transform.localPosition.x) >= 300f && Mathf.Abs(dragCloud2.transform.localPosition.x) >= 300f)
            {
                if (dragCloud1.transform.localPosition.x > 0)
                    dragCloud1.transform.DOMoveX(1000f, 1f);
                else
                    dragCloud1.transform.DOMoveX(-1000f, 1f);

                if (dragCloud2.transform.localPosition.x > 0)
                    dragCloud2.transform.DOMoveX(1000f, 1f).OnComplete(() => RightAnswer());
                else
                    dragCloud2.transform.DOMoveX(-1000f, 1f).OnComplete(() => RightAnswer());
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
        GameController.Instance.ResetLevel();
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
