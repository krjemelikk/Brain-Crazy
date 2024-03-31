using UnityEngine;
using UnityEngine.UI;

public class Level_153 : BaseLevel
{
    public Image[] imgSounds = new Image[2];

    private bool isDone;

    protected override void Start()
    {
        base.Start();
        AndroidNativeVolumeService.SetSystemVolume(100f);
    }

    protected override void Update()
    {
        base.Update();
        foreach (Image imgSound in imgSounds)
        {
            float m_Volume = AndroidNativeVolumeService.GetSystemVolume();
            imgSound.transform.localScale = new Vector3(m_Volume, m_Volume, m_Volume);
        }

        if (AndroidNativeVolumeService.GetSystemVolume() <= 0.5f && !isDone)
        {
            RightAnswer();
            isDone = true;
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
}
