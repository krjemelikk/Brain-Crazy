using UnityEngine.UI;
using DG.Tweening;

public class Level_188 : BaseLevel
{
    public Image[] imgHint = new Image[8];
    private int currentRight;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < imgHint.Length; i++)
        {
            imgHint[i].GetComponent<Level_188_Player>().ID = i;
        }
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
        for (int i = 0; i < imgHint.Length; i++)
        {
            imgHint[i].DOKill();
            imgHint[i].DOFillAmount(1f, 0.7f);
        }
        
    }

    public void CheckRightAnswer(int id)
    {
        currentRight++;
        
        int id_2 = 1;

        if(id == 0)
        {
            id_2 = 1;
        }
        else if(id == 1)
        {
            id_2 = 0;
        }
        else if (id == 2)
        {
            id_2 = 3;
        }
        else if (id == 3)
        {
            id_2 = 2;
        }
        else if (id == 4)
        {
            id_2 = 5;
        }
        else if (id == 5)
        {
            id_2 = 4;
        }
        else if (id == 6)
        {
            id_2 = 7;
        }
        else if (id == 7)
        {
            id_2 = 6;
        }

        imgHint[id].DOKill();
        imgHint[id].fillAmount = 0;
        imgHint[id].DOFillAmount(1f, 0.7f).OnComplete(() =>
        {
            if(currentRight >= 4)
            RightAnswer();
        });

        imgHint[id_2].DOKill();
        imgHint[id_2].fillAmount = 0;
        imgHint[id_2].DOFillAmount(1f, 0.7f).OnComplete(() =>
        {
            
        });

        imgHint[id].gameObject.GetComponent<Button>().enabled = false;
        imgHint[id_2].gameObject.GetComponent<Button>().enabled = false;
    }
}
