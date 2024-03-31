using UnityEngine;

public class Level_231_R : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoints;
    private int currentCheckPoint;
    [SerializeField] private float speed;
    private bool isDie;

    private void Update()
    {
        if (isDie)
            return;

        Move();
    }

    public void Move()
    {

        this.transform.position = Vector2.MoveTowards(this.transform.position, checkPoints[currentCheckPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(this.transform.position, checkPoints[currentCheckPoint].position) <= 0.2f)
        {
            currentCheckPoint += 1;
            if (currentCheckPoint >= checkPoints.Length)
            {
                currentCheckPoint = 0;
            }
        }
    }

    public void Die()
    {
        isDie = true;
    }
}
