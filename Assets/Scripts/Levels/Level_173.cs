using UnityEngine;
using UnityEngine.UI;

public class Level_173 : BaseLevel
{
    private bool _hold1;
    private bool _hold2;

    private int countClick = 0;

    private bool isEnd;

    public Image imgBoom;
    public Image imgRed;
    public Image imgGreen;

    public Sprite spBoom;
    public Sprite spRed;
    public Sprite spGreen;

    protected override void Start()
    {
        base.Start();
        countClick = 0;
        isEnd = _hold1 = _hold2  = false;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.A))
            OnPointDown(1);
        if (Input.GetKeyDown(KeyCode.B))
            OnPointDown(2);

        if (Input.GetKeyUp(KeyCode.A))
            OnPointUp(1);
        if (Input.GetKeyUp(KeyCode.B))
            OnPointUp(2);
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
        StartCoroutine(Helper.StartAction(() =>
        {
            GameController.Instance.ResetLevel();
        }, 1f));
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void CheckAnswer()
    {
        if (isEnd)
            return;
        if (countClick >= 2)
        {
            imgRed.sprite = spRed;
            imgGreen.sprite = spGreen;
            StartCoroutine(Helper.StartAction(() => { RightAnswer(); }, 1));
           
            isEnd = true;
        }
        else
        {
            WrongAnswer();
        }
    }

    public void OnPointDown(int indexHold)
    {
        if (indexHold == 1 && !_hold1)
        {
            _hold1 = true;
            countClick++;
        }
        if (indexHold == 2 && !_hold2)
        {
            _hold2 = true;
            countClick++;
        }
    }

    public void OnPointUp(int indexHold)
    {
        CheckAnswer();
        if (isEnd) return;
        if (indexHold == 1 && _hold1)
        {
            _hold1 = false;
            countClick--;
            if (countClick < 0)
                countClick = 0;
            imgRed.sprite = spRed;
            imgBoom.sprite = spBoom;
        }
        if (indexHold == 2 && _hold2)
        {
            _hold2 = false;
            countClick--;
            if (countClick < 0)
                countClick = 0;
            imgGreen.sprite = spGreen;
            imgBoom.sprite = spBoom;
        }
    } 
}
