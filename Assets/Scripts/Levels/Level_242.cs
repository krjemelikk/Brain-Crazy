using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_242 : BaseLevel
{
    private int id_Current = -1;
    private int index = -1;
    public List<Level_242_Button> lsAva = new List<Level_242_Button>();

    private bool isFaild;

    private bool isStart;

    private int countDone = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!isStart) return;
        if (isFaild) return;
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

    public void Onclick(int _index,int _id)
    {
        isStart = true;
        if (isFaild) return;
        if (id_Current == -1)
        {
            id_Current = _id;
            index = _index;
            lsAva[_index].GetComponent<Image>().color = new Color(1f, 0.85f, 0f);
        }
        else
        {
            if(id_Current == _id)
            {
                lsAva[index].gameObject.SetActive(false);
                lsAva[_index].gameObject.SetActive(false);
                countDone++;
                id_Current = -1;
                if (countDone >= 5)
                    RightAnswer();
            }
            else
            {
                WrongAnswer();
            }
        }
    }
}
