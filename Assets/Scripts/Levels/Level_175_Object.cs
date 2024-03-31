using UnityEngine;

public class Level_175_Object : MonoBehaviour
{
    public Level_175 levelCurrent;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            levelCurrent.isDone = true;
            if(!levelCurrent.isFaild)
                levelCurrent.SetGian();
        }

        if (collision.tag == "Objects")
        {
            levelCurrent.isFaild = true;
            levelCurrent.WrongAnswer();
        }
    }
}
