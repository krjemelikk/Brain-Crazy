using UnityEngine;

public class Level_157 : BaseLevel
{
    [SerializeField] private GameObject childObj;
    [SerializeField] private GameObject houseObj;
    private bool isEnd;

    protected override void Update()
    {
        if (isEnd)
            return;
    
        base.Update();

        if(Vector2.Distance(childObj.transform.position, houseObj.transform.position) <= 0.5f)
        {
            childObj.SetActive(false);
            RightAnswer();
            isEnd = true;
        }
    }

    public void WrongPlayer()
    {
        if (isEnd)
            return;

        WrongAnswer();
    }
}
