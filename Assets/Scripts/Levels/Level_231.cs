using UnityEngine;

public class Level_231 : BaseLevel
{
    [SerializeField] private Level_231_R rObj;
    private bool isEnd;

    protected override void Update()
    {
       

        //if(Vector2.Distance(Input.mousePosition, rObj.transform.position) <= 2f)
        //{
           
        //}
    }

    public void ClickR()
    {
        if (isEnd)
            return;
        isEnd = true;
        rObj.Die();
        RightAnswer();
    }
}
