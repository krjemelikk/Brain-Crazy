using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_236 : BaseLevel
{
    public float maxCanoScale = 1f;

    public GameObject SitScale;
    public ZoomObject zoomObject;

    public GameObject SitScale2;
    public ZoomObject zoomObject2;

    public List<GameObject> lsObj = new List<GameObject>();

    [Header("Answers")]
    public Button btOK;
    public InputField inputField;

    private int resultAnswer;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject2.isCanZoom = false;
            for (int i = 0; i < 4; i++)
            {
                lsObj[i].SetActive(true);
            }
        }, () => SitScale2.transform.localScale.x >= maxCanoScale));

        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
            for (int i = 4; i < 8; i++)
            {
                lsObj[i].SetActive(true);
            }
        }, () => SitScale.transform.localScale.x >= maxCanoScale));

        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 12;
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
        int _result = 0;
        if (string.IsNullOrEmpty(inputField.text) || !int.TryParse(inputField.text, System.Globalization.NumberStyles.Integer, null, out _result) || zoomObject.isCanZoom || zoomObject2.isCanZoom)
        {
            WrongAnswer();
            return;
        }

        if (_result == resultAnswer) RightAnswer();
        else WrongAnswer();
    }
}
