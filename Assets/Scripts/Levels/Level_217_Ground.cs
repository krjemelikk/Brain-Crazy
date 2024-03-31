using UnityEngine;
using DG.Tweening;

public class Level_217_Ground : MonoBehaviour
{
    public RectTransform contentDog;
    public DragUI dragUI;
    [HideInInspector] public CanvasGroup canvasGroup;

    private float startScale;

    private void Start()
    {
        startScale = this.transform.localScale.x;
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    // public RectTransform rectTransform;
    public void Scale()
    {
        this.transform.localScale = startScale * Vector3.one;
        this.transform.DOKill();
        this.transform.DOScale(startScale * Vector3.one + 0.2f * Vector3.one, 0.2f).OnComplete(
            ()=> 
            {
                this.transform.DOScale(startScale * Vector3.one, 0.2f);
            });
    }
}
