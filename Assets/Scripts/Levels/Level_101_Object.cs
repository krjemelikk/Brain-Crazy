using UnityEngine;

public class Level_101_Object : MonoBehaviour
{
    private Level_101 controller;

    [SerializeField] private float speedMove;

    private Vector3 posCheckDone;

    public void Init(Level_101 controller)
    {
        this.controller = controller;

        posCheckDone = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (controller != null && !controller.isPlayerDie)
        {
            this.transform.Translate(Vector2.left * speedMove * Time.deltaTime);
            if(this.transform.position.x < posCheckDone.x - 3)
            {
                controller.CheckDoneLevel();
                this.gameObject.SetActive(false);
            }
        }
    }
}
