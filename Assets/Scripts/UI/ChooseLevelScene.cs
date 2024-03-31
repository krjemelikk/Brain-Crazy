using UnityEngine;

public class ChooseLevelScene : MonoBehaviour
{
    public Transform contentScroll;

    private void OnEnable()
    {
        contentScroll.transform.localPosition = new Vector3(contentScroll.transform.localPosition.x,
            915 * ((DataManager.GetHighestLevelUnlocked - 1) / 12),
            contentScroll.transform.localPosition.y);
    }
}
