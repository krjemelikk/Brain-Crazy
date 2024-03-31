using UnityEngine;
using UnityEngine.UI;

public class Level_160 : BaseLevel
{
    public Image btnLeft;
    public Image btnRight;

    public Image imgLeft;
    public Image imgRight;

    public GameObject objDone;
    public Image imgDone1;
    public Image imgDone2;
    public Image imgCoin;

    public Sprite spNone;

    private bool isDone = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (!isDone && Vector2.Distance(btnLeft.transform.position, btnRight.transform.position) <= 0.5f)
        {
            objDone.transform.position = (imgLeft.transform.position + imgRight.transform.position) / 2f;
            objDone.SetActive(true);
            imgLeft.gameObject.SetActive(false);
            imgRight.gameObject.SetActive(false);
            btnLeft.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(false);
            isDone = true;
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

        StartCoroutine(Helper.StartAction(() =>
        {
            GameController.Instance.ResetLevel();
        }, 1f));
    }

    public override void RightAnswer()
    {
        imgDone1.sprite = spNone;
        imgDone2.sprite = spNone;
        imgDone1.SetNativeSize();
        imgDone2.SetNativeSize();
        imgCoin.gameObject.SetActive(true);
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void CheckWrongAnswer(bool isLeft)
    {
        if (isDone) return;

        if (isLeft)
        {
            imgLeft.sprite = spNone;
            imgLeft.SetNativeSize();
        }
        else
        {
            imgRight.sprite = spNone;
            imgRight.SetNativeSize();
        }

        WrongAnswer();
    }
}
