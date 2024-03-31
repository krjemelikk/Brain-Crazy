using UnityEngine;

public enum TypeZoom
{
    All = 0,
    OnlyZoomIn = 1,//Thu nhỏ
    OnlyZoomOut = 2//Phóng to
}
public class ZoomObject : MonoBehaviour
{
    public float scaleFactor = 0.5f;

    public float minScale = 0.6f;
    public float maxScale = 2.0f;

    private bool isEnterInObject;

    [SerializeField] private TypeZoom typeZoom;

    [HideInInspector] public bool isCanZoom;
    [HideInInspector] public bool isZooming;

    private void Start()
    {
        isCanZoom = true;
    }

    void Update()
    {
        if (!isCanZoom)
            return;
        if (isEnterInObject && Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;


            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (typeZoom == TypeZoom.OnlyZoomIn)
            {
                if (deltaMagnitudeDiff <= 0)
                    this.transform.localScale = new Vector3(
                        Mathf.Clamp(this.transform.localScale.x - deltaMagnitudeDiff * scaleFactor, minScale, maxScale),
                        Mathf.Clamp(this.transform.localScale.y - deltaMagnitudeDiff * scaleFactor, minScale, maxScale),
                        0
                        );
            }
            else if (typeZoom == TypeZoom.OnlyZoomOut)
            {
                if (deltaMagnitudeDiff > 0)
                    this.transform.localScale = new Vector3(
                        Mathf.Clamp(this.transform.localScale.x - deltaMagnitudeDiff * scaleFactor, minScale, maxScale),
                        Mathf.Clamp(this.transform.localScale.y - deltaMagnitudeDiff * scaleFactor, minScale, maxScale),
                        0
                        );
            }
            else
            {
                this.transform.localScale = new Vector3(
                       Mathf.Clamp(this.transform.localScale.x - deltaMagnitudeDiff * scaleFactor, minScale, maxScale),
                       Mathf.Clamp(this.transform.localScale.y - deltaMagnitudeDiff * scaleFactor, minScale, maxScale),
                       0
                       );
            }
        }
    }


    public void EnterInObject(bool isEnter)
    {
        isEnterInObject = isEnter;
        isZooming = isEnter;
    }
}
