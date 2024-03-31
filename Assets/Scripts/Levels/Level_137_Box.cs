using UnityEngine;

public class Level_137_Box : MonoBehaviour
{
    public Level_137 levelCurrent;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            levelCurrent.CheckRightAnswer();
        }
    }
}