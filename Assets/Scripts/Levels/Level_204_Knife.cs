using UnityEngine;

public class Level_204_Knife : MonoBehaviour
{
    public float speed;
    public bool isMoveKinfe;

    private Level_204 controller;

    public void Init(Level_204 controller)
    {
        isMoveKinfe = false;
        this.controller = controller;
    }

    public void MoveKnife()
    {
        isMoveKinfe = true;
    }

    private void Update()
    {
        if (isMoveKinfe)
        {
            this.transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        //if (controller.isEnd)
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wood")
            return;

        isMoveKinfe = false;

        if (controller.isEnd)
            return;

        controller.WrongAnswer();
        GameController.Instance.ResetLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller.isEnd)
            return;
        isMoveKinfe = false;
        if (collision.gameObject.tag == "Wood")
        {
            this.transform.parent = controller.wood;
            this.transform.SetAsFirstSibling();
            controller.AddScore();
        }
    }
}
