using UnityEngine;

public class Level_114_Cloud : MonoBehaviour
{
    public Level_114 levelCurrent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Finish")
        {
            levelCurrent.isComplete = true;
            levelCurrent.CheckAnswer();
        }
    }
}
