using Spine.Unity;
using UnityEngine;

public class Level_101_Player : MonoBehaviour
{
    private Level_101 controller;

    [SerializeField] private float speedJump;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool isJumping;
    private bool isDie;

    public SkeletonGraphic animPlayer;

    public void Init(Level_101 controller)
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

        animPlayer.AnimationState.SetAnimation(0, "animation", false);
        rb.AddForce(Vector2.up * speedJump);
        
        isJumping = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

}
