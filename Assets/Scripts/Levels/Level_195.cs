using UnityEngine;
using DG.Tweening;

public class Level_195 : BaseLevel
{
    [SerializeField] private DragUI gachDrag;
    [SerializeField] private Transform posEnd;
    private bool isDone;

    private GameObject objectFollow;
    private Vector3 offsetFollow;

    public void OnEndDragGach()
    {
        if (Vector2.Distance(gachDrag.transform.position, posEnd.position) <= 0.2f)
        {
            isDone = true;
            gachDrag.isCanActive = false;
            gachDrag.transform.DOMove(posEnd.position, 0.7f).SetUpdate(true).OnComplete(() => { RightAnswer(); });
        }
    }
}
