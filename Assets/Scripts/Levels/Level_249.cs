using UnityEngine;
using UnityEngine.UI;

public class Level_249 : BaseLevel
{
    public Image viewFire;

    public Sprite spFire1;
    public Sprite spFire2;
    public Sprite spFire3;

    public void SetView(int viewIndex)
    {
        if(viewIndex == 1)
        {
            viewFire.color = Color.white;
        }else if (viewIndex == 2)
        {
            viewFire.sprite = spFire1;
            viewFire.SetNativeSize();
        }
        else if (viewIndex == 3)
        {
            viewFire.sprite = spFire2;
            viewFire.SetNativeSize();

        }
        else if (viewIndex == 4)
        {
            viewFire.sprite = spFire3;
            viewFire.SetNativeSize();

        }
    }

    protected override void Start()
    {
        base.Start();
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
}