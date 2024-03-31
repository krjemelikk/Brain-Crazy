using UnityEngine;
using UnityEngine.UI;

public class Level_210 : BaseLevel
{
    [SerializeField] private DragUI mouse;
    [SerializeField] private GameObject mousePoint;
    [SerializeField] private GameObject playObj;
    [SerializeField] private GameObject pointHandRight;
    [SerializeField] private DragUI handDrag;
    private bool isControlMouse;

    [SerializeField] private Transform posLimitLeftTopTV;
    [SerializeField] private Transform posLimitRightBotTV;

    [SerializeField] private Image tiviImg;
    [SerializeField] private Sprite onTiviSpr;

    private Vector3 offsetFollow;

    private bool isEnd;

    public void OnDragHandRight()
    {
        if (isEnd)
            return;

        if (isControlMouse)
        {
            Vector3 targetCamPos = pointHandRight.transform.position + offsetFollow;
            // Smoothly interpolate between the camera's current position and it's target position.
            mousePoint.transform.position = new Vector3(Mathf.Clamp(targetCamPos.x, posLimitLeftTopTV.position.x, posLimitRightBotTV.position.x),
                Mathf.Clamp(targetCamPos.y, posLimitRightBotTV.position.y, posLimitLeftTopTV.position.y),
                 mousePoint.transform.position.z);

            if (Vector2.Distance(playObj.transform.position, mousePoint.transform.position) <= 0.05f)
            {
                isEnd = true;
                tiviImg.sprite = onTiviSpr;
                playObj.gameObject.SetActive(false);
                RightAnswer();
            }
        }
        else
        {
            if (Vector2.Distance(pointHandRight.transform.position, mouse.transform.position) <= 0.1f)
            {
                isControlMouse = true;
                mouse.transform.parent = handDrag.transform;
                mouse.transform.SetAsFirstSibling();
                mouse.isCanActive = false;

                offsetFollow = mousePoint.transform.position - pointHandRight.transform.position;
            }
        }
    }
}
