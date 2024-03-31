using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspect : MonoBehaviour
{
    public float defaultAspect = 720.0f / 1280.0f;

    
    void Awake()
    {
        var _camera = GetComponent<Camera>();
        var _currentAspect = Screen.width / (float) Screen.height;
        if (_currentAspect < defaultAspect)
        {
            _camera.orthographicSize *= defaultAspect / _currentAspect;
        }
    }
    
}
