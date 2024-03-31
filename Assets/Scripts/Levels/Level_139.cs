using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_139 : BaseLevel
{
    public Image rocket;
    public Sprite spDoneRocket;

    public Transform v1;
    public Transform v2;
    public Transform v3;

    private float distancePos;

    private bool isEndV1;
    private bool isEndV2;
    private bool isEndV3;


    protected override void Start()
    {
        base.Start();
        distancePos = 1;
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
        rocket.sprite = spDoneRocket;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void EndDragObjectV1()
    {
        if (Vector2.Distance(rocket.transform.position, v1.position) > distancePos)
        {
            v1.gameObject.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            isEndV1 = true;
            CheckAnswer();
        }
    }

    public void EndDragObjectV2()
    {
        if (Vector2.Distance(rocket.transform.position, v2.position) > distancePos)
        {
            v2.gameObject.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            isEndV2 = true;
            CheckAnswer();
        }
    }

    public void EndDragObjectV3()
    {
        if (Vector2.Distance(rocket.transform.position, v3.position) > distancePos)
        {
            v3.gameObject.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            isEndV3 = true;
            CheckAnswer();
        }
    }

    public void CheckAnswer()
    {
       if (isEndV1
            && isEndV2 &&
            isEndV3)
        
        {
            RightAnswer();
        }
    }
}