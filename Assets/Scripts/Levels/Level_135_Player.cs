using UnityEngine;

public class Level_135_Player : MonoBehaviour
{
    private Level_135 controller;

    [SerializeField] private float speedJump;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool isJumping;
    private bool isDie;

    public void Init(Level_135 controller)
    {
        this.controller = controller;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie)
            return;

        if (collision.tag == "Objects")
        {
            Die();
        }

        if(collision.tag == "Finish")
        {
            collision.gameObject.SetActive(false);
            controller.CheckDoneLevel();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    private void Die()
    {
        controller.WrongAnswer();
        isDie = true;
        controller.PlayerDie();
        rb.gravityScale = 0;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    public void Jump()
    {
        if (isJumping)
            return;

        rb.AddForce(Vector2.up * speedJump);

        isJumping = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            controller.BtnLeft();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            controller.BtnNotMoveLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            controller.BtnRight();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            controller.BtnNotMoveRight();
    }

}
