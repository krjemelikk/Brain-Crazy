using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level_71 : BaseLevel
{
    [Header("Answers")]
    public Button[] btBall;
    public Sprite[] imgBreakBall;

    private Transform tfSkipLevel;
    private int countBalls;

    protected override void Start()
    {
        base.Start();
        countBalls = btBall.Length;
        tfSkipLevel = GameObject.FindGameObjectWithTag("ImageKeySkipTop").transform;
        for (int i = 0; i < btBall.Length; i++)
        {
            btBall[i].onClick.AddListener(() => OnClickBall());
        }
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void OnClickBall()
    {
        Debug.Log(countBalls);

        if(countBalls > 1)
        {
            WrongAnswer();
            return;
        }

        RightAnswer();
    }

    public void EndDrag()
    {
        //CheckAnswer();
    }

    int tempIndex = -1;
    public void OnBeginDrag(int index)
    {
        tempIndex = index;
    }

    bool isDeactive = false;
    public void OnDrag(Transform tf)
    {
        var distance = Vector2.Distance(tf.position, tfSkipLevel.transform.position);

        if (distance <= 0.35f)
        {
            if (countBalls <= 1)
            {
                return;
            }
            else
            {
                if (!isDeactive)
                {
                    isDeactive = true;
                    btBall[tempIndex].image.sprite = imgBreakBall[tempIndex];
                    StartCoroutine(DisableBall(btBall[tempIndex].gameObject));
                }            
            }
        }
    }

    private void CheckAnswer()
    {
        for (int i = 0; i < btBall.Length; i++)
        {
            if (!btBall[i].gameObject.activeInHierarchy) continue;

            var distance = Vector2.Distance(btBall[i].transform.position, tfSkipLevel.transform.position);

            if(distance <= 0.25f)
            {
                if(countBalls <= 1)
                {
                    return;
                }
                else
                {
                    btBall[i].image.sprite = imgBreakBall[i];
                    StartCoroutine(DisableBall(btBall[i].gameObject));
                }
            }
        }
    }

    IEnumerator DisableBall(GameObject g)
    {
        yield return new WaitForSeconds(0.5f);

        if (g != null) g.SetActive(false);
        countBalls--;
        isDeactive = false;
    }
}
