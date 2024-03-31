using UnityEngine;
using UnityEngine.UI;

public class Level_91 : BaseLevel
{
    [Header("Answers")]
    public int numCats;

    public Transform[] catChilds;
    private Vector3[] remeberCatChildPos;

    [SerializeField] private Button preBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button clearBtn;
    [SerializeField] private Button okBtn;
    [SerializeField] private Text numberTxt;

    private int numberResult;

    protected override void Start()
    {
        base.Start();
        remeberCatChildPos = new Vector3[catChilds.Length];
        for (int i = 0; i < catChilds.Length; i++)
        {
            remeberCatChildPos[i] = catChilds[i].position;
        }

        preBtn.onClick.RemoveAllListeners();
        preBtn.onClick.AddListener(PreHandle);

        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(NextHandle);

        clearBtn.onClick.RemoveAllListeners();
        clearBtn.onClick.AddListener(ClearHandle);

        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(OkHandle);

        numberTxt.text = "0";
        numberResult = 0;
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

    private void PreHandle()
    {
        numberResult--;
        if (numberResult < 0)
            numberResult = 0;

        numberTxt.text = numberResult.ToString();
    }

    private void NextHandle()
    {
        numberResult++;
        numberTxt.text = numberResult.ToString();
    }

    private void OkHandle()
    {
        CheckAnswerHandle();
    }

    private void ClearHandle()
    {
        numberResult = 0;
        numberTxt.text = numberResult.ToString();
    }

    private void CheckAnswerHandle()
    {
        bool isMoved = false;
        for (int i = 0; i < catChilds.Length; i++)
        {
            if(remeberCatChildPos[i] != catChilds[i].position)
            {
                isMoved = true;
                break;
            }
        }

        if(isMoved)
        {
            if(numberResult == numCats)
            {
                RightAnswer();
            }
            else
            {
                WrongAnswer();
            }
        }
        else
        {
            WrongAnswer();
        }
    }
}
