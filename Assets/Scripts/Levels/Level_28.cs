using UnityEngine;
using UnityEngine.UI;

public class Level_28 : BaseLevel
{
    [Header("Answers")]
    public Button btOK;
    public Button btNext;
    public Button btPre;
    public Text txtNumber;
    public GameObject treeScale;
    public float minScaleTree = 0.8f;

    private bool isCanRight;

    private int resultAnswer;

    private int answer;


    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        btNext.onClick.AddListener(() => OnClickNext());
        btPre.onClick.AddListener(() => OnClickPre());
        resultAnswer = 7;
        answer = 0;
        UpdateUI();

        isCanRight = false;
        StartCoroutine(Helper.StartAction(() => { isCanRight = true; }, () => treeScale.transform.localScale.x <= minScaleTree));
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
        if (answer == resultAnswer && isCanRight)//Phải từng thu nhỏ cái cây và nhập đúng kết quả mới tính là đúng
            RightAnswer();
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
