using UnityEngine;

public class Level_83 : BaseLevel
{
    [SerializeField] private Transform circleFind;
    [SerializeField] private Transform diamodObj;
    private bool isWin;
    [SerializeField] private GameObject handObj;
    [SerializeField] private DragUI circleDrag;

    public void OnStartLevel()
    {
        handObj.gameObject.SetActive(false);
    }

    private void CheckAnswer()
    {
        if (isWin)
            return;
        if (Vector2.Distance(diamodObj.transform.position, circleFind.transform.position) <= 0.08f)
        {
            circleDrag.SetActiveDrag(false);
            StartCoroutine(Helper.StartAction(() => { RightAnswer(); }, 0.75f));
            isWin = true;
        }
    }
    
    public void OnDrag()
    {
        if (isWin)
            return;
        OnStartLevel();
        CheckAnswer();
    }
}
