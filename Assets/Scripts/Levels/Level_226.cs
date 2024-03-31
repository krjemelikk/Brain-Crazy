using UnityEngine;
using UnityEngine.UI;

public class Level_226 : BaseLevel
{
    public DragUI dragUI1;
    public DragUI dragUI2;

    public Transform tfCheck;

    private bool isDone1;
    private bool isDone2;

    public Image viewDone;

    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone1 && dragUI2.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI2.transform.position) <= 0.25f)
            {
                dragUI1.GetComponent<Image>().raycastTarget = true;
                dragUI2.SetActiveDrag(false);
                dragUI2.transform.SetParent(dragUI1.transform);
                dragUI2.transform.localPosition = Vector3.zero;
                dragUI2.transform.SetAsFirstSibling();
                isDone1 = true;
            }
        }

        if (isDone1 && !isDone2)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfCheck.position) <= 0.25f)
            {
                dragUI1.SetActiveDrag(false);
                dragUI1.transform.SetParent(tfCheck);
                dragUI1.transform.localPosition = Vector3.zero;
                StartCoroutine(Helper.StartAction(() =>
                {
                    viewDone.sprite = sp1;
                    StartCoroutine(Helper.StartAction(() =>
                    {
                        viewDone.sprite = sp2;
                        StartCoroutine(Helper.StartAction(() =>
                        {
                            viewDone.sprite = sp3;
                            RightAnswer();
                        }, 0.5f));
                    }, 0.5f));
                }, 0.5f));
                isDone2 = true;
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
