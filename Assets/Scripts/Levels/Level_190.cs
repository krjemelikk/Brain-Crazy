using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_190 : BaseLevel
{
    [SerializeField] private Button lipBtn;
    [SerializeField] private Button FrogBtn;
    [SerializeField] private Sprite PrinceSpr;

    private bool isDone;

    protected override void Start()
    {
        base.Start();

        FrogBtn.onClick.RemoveAllListeners();
        FrogBtn.onClick.AddListener(OnClickDogDone);
    }

    public void OnDragLip()
    {
        if (isDone)
            return;

        if (Vector2.Distance(lipBtn.gameObject.transform.position, FrogBtn.gameObject.transform.position) <= 0.2f)
        {
            
            lipBtn.GetComponent<DragUI>().isCanActive = false;
            lipBtn.transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() => {
                lipBtn.gameObject.SetActive(false);
                FrogBtn.GetComponent<Image>().sprite = PrinceSpr;
                FrogBtn.GetComponent<Image>().SetNativeSize();
            });
            isDone = true;
        }
    }

    public void OnClickDogDone()
    {
        if (isDone)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}
