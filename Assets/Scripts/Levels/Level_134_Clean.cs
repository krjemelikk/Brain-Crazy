using UnityEngine;

public class Level_134_Clean : MonoBehaviour
{
    public Level_134 levelCurrent;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            levelCurrent.CheckAnswer();
        }
    }
}
