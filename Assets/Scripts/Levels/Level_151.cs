using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_151 : BaseLevel
{
    public Transform[] clouds = new Transform[3];
    public Transform sun;
    public Image rain;

    public Image player;
    public Sprite spSmilePlayer;

    private bool isSun;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (CheckSun() && !isSun)
        {
            rain.DOFillAmount(0, 1f).OnComplete(()=> 
            {
                player.sprite = spSmilePlayer;
                RightAnswer();
            });
            isSun = true;
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
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public bool CheckSun()
    {
        foreach (Transform cloud in clouds)
        {
            if (Vector3.Distance(cloud.position, sun.position) < 1.5f)
                return false;
        }
        return true;
    }
}
