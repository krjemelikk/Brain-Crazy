using UnityEngine;
using DG.Tweening;

public class Level_133 : BaseLevel
{
    private Vector3 dir;
    public Transform ball;
    public Transform posStart;
    public Transform posEnd;

    private bool isEnd;

    public Rigidbody2D rbBall;
    Vector2 dirCurrent = Vector2.zero;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isEnd) return;
        base.Update();

        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;
        if (dir.x >= 0.2f)
            dirCurrent.x = 1f;
        else if (dir.x <= -0.2f)
            dirCurrent.x = -1f;
        else
            dirCurrent.x = 0f;

        if (dir.y >= -0.4f)
            dirCurrent.y = 1f;
        else if (dir.y <= -0.7f)
            dirCurrent.y = -1f;
        else
            dirCurrent.y = 0f;

        rbBall.velocity = dirCurrent * 20f * Time.deltaTime;
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

    public void CheckAnswer()
    {
        isEnd = true;
        ball.position = posStart.position;
        ball.DOMove(posEnd.position, 1f).OnComplete(() =>
        {
            RightAnswer();
        });
    }
}
