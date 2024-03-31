using UnityEngine;

public class Level_33_Dinosaurs : MonoBehaviour
{
    public BoxCollider2D boxDinosaurs;
    public Level_33 level;
    public RectTransform rectTransform;
    public Rigidbody2D rb2D;
    public Transform checkGround;
    private bool isJump;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Tree")
        {
            Debug.Log("Treeeeeeeeeee");
            level.CheckWrongAnswer();
        }

        if (other.gameObject.name == "ObjWin")
        {
            level.CheckRightAnswer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            Debug.Log("Treeeeeeeeeee");
            level.CheckWrongAnswer();
        }

        if (collision.gameObject.name == "ObjWin")
        {
            level.CheckRightAnswer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            Debug.Log("Treeeeeeeeeee");
            level.CheckWrongAnswer();
        }

        if (collision.gameObject.name == "ObjWin")
        {
            level.CheckRightAnswer();
        }
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(checkGround.position, 0.05f, 1 << 8) && isJump)
        {
            isJump = false;
        }
    }

    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            Vector2 dir = new Vector2(1f, 1f).normalized;
            rb2D.AddForce(dir * 200f);
        }
    }
}