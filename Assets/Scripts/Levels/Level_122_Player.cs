using UnityEngine;

public class Level_122_Player : MonoBehaviour
{
    public Level_122 level_Current;

    public void OnMouseDown()
    {
        level_Current.CheckRightAnswer();
    }
}
