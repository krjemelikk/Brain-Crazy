using UnityEngine;
using DG.Tweening;

public class CollisionWater : MonoBehaviour
{
    [SerializeField] private Level_41 controller;
    [SerializeField] private int currentChoice;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentChoice != controller.currentChoice)
            return;

        if (this.gameObject.transform.localScale.x >= other.gameObject.transform.localScale.x)
        {
            other.transform.DOKill();
            other.gameObject.SetActive(false);

            Vector3 scale = this.gameObject.transform.localScale + Vector3.one * 0.1f;
            this.transform.DOKill();
            this.transform.DOScale(scale, 0.25f).SetEase(Ease.InBack);
        }
        else
        {
            Vector3 scale = other.gameObject.transform.localScale + Vector3.one * 0.1f;
            other.transform.DOKill();
            other.transform.DOScale(scale, 0.25f).SetEase(Ease.InBack);

            this.transform.DOKill();
            this.gameObject.SetActive(false);

        }

        controller.WaterUnify();
    }
}
