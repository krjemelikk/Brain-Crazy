using UnityEngine;

public class Level_129_Box : MonoBehaviour
{
    public Level_129 levelCurrent;

    private bool isDone;

    public void EndDrag()
    {
        if (isDone) return;
        levelCurrent.WrongAnswer();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            isDone = true;
            levelCurrent.PlayerSmile();
        }
    }
}