using UnityEngine;

public class Level_128_Fire : MonoBehaviour
{
    public Level_128 levelCurrent;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            levelCurrent.GetFire();
        }
    }
}