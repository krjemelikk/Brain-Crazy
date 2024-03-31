using UnityEngine;

public class Level_118_Player : MonoBehaviour
{
    public Level_118 level_Current;

    public void OnMouseDown()
    {
        level_Current.CheckRightAnswer();
    }
}
