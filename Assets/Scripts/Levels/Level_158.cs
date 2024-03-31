using UnityEngine;
using UnityEngine.UI;

public class Level_158 : BaseLevel
{
    [SerializeField] private DragUI pinObj;
    [SerializeField] private DragUI tongder;
    [SerializeField] private GameObject hairObj;
    private bool isEnd;
    private bool addedPin;
    private bool isChoicePin;

    [Header("Answers")]
    public Button btOK;
    public InputField inputField;

    private int resultAnswer;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 0;
    }

    public void ChoicePin(bool isChoice)
    {
        isChoicePin = isChoice;
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if (isChoicePin && !addedPin)
        {
            if (Vector2.Distance(pinObj.transform.position, tongder.transform.position) <= 0.15f)
            {
                addedPin = true;
                pinObj.gameObject.SetActive(false);
            }
        }

        if(addedPin)
        {
            if (Vector2.Distance(hairObj.transform.position, tongder.transform.position) <= 0.2f)
            {
                hairObj.gameObject.SetActive(false);
                isEnd = true;
            }
        }
    }

    private void CheckAnswer()
    {
        if (!isEnd)
        {
            WrongAnswer();
            return;
        }

        int _result = 0;
        if (string.IsNullOrEmpty(inputField.text) || !int.TryParse(inputField.text, System.Globalization.NumberStyles.Integer, null, out _result))
        {
            WrongAnswer();
            return;
        }

        if (_result == resultAnswer) RightAnswer();
        else WrongAnswer();
    }
}
