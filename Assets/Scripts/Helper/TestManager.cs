using UnityEngine;

public class TestManager : MonoBehaviour
{
    private Vector3 shakeDir;
    [SerializeField] private Vector3 dir;

    public void CheckShakeTrigger()
    {
        shakeDir = Input.acceleration;
        
        if (shakeDir.sqrMagnitude >= 5f)
        {
            Debug.Log(StringHelper.StringColor("Shakeeeeeeeeee", ColorString.red));
            Debug.Log(StringHelper.StringColor(shakeDir.sqrMagnitude.ToString(), ColorString.yellow));
        }
    }

    public void Acceleration()
    {
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;
        
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        
        Debug.Log(dir);
        //thẳng đứng: 0,-1,0
        //cầm đt bình thường: 0,-0.8,-0.6
        //ngược đầu đít 0,1,0
        
        //xoay theo trục Z:
        //ngang sang trái: -1,0,0
        //ngán sang phải 1,0,0
    }
    private void Update()
    {
        //CheckShakeTrigger();
        Acceleration();
    }
}