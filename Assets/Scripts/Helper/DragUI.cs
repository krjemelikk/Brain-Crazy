using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public enum TypeDrag
{
    All = 0,
    X_axis = 1,
    Y_axis = 2,
    Limit = 3,
    All_delta = 4,
    Delay = 5,
    ComeBackFirstPos = 6,
        All_delta_X = 7,
    All_delta_Y = 8,

}
public class DragUI : MonoBehaviour
{
    public TypeDrag typeDrag;

    private Canvas parentCanvasOfImageToMove;
    private Vector2 pos;
    private Button button;

    private Vector2 posStart;

    private float timer;

    [ShowIf("IsLimit")] [SerializeField] private Transform limit_TopRight;
    [ShowIf("IsLimit")] [SerializeField] private Transform limit_BotLeft;

    [HideInInspector] public bool isCanActive;

    private Vector3 dir;

    [HideInInspector]
    public bool isDraging;

    private bool IsLimit()
    {
        return typeDrag == TypeDrag.Limit;
    }

    private void Awake()
    {
        isCanActive = true;
    }

    public void SetActiveDrag(bool isActive)
    {
        isCanActive = isActive;
        Image img = this.GetComponent<Image>();
        if (img)
            img.raycastTarget = false;
    }
    
    public void SetActiveDragNew(bool isActive)
    {
        isCanActive = isActive;
        Image img = this.GetComponent<Image>();
        if (img)
            img.raycastTarget = isActive;
    }

    public void OnBeginDrag()
    {
        timer = 0;

        posStart = this.transform.position;
    }

    public void OnDrag()
    {
        if (!isCanActive)
            return;

        if (parentCanvasOfImageToMove == null) return;
        //Debug.Log("Dragggggg");
        if (button != null) button.interactable = false;
        isDraging = true;
        switch (typeDrag)
        {
            case TypeDrag.ComeBackFirstPos:
            case TypeDrag.All:
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.mousePosition, parentCanvasOfImageToMove.worldCamera, out pos);
                transform.position = parentCanvasOfImageToMove.transform.TransformPoint(pos);
                break;
            case TypeDrag.X_axis:
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.mousePosition, parentCanvasOfImageToMove.worldCamera, out pos);
                transform.position = new Vector3(parentCanvasOfImageToMove.transform.TransformPoint(pos).x, transform.position.y, transform.position.z);
                break;
            case TypeDrag.Y_axis:
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.mousePosition, parentCanvasOfImageToMove.worldCamera, out pos);
                transform.position = new Vector3(transform.position.x, parentCanvasOfImageToMove.transform.TransformPoint(pos).y, transform.position.z);
                break;
            case TypeDrag.Limit:
                Vector3 _min = new Vector3(limit_BotLeft.position.x, limit_BotLeft.position.y);
                Vector3 _max = new Vector3(limit_TopRight.position.x, limit_TopRight.position.y);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.mousePosition, parentCanvasOfImageToMove.worldCamera, out pos);
                //pos.x = Mathf.Clamp(pos.x, _min.x, _max.x);
                //pos.y = Mathf.Clamp(pos.y, _min.y, _max.y);
                Vector3 a = parentCanvasOfImageToMove.transform.TransformPoint(pos);
                transform.position = new Vector3(Mathf.Clamp(a.x, _min.x, _max.x), Mathf.Clamp(a.y, _min.y, _max.y));
                break;
            case TypeDrag.All_delta:
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.mousePosition, parentCanvasOfImageToMove.worldCamera, out pos);
                Vector3 posMouse = parentCanvasOfImageToMove.worldCamera.ScreenToWorldPoint(Input.mousePosition);
                posMouse.z = 0f;
                Vector3 posMove = posMouse + dir;
                posMove.z = 0f;
                transform.position = posMove;
                break;
            case TypeDrag.All_delta_X:
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.mousePosition, parentCanvasOfImageToMove.worldCamera, out pos);
                Vector3 x_posMouse = parentCanvasOfImageToMove.worldCamera.ScreenToWorldPoint(Input.mousePosition);
                x_posMouse.z = 0f;
                Vector3 x_posMove = x_posMouse + dir;
                x_posMove.z = 0f;
                transform.position = new Vector3(x_posMove.x, transform.position.y, transform.position.z);
                break;
            case TypeDrag.All_delta_Y:
                Vector3 y_posMouse = parentCanvasOfImageToMove.worldCamera.ScreenToWorldPoint(Input.mousePosition);
                y_posMouse.z = 0f;
                Vector3 y_posMove = y_posMouse + dir;
                y_posMove.z = 0f;
                transform.position = new Vector3(y_posMove.x, transform.position.y, transform.position.z);
                break;

            case TypeDrag.Delay:
                if (button != null)
                    button.interactable = true;
                timer += Time.deltaTime;
                if (timer >= 0.1f)
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.mousePosition, parentCanvasOfImageToMove.worldCamera, out pos);
                    transform.position = parentCanvasOfImageToMove.transform.TransformPoint(pos);
                    //timer = 0;
                }
                break;
            
        }
    }


    public void EndDrag()
    {
        if (!isCanActive)
            return;
        Debug.Log(StringHelper.StringColor("EndDrag", ColorString.yellow));
        if (button != null) button.interactable = true;
        isDraging = false;
        switch (typeDrag)
        {
            case TypeDrag.ComeBackFirstPos:
                this.transform.position = posStart;
                break;
        }
    }

    public void OnDrop()
    {
        if (!isCanActive)
            return;
        Debug.Log(StringHelper.StringColor("OnDrop", ColorString.yellow));
        if (button != null) button.interactable = true;
        Vector3 posMouse = parentCanvasOfImageToMove.worldCamera.ScreenToWorldPoint(Input.mousePosition);
        posMouse.z = 0;
        dir = transform.position - posMouse;
        dir.z = 0;
        if (gameObject.name == "Scale")
            Debug.Log(dir);
    }

    private void Start()
    {
        if (parentCanvasOfImageToMove == null) parentCanvasOfImageToMove = FindObjectOfType<Canvas>();
        if (button == null) button = GetComponent<Button>();
    }
}