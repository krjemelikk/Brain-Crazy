using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Level_27 : BaseLevel
{
    [SerializeField] private GameObject[] objectsHint;

    [Header("Answers")]
    public Button btOK;
    public Button btNext;
    public Button btPre;
    public Text txtNumber;

    private int resultAnswer;

    private int answer;

    private bool isUsedHint;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        btNext.onClick.AddListener(() => OnClickNext());
        btPre.onClick.AddListener(() => OnClickPre());

        for (int i = 0; i < objectsHint.Length; i++)
        {
            objectsHint[i].SetActive(false);
        }

        resultAnswer = 4;
        answer = 0;
        UpdateUI();
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
        if (!DataManager.SuggestedHint)
        {
            DataManager.SuggestedHint = true;
            _disposeHand?.Dispose();
        }

        isUsedHint = true;
        StartCoroutine(ShowNumber());
    }

    private void CheckAnswer()
    {
        if (answer == resultAnswer)
        {
            if (!isUsedHint)
                StartCoroutine(ShowNumber(RightAnswer));
            else
                RightAnswer();
        }

        else WrongAnswer();
    }

    private void OnClickNext()
    {
        answer++;
        UpdateUI();
    }

    private void OnClickPre()
    {
        answer--;
        if (answer < 0)
            answer = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        txtNumber.text = answer.ToString();
    }

    private IEnumerator ShowNumber(UnityAction action = null)
    {
        objectsHint[0].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        objectsHint[1].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        objectsHint[2].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        objectsHint[3].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        if (action != null)
            action();
    }
}
