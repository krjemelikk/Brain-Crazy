using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IPointerUpHandler, IEndDragHandler
{

    private Vector2 pointerOffset;
    private RectTransform canvasRectTransform;
    private RectTransform panelRectTransform;
    public UnityEvent OnButtonDown;
    void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.transform as RectTransform;

            panelRectTransform = transform.parent as RectTransform;
        }
    }

    public bool check;
    public void OnPointerDown(PointerEventData data)
    {
        if (this.tag != "circlemini")
        {
            panelRectTransform.SetAsLastSibling();
        }
        else
        {
            Debug.Log("draggggggggggg");

        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
    }


    public void OnDrag(PointerEventData data)
    {
        if (panelRectTransform == null)
            return;

        Vector2 pointerPostion = ClampToWindow(data);

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, pointerPostion, data.pressEventCamera, out localPointerPosition
        ))
        {
            panelRectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }

    public bool isDragging = false;
    public void OnEndDrag(PointerEventData data)
    {
        isDragging = false;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        isDragging = true;

        //GameManager.instance.btnButton[0].GetComponent<Button>().enabled = false;
        //GameManager.instance.btnButton[1].GetComponent<Button>().enabled = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging == false)
        {
            Debug.Log(StringHelper.StringColor("OnPointerUp isDragging == false", ColorString.yellow));
        }

    }

    private Vector2 ClampToWindow(PointerEventData data)
    {
        Vector2 rawPointerPosition = data.position;

        Vector3[] canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);

        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
        return newPointerPosition;
    }
}