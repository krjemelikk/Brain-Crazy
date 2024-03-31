using UnityEngine;
using UnityEngine.UI;

public class Level_209 : BaseLevel
{
    [SerializeField] private float minScaleLight;
    [SerializeField] private float maxScaleLight;

    [SerializeField] private DragUI lightObj;
    [SerializeField] private Transform pubObj;

    [SerializeField] private Image congTacImg;
    [SerializeField] private Sprite congTacOnSpr;

    private bool isLight;

    public void OnDragLight()
    {
        CheckAnswres();
    }

    public void CheckAnswres()
    {
        if (isLight)
            return;

        var lightObjChild = lightObj.transform.GetChild(0).gameObject;
        if (lightObjChild.transform.localScale.x <= maxScaleLight && lightObjChild.transform.localScale.x >= minScaleLight)
        {
            if(Vector2.Distance(lightObj.transform.position, pubObj.transform.position) <= 0.3f)
            {
                lightObj.transform.position = pubObj.transform.position;
                lightObj.isCanActive = false;
                lightObjChild.GetComponent<ZoomObject>().enabled = false;
                isLight = true;
            }
        }
    }

    public override void WrongAnswer()
    {
        base.WrongAnswer();
    }

    public void CheckOnLight()
    {
        if(isLight)
        {
            congTacImg.sprite = congTacOnSpr;
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}
