using UnityEngine;

public class DisableMe : MonoBehaviour
{
    float timer;
    public float timeLife = 1;

    private void OnEnable()
    {
        timer = 0;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeLife)
        {
            timer = 0;
            this.gameObject.SetActive(false);
        }
    }
}
