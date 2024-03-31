using UnityEngine;

public class Level_143_Player : MonoBehaviour
{
    public Level_143 level_Current;

    public void OnMouseDown()
    {
        level_Current.CheckRightAnswer();
    }
}
