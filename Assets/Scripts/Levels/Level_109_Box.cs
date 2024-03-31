using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_109_Box : MonoBehaviour
{
    public Level_109 level_109;
    private Vector3 posStart;
    private bool isCheckDone;
    public Transform posEnd;
    [SerializeField] private Image mask;
    private bool isDone;

    public void Start()
    {
        posStart = transform.localPosition;
    }

    public void EndDrag()
    {
        if (isDone)
            return;
        if (!isCheckDone)
        {
            level_109.WrongAnswer();
            transform.localPosition = posStart;
        }
        //else
        //{
        //    level_109.LevelDone();
        //    transform.localPosition = posEnd.localPosition;
        //    level_109.mask.DOFillAmount(1f, 1f);
        //    if(mask != null)
        //        mask.DOFillAmount(0f, 1f);
        //    transform.DOLocalRotate(new Vector3(0f,0f,-90f), 1f).OnComplete(() =>
        //    {
        //        transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.5f).
        //        OnComplete(()=> 
        //        {
        //            transform.DOLocalMove(posStart, 0.5f).OnComplete(() =>
        //            {
        //                level_109.RightAnswer();
        //            });
        //        });
                
        //    });
        //}
    }

    public void OnDrag()
    {
        if (isDone)
            return;
        if (isCheckDone)
        { 
            level_109.LevelDone();
            transform.localPosition = posEnd.localPosition;
            level_109.mask.DOFillAmount(1f, 1f);
            if (mask != null)
                mask.DOFillAmount(0f, 1f);
            transform.DOLocalRotate(new Vector3(0f, 0f, -90f), 1f).OnComplete(() =>
            {
                transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.5f).
                OnComplete(() =>
                {
                    transform.DOLocalMove(posStart, 0.5f).OnComplete(() =>
                    {
                        level_109.RightAnswer();
                    });
                });

            });
            isDone = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Finish")
        {
            isCheckDone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Finish")
        {
            isCheckDone = false;
        }
    }
}
