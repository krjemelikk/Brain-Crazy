using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Level_48 : BaseLevel
{
    [Header("Answers")]
    public Button theSwitch_1;
    public Button theSwitch_2;
    public Button theSwitch_3;
    public Image imgLight_1;
    public Image imgLight_2;
    public Image imgLight_3;
    public Sprite[] sprLight;
    public Sprite[] sprSwitch;

    private int totalLight;
    private int countLight = 0;

    private bool isOn_1;
    private bool isOn_2;
    private bool isOn_3;

    protected override void Start()
    {
        base.Start();
        theSwitch_1.onClick.AddListener(() => { if(!isOn_1) OnClickSwitch(1); });
        theSwitch_2.onClick.AddListener(() => { if (!isOn_2) OnClickSwitch(2); });
        theSwitch_3.onClick.AddListener(() => { if (!isOn_3) OnClickSwitch(3); });
        totalLight = 3;
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

    private void CheckAnswer()
    {
        if (countLight == totalLight)
        {
            RightAnswer();
        }
    }

    private void OnClickSwitch(int numberSwitch)
    {
        if (numberSwitch == 1)
        {
            if (imgLight_1.gameObject.activeInHierarchy)
            {
                SetStateLight_1(true);
                countLight -= -1;
            }
            else
            {
                return;
            }
        }
        else if (numberSwitch == 2)
        {
            if (imgLight_2.gameObject.activeInHierarchy)
            {
                SetStateLight_2(true);
                countLight -= -1;
            }
            else
            {
                return;
            }
        }
        else if (numberSwitch == 3)
        {
            if (imgLight_3.gameObject.activeInHierarchy)
            {
                SetStateLight_3(true);
                countLight -= -1;
            }
            else
            {
                return;
            }
        }

        if (countLight >= 3)
        {
            GameController.Instance.ResetLevel();
            //countLight = 0;
            //SetStateLight_1(false);
            //SetStateLight_2(false);
            //SetStateLight_3(false);
            return;
        }

        CheckAnswer();
    }

    public void OnEndDrag(RectTransform tran)
    {
        if (tran.localPosition.x - tran.rect.width / 2 < GameController.Instance.HomeScene.BoundLeft.localPosition.x
            || tran.localPosition.x + tran.rect.width / 2 > GameController.Instance.HomeScene.BoundRight.localPosition.x
            || tran.localPosition.y + tran.rect.height / 2 > GameController.Instance.HomeScene.BoundTop.localPosition.y
            || tran.localPosition.y - tran.rect.height / 2 < GameController.Instance.HomeScene.BoundBottom.localPosition.y)
        {
            tran.gameObject.SetActive(false);
            totalLight = 2;
            CheckAnswer();
        }
 
        ActiveDrag();
    }

    private void SetStateLight_1(bool isOn)
    {
        isOn_1 = isOn;
        imgLight_1.sprite = isOn ? sprLight[1] : sprLight[0];
        theSwitch_1.GetComponent<Image>().sprite = isOn ? sprSwitch[1] : sprSwitch[0];
        imgLight_1.transform.GetComponent<EventTrigger>().enabled = !isOn;
    }

    private void SetStateLight_2(bool isOn)
    {
        isOn_2 = isOn;
        imgLight_2.sprite = isOn ? sprLight[1] : sprLight[0];
        theSwitch_2.GetComponent<Image>().sprite = isOn ? sprSwitch[1] : sprSwitch[0];
        imgLight_2.transform.GetComponent<EventTrigger>().enabled = !isOn;
    }

    private void SetStateLight_3(bool isOn)
    {
        isOn_3 = isOn;
        imgLight_3.sprite = isOn ? sprLight[1] : sprLight[0];
        theSwitch_3.GetComponent<Image>().sprite = isOn ? sprSwitch[1] : sprSwitch[0];
        imgLight_3.transform.GetComponent<EventTrigger>().enabled = !isOn;
    }

    private void ActiveDrag()
    {
        if (totalLight == 3)
        {
            imgLight_1.transform.GetComponent<EventTrigger>().enabled = true;
            imgLight_2.transform.GetComponent<EventTrigger>().enabled = true;
            imgLight_3.transform.GetComponent<EventTrigger>().enabled = true;
        }
        else if(totalLight == 2)
        {
            imgLight_1.transform.GetComponent<EventTrigger>().enabled = false;
            imgLight_2.transform.GetComponent<EventTrigger>().enabled = false;
            imgLight_3.transform.GetComponent<EventTrigger>().enabled = false;
        }
    }
}