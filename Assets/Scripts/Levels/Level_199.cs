using UnityEngine;

public class Level_199 : BaseLevel
{
    [SerializeField] private Transform[] posBreaks;
    [SerializeField] private DragUI[] breaks;

    private int numDone;

    protected override void Start()
    {
        base.Start();
    }

    public void OnEndDragBreak(int idDrag)
    {
        if (!breaks[idDrag].isCanActive)
            return;
       // Debug.Log("CCCCCCCCCCCCCCCCCCCCCCCC");
        if(Vector2.Distance(breaks[idDrag].gameObject.transform.position, posBreaks[idDrag].gameObject.transform.position) <= 0.2f)
        {
            breaks[idDrag].gameObject.transform.position = posBreaks[idDrag].gameObject.transform.position;
            breaks[idDrag].isCanActive = false;
            //breaks[idDrag]
            numDone++;
            if (numDone >= 8)
            {
                StartCoroutine(Helper.StartAction(() =>
                {
                    RightAnswer();
                }, 0.5f));
            }
                
        }
    }
}
