using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Level_102_Egg : MonoBehaviour
{
    public Level_102 levelCurrent;
    public Image iconEgg;
    public Sprite eggBroken;
    protected IDisposable _completeCountDown;
    public Rigidbody2D rb;

    private bool isFinish;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isFinish)
            return;

        if (other.tag == "Respawn")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            iconEgg.sprite = eggBroken;
            levelCurrent.FailQuest();

            _completeCountDown = Observable.Timer(TimeSpan.FromSeconds(0.15f))
           .Subscribe(_ =>
           {
               levelCurrent.RemoveEgg(gameObject);
               Destroy(gameObject);
           })
           .AddTo(this);
        }
        else if(other.tag == "Finish")
        {
            levelCurrent.RemoveEgg(gameObject);
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            isFinish = true;
            //rb.isKinematic = false;
            transform.parent = other.transform;
            //Destroy(gameObject);
        }
    }

    public void OnDisable()
    {
        if (_completeCountDown != null)
            _completeCountDown.Dispose();
    }
}
