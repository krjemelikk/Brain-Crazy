using UnityEngine;
using UnityEngine.UI;

public class Level_128_Matches : MonoBehaviour
{
    public Level_128 levelCurrent;

    public Image imageMacth;
    public Collider2D collider;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "shield")
        {
            levelCurrent.GetMatches();
        }
    }

    public void ActiveMatches()
    {
        imageMacth.enabled = true;
        collider.enabled = true;
    }
}