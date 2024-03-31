using UnityEngine;
using UnityEngine.UI;

public class Level_69 : BaseLevel
{
    [Header("Answers")]
    public Button btMouse;
    public Button btCat;
    public float maxScale = 0.8f;

    private bool isCanRight;

    protected override void Start()
    {
        base.Start();
        btMouse.onClick.AddListener(() => WrongAnswer());
        btCat.onClick.AddListener(() => WrongAnswer());
        StartCoroutine(Helper.StartAction(() =>
        {
            RightAnswer();
        }, () => btMouse.transform.localScale.x >= maxScale));
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
