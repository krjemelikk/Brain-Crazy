using UnityEngine;

public class Level_198 : BaseLevel
{
    [SerializeField] private Transform hair;

    private bool isHair;

    protected override void Start()
    {
        base.Start();
    }

    public void OnEndDragHair()
    {
        hair.transform.parent = this.transform;
        hair.transform.SetAsLastSibling();
        isHair = true;
    }

    public void CheckAnswer()
    {
        if(isHair)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}
