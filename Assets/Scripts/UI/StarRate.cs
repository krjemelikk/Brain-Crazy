using UnityEngine;

using UnityEngine.EventSystems;

public class StarRate : MonoBehaviour, IPointerEnterHandler {

    [SerializeField] RateBox controller;
    [SerializeField] int IDStar;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter " + IDStar);
        controller.OnClickStar(IDStar);
    }
}
