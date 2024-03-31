using UnityEngine;
using UnityEngine.UI;

public class Level_171 : BaseLevel
{
    public float maxMountScale = 1f;

    public GameObject Mount1Scale;
    public ZoomObject zoomObject1;
    public GameObject objDone1;

    public GameObject Mount2Scale;
    public ZoomObject zoomObject2;
    public GameObject objDone2;

    [Header("Answers")]
    public Button btOK;
    public InputField inputField;

    private int resultAnswer;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject1.isCanZoom = false;
            objDone1.SetActive(true);
        }, () => Mount1Scale.transform.localScale.x >= maxMountScale));

        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject2.isCanZoom = false;
            objDone2.SetActive(true);
        }, () => Mount2Scale.transform.localScale.x >= maxMountScale));

        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 7;
    }

    private void CheckAnswer()
    {
        if (objDone1.activeSelf && objDone2.activeSelf)
        {
            int _result = 0;
            if (string.IsNullOrEmpty(inputField.text) || !int.TryParse(inputField.text, System.Globalization.NumberStyles.Integer, null, out _result))
            {
                WrongAnswer();
                return;
            }

            if (_result == resultAnswer) RightAnswer();
            else WrongAnswer();
        }
        else WrongAnswer();
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
}
