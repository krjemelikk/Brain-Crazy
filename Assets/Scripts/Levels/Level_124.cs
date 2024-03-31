using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class Level_124 : BaseLevel
{
    [Header("Answers")]
    public Level_124_Car car;

    public List<RectTransform[]> lsMoveCar = new List<RectTransform[]>();

    public List<Level_124_Car> pooledObjects;

    public bool isStart;

    private float timePlay;
    private float timeDelay;
    private float timeDelta;
    private float timeCurrent = 10f;
    [SerializeField] private float timeEnd;

    [SerializeField] private Text txtTimeReset;

    [SerializeField] private GameObject btnPlay;

    protected override void Start()
    {
        base.Start();
        timeCurrent = 10;
        OnclickStartLevel();
    }

    protected override void Update()
    {
        base.Update();

        if (isStart)
        {
            timePlay += Time.deltaTime;
            timeDelay += Time.deltaTime;
            timeDelta += Time.deltaTime;

            //if (timeDelta >= 1f)
            //{
            //    timeCurrent -= 1f;
            //    txtTimeReset.text = timeCurrent.ToString();
            //    timeDelta = 0;
            //}

            if (timeDelay >= 0.5f)
            {
                Move_Car();
                timeDelay = 0;
            }

            //if (timePlay > timeEnd)
            //{
            //    RightAnswer();
            //    isStart = false;
            //}
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
        isStart = false;
        btnPlay.SetActive(true);
        txtTimeReset.text = timeEnd.ToString();
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

    private void Move_Car()
    {
        Level_124_Car car = GetPooledObject();
        car.gameObject.SetActive(true);
        int randomIndexArr = UnityEngine.Random.Range(0, 4);
        int radomDir = UnityEngine.Random.Range(0, 2);
        car.isLeft = radomDir == 0 ? true : false;
        float timeRun = UnityEngine.Random.Range(3f, 4f);
        car.dirCar = lsMoveCar[randomIndexArr];
        if (car.isLeft)
        {
            car.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            car.transform.localPosition = lsMoveCar[randomIndexArr][0].localPosition;
            car.transform.DOLocalMove(lsMoveCar[randomIndexArr][1].localPosition, timeRun);
        }
        else
        {
            car.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            car.transform.localPosition = lsMoveCar[randomIndexArr][1].localPosition;
            car.transform.DOLocalMove(lsMoveCar[randomIndexArr][0].localPosition, timeRun).OnComplete(() => car.gameObject.SetActive(false));
        }
    }

    public void CompleteCar(Level_124_Car car)
    {
        float timeRun = UnityEngine.Random.Range(1.5f, 4f);
        car.transform.DOKill();
        car.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        car.transform.DOLocalMove(car.dirCar[0].localPosition, timeRun).OnComplete(() => car.gameObject.SetActive(false));

        timeCurrent -= 1f;
        txtTimeReset.text = timeCurrent.ToString();
        if (timeCurrent <= 0)
        {
            timeCurrent = 0;
            txtTimeReset.text = timeCurrent.ToString();
            RightAnswer();
        }
    }

    public Level_124_Car GetPooledObject()
    {
        Level_124_Car obj = FindAvailableObject(pooledObjects);

        if (obj != null)
        {
            return obj;
        }
        else
        {
            Level_124_Car obj2 = (Level_124_Car)Instantiate(car, transform);
            pooledObjects.Add(obj2);
            obj2.gameObject.SetActive(false);
            return obj2;
        }
    }

    private Level_124_Car FindAvailableObject(List<Level_124_Car> pooledObjects)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
                return pooledObjects[i];
        }
        return null;
    }

    public void OnclickStartLevel()
    {
        timePlay = 0f;
        timeDelay = 0f;
        timeDelta = 0f;
        timeCurrent = timeEnd;
        txtTimeReset.text = timeCurrent.ToString();
        isStart = true;
        btnPlay.SetActive(false);
    }
}
