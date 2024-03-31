using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_102 : BaseLevel
{
    public GameObject prefabsEgg;
    public GameObject O_Egg;
    private float timePlay;
    private float timeDelay;
    private float timeDelta;
    private float timeCurrent = 10f;
    [SerializeField] private float timeEnd;
    private bool isStastFallingEgg;
    [SerializeField] private GameObject btnPlay;

    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    private List<GameObject> lsEgg = new List<GameObject>();

    [SerializeField] private Text txtTimeReset;

    [SerializeField] private GameObject checkFaill;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isStastFallingEgg)
        {
            timePlay += Time.deltaTime;
            timeDelay += Time.deltaTime;
            timeDelta += Time.deltaTime;

            if (timeDelta >= 1f)
            {
                timeCurrent -= 1f;
                txtTimeReset.text = timeCurrent.ToString();
                timeDelta = 0;
            }

            if (timeDelay >= 0.5f)
            {
                FallingEgg();
                timeDelay = 0;
            }

            if (timePlay > timeEnd)
            {
                RightAnswer();
                isStastFallingEgg = false;
            }
        }
    }

    public override void StartLevel()
    {
        base.StartLevel();

        checkFaill.transform.position = new Vector3(checkFaill.transform.position.x,
            Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y);
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();

        //GameController.Instance.HomeScene.Rate();
    }

    public override void WrongAnswer()
    {
        base.WrongAnswer();
        isStastFallingEgg = false;
        O_Egg.transform.localPosition = new Vector3(-21f, -362f, 0f);
        btnPlay.SetActive(true);
        txtTimeReset.text = 10.ToString();
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

    public void StartFallingEgg()
    {
        timePlay = 0f;
        timeDelay = 0f;
        timeDelta = 0f;
        timeCurrent = 10f;
        txtTimeReset.text = timeCurrent.ToString();
        isStastFallingEgg = true;
        btnPlay.SetActive(false);
    }

    public void FallingEgg()
    {
        float xR = Random.Range(left.localPosition.x, right.localPosition.x);
        float yR = left.position.y;
        GameObject eggClone = Instantiate(prefabsEgg, transform);
        lsEgg.Add(eggClone);
        eggClone.transform.localPosition = new Vector3(xR, yR + 200f, 0f);
        eggClone.SetActive(true);
    }

    public void FailQuest()
    {
        WrongAnswer();
    }

    public void RemoveEgg(GameObject egg)
    {
        lsEgg.Remove(egg);
    }
}
