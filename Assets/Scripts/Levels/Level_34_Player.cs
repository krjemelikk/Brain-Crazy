using UnityEngine;

public class Level_34_Player : MonoBehaviour
{
    public Level_34 level34;
    public Rigidbody2D rb2D;
    public float speed;
    
    public void MoveUp()
    {
        rb2D.velocity = (Vector3.up * speed);
    }
    
    public void MoveDown()
    {
        rb2D.velocity = (Vector3.down * speed);
    }
    
    public void MoveLeft()
    {
        rb2D.velocity = (Vector3.left * speed);
    }
    
    public void MoveRight()
    {
        rb2D.velocity = (Vector3.right * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Win")
        {
            level34.CheckAnswer();
        }
    }

    public void StopMove()
    {
        rb2D.velocity = Vector3.zero;
    }
}