using UnityEngine;

public class Level_123_Player : MonoBehaviour
{
    public Level_123 level_Current;

    public void OnMouseDown()
    {
        level_Current.CheckRightAnswer();
    }
}
