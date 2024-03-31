using UnityEngine;
using UnityEngine.UI;

public class Level_76 : BaseLevel
{
    [Header("Answers")]
    public Button[] theWrongs;
    public Button theRadio;

    private bool isEnd;
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < theWrongs.Length; i++)
        {
            theWrongs[i].onClick.AddListener(WrongAnswer);
        }
        theRadio.onClick.AddListener(RightAnswer);
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
        if (isEnd) return;

        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        isEnd = true;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void EndDragWrong(RectTransform tran)
    {
        if (tran.transform.localPosition != localPositionWrong)
        {
            WrongAnswer();
            tran.transform.localPosition = localPositionWrong;
        }
    }
     
    Vector3 localPositionWrong;

    public void BeginDragWrong(RectTransform tran)
    {
        localPositionWrong = tran.transform.localPosition;
    }
}
