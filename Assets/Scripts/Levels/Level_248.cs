using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_248 : BaseLevel
{
    [SerializeField] private Transform posCheckText;
    [SerializeField] private Transform water;
    [SerializeField] private Image chauObj;
    [SerializeField] private Sprite sprtChauDone;
    [SerializeField] private DragUI TxtDrag;

    private bool isEnd;

    protected override void Update()
    {
        if (isEnd)
            return;

        if(Vector2.Distance(posCheckText.gameObject.transform.position, txtQuestion.transform.position) <= 0.5f)
        {
            TxtDrag.isCanActive = false;
            txtQuestion.transform.DOMove(posCheckText.gameObject.transform.position, 0.3f).OnComplete(() =>
            {
                txtQuestion.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -52), 0.5f);
                StartCoroutine(Helper.StartAction(() => { water.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 40), 0.5f).OnComplete(()=> 
                {
                    chauObj.sprite = sprtChauDone;
                    RightAnswer();

                }); }, 0.3f));
            });

            isEnd = true;
        }
    }
}
