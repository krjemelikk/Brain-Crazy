using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Level_80 : BaseLevel
{
    public Button btStart;
    public RectTransform theRabbit;
    public RectTransform theTurtle;
    public RectTransform posEnd;
    public RectTransform bikeRabbit;
    public RectTransform bikeTurtle;
    public float speedRabbit;
    public float speedTurtle;

    private Vector3 posStartBikeRabbit;
    private Vector3 posStartBikeTurtle;
    private Vector3 posStartRabbit;
    private Vector3 posStartTurtle;
    public RectTransform panelRestart;
    

    private RectTransform currTransform;
    private bool isEnd = true;
    protected override void Start()
    {
        base.Start();
        btStart.onClick.AddListener(() => StartMove());
        posStartRabbit = theRabbit.position;
        posStartTurtle = theTurtle.position;
        posStartBikeRabbit = bikeRabbit.position;
        posStartBikeTurtle = bikeTurtle.position;
    }

    protected override void Update()
    {
        base.Update();

        if (isEnd)
            return;

        if (isRightSwitch)
        {
            theRabbit.Translate(Vector3.right * speedTurtle * Time.deltaTime);
            bikeRabbit.Translate(Vector3.right * speedRabbit * Time.deltaTime);


            theTurtle.Translate(Vector3.right * speedRabbit * Time.deltaTime);
            bikeTurtle.Translate(Vector3.right * speedTurtle * Time.deltaTime);
        }
        else
        {
            theRabbit.Translate(Vector3.right * speedRabbit * Time.deltaTime);
            bikeRabbit.Translate(Vector3.right * speedRabbit * Time.deltaTime);


            theTurtle.Translate(Vector3.right * speedTurtle * Time.deltaTime);
            bikeTurtle.Translate(Vector3.right * speedTurtle * Time.deltaTime);
        }
        


        CheckAnswer();
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
        panelRestart.gameObject.SetActive(true);
        theRabbit.transform.position = posStartRabbit;
        theTurtle.transform.position = posStartTurtle;
        bikeRabbit.position = posStartBikeRabbit;
        bikeTurtle.position = posStartBikeTurtle;
        bikeRabbit.gameObject.GetComponent<EventTrigger>().enabled = true;
        bikeTurtle.gameObject.GetComponent<EventTrigger>().enabled = true;
        isRightSwitch = false;
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (theRabbit.localPosition.x >= posEnd.localPosition.x)
        {
            isEnd = true;
            WrongAnswer();
            return;
        }

        if (theTurtle.localPosition.x >= posEnd.localPosition.x)
        {
            isEnd = true;
            RightAnswer();
        }

    }

    private void StartMove()
    {
        isEnd = false;
        panelRestart.gameObject.SetActive(false);
        bikeRabbit.gameObject.GetComponent<EventTrigger>().enabled = false;
        bikeTurtle.gameObject.GetComponent<EventTrigger>().enabled = false;
    }

    Vector3 remeberPos;
    public void BeginDrag(RectTransform tran)
    {
        currTransform = tran;
        Debug.Log("Name Â: " + currTransform.gameObject.name);
        remeberPos = currTransform.transform.position;
    }

    bool isRightSwitch = false;
    public void EndDrag()
    {
        if (currTransform.gameObject.name == "BikeRabbit")
        {
            var distance = Vector2.Distance(currTransform.position, theTurtle.transform.position);
            Debug.Log("distance " + distance);

            if (distance <= 0.5f)
            {
                bikeRabbit.position = posStartBikeTurtle;
                bikeTurtle.position = posStartBikeRabbit;
                isRightSwitch = true;
            }
            else
            {
                var distance_me = Vector2.Distance(currTransform.position, theRabbit.transform.position);
                if (distance_me <= 0.5f)
                {
                    bikeRabbit.position = posStartBikeRabbit;
                    bikeTurtle.position = posStartBikeTurtle;
                }
                else
                {
                    currTransform.transform.position = remeberPos;
                }
                //currTransform.transform.position = remeberPos;
                isRightSwitch = false;
            }
        }
        else if (currTransform.gameObject.name == "BikeTurtle")
        {
            var distance = Vector2.Distance(currTransform.position, theRabbit.transform.position);
            Debug.Log("distance " + distance);

            if (distance <= 0.5f)
            {
                bikeRabbit.position = posStartBikeTurtle;
                bikeTurtle.position = posStartBikeRabbit;
                isRightSwitch = true;
            }
            else
            {
                var distance_me = Vector2.Distance(currTransform.position, theTurtle.transform.position);

                if (distance_me <= 0.5f)
                {
                    bikeRabbit.position = posStartBikeRabbit;
                    bikeTurtle.position = posStartBikeTurtle;
                }
                else
                {
                    currTransform.transform.position = remeberPos;
                }
               // currTransform.transform.position = remeberPos;
                isRightSwitch = false;
            }
        }

    }
}
