using UnityEngine;

public class Level_98 : BaseLevel
{
    [SerializeField] private float speedTurtle;
    [SerializeField] private GameObject turtleObj;
    [SerializeField] private GameObject finishObj;
    private bool isRight;

    protected override void Start()
    {
        base.Start();
        isRight = false;
    }

    protected override void Update()
    {
        if (isRight)
            return;

        base.Update();

        turtleObj.transform.Translate(Vector3.right * speedTurtle * Time.deltaTime);
        if(Vector2.Distance(turtleObj.transform.position, finishObj.transform.position) <= 0.34f)
        {
            RightAnswer();
        }
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
        isRight = true;
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnClickEveryWhere()
    {
        WrongAnswer();
        GameController.Instance.ResetLevel();
    }
}
