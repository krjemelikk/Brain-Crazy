using UnityEngine;

public class Level_249_Fire : MonoBehaviour
{
    public Level_249 level;
    private int count = 0;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            count++;
            if (count == 3)
            {
                level.SetView(1);
            }
            else if (count == 4)
            {
                level.SetView(2);
            }
            else if (count == 5)
            {
                level.SetView(3);
            }
            else if (count == 6)
            {
                level.SetView(4);
                level.RightAnswer();
            }
        }
    }
}