using UnityEngine;

public class Level_39_Box : MonoBehaviour
{
    private Vector3 posStart;


    public void Start()
    {
        posStart = transform.position;
    }

    public void EndDrag()
    {
        transform.position = posStart;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}