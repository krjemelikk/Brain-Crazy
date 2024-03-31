using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_212 : BaseLevel
{
    public Image imgDog1;
    public Image imgDog2;

    public Sprite spDog;

    public GameObject objCoppy;

    private bool isDown;
    private float timeDown = 0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(isDown && !objCoppy.activeSelf)
        {
            timeDown += Time.deltaTime;
            if(timeDown >= 4)
            {
                objCoppy.SetActive(true);
            }
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

    public void OnDogDown()
    {
        isDown = true;
        timeDown = 0f;
    }
    
    public void OnDogUp()
    {
        isDown = false;
    }

    public void OnclickCoppy()
    {
        imgDog2.enabled = true;
        imgDog1.transform.DOLocalMoveX(-150f, 1f);
        imgDog2.transform.DOLocalMoveX(150f, 1f).OnComplete(() =>
        {
            imgDog1.sprite = spDog;
            imgDog2.sprite = spDog;
            RightAnswer();
        });
    }
}
