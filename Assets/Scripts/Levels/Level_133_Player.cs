using UnityEngine;

public class Level_133_Player : MonoBehaviour
{
    public Level_133 levelCurrent;
    private bool isEnd;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnd) return;
        if(collision.tag == "Finish")
        {
            isEnd = true;
            levelCurrent.CheckAnswer();
        }
    }
}
