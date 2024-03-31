using UnityEngine;
using UnityEngine.UI;

public class Level_127 : BaseLevel
{
    private int numClick;
    [SerializeField] private Text numTxt;

    protected override void Start()
    {
        base.Start();
        numTxt.text = "0";
    }

    public void Click_Add_1()
    {
        numClick += 1;
        numTxt.text = numClick.ToString();
        if (numClick >= 50000)
            RightAnswer();
    }

    public void Click_Add_50000()
    {
        numClick = 50000;
        numTxt.text = numClick.ToString();

        RightAnswer();
    }
}
