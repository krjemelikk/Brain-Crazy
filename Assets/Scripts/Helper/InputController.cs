using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public InputType _inputType;
    public float speed;
    public static InputController Instance;
    public static bool ShakeCamOnMove = true;
    public bool CanControlPermission = true;
    [SerializeField] private GameObject moveTarget;
    private float _xPos;
    private float _yPos;
    private bool _IsTouch;
    private Vector2 _TempVector2 = Vector2.zero;
    private Vector3 _vecPos;
    private float _delayTime;
    private float _LastXpos;
    private float _LastYpos;
    private bool onShakeCam = false;
    private bool _canControl = true;

    public static UnityAction<bool> GetInputAction;
    private Vector3 touchPosition
    {
        get
        {
            //if (Input.touchCount > 1)
               // return Input.GetTouch(0).position;
            return Input.mousePosition;
        }
    }


    private void Awake()
    {
        CanControlPermission = true;
        Instance = this;
        Reload();
    }
    //public static bool ShakeCamOnDam = true;
    private void Start()
    {
    }

    private void Update()
    {
        //if (!CanControlPermission)
        //    return;
        if (_canControl)
            TouchMove();
        if (!onShakeCam && ShakeCamOnMove)
            Camera.main.transform.position = new Vector3(moveTarget.transform.position.x * .05f, 0, -10);
    }


    public void ShakeCamOnDam(float time = 1, float streng = 1, bool disableControl = false)
    {
        onShakeCam = true;
        if (disableControl)
            _canControl = false;
        Camera.main.transform.DOKill();
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.DOShakePosition(time, streng).OnComplete(OnDoneShakeCam);
    }

    private void OnDoneShakeCam()
    {
        onShakeCam = false;
        _canControl = true;
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    private void TouchMove()
    {
        _xPos = 0f;
        _yPos = 0f;

        //if (EventSystem.current.currentSelectedGameObject != null)
        //{
        //    return;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            _IsTouch = true;
            _vecPos = Camera.main.ScreenToWorldPoint(touchPosition);
            _xPos = _vecPos.x;
            _yPos = _vecPos.y;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!_IsTouch) _IsTouch = true;
            _vecPos = Camera.main.ScreenToWorldPoint(touchPosition);
            _xPos = _vecPos.x;
            _yPos = _vecPos.y;

            if (CanControlPermission && _IsTouch)
                switch (_inputType)
                {
                    case InputType.Point:
                        {
                            _TempVector2.x = (_xPos - _LastXpos) * 1.3f + moveTarget.transform.position.x;
                            _TempVector2.y = (_yPos - _LastYpos) * 1.3f + moveTarget.transform.position.y;
                            //controlActor.MoveQuickTo(_TempVector2);
                            MoveQuickTo(moveTarget.transform, _TempVector2);
                        }
                        break;
                    case InputType.Follow:
                        {
                            _TempVector2.x = _xPos;
                            _TempVector2.y = _yPos + 0.45f;
                            MoveTo(moveTarget.transform, _TempVector2);
                            //controlActor.MoveTo(_TempVector2);
                        }
                        break;
                }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _IsTouch = false;
        }
        _LastXpos = _xPos;
        _LastYpos = _yPos;
        if (GetInputAction != null) GetInputAction(_IsTouch);
    }

    public void Reload()
    {
        _IsTouch = false;
    }

    public Vector3 _min, _max;
    public void MoveTo(Transform tf, Vector3 position)
    {
        var _pos = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        _pos.x = Mathf.Clamp(_pos.x, _min.x, _max.x);
        _pos.y = Mathf.Clamp(_pos.y, _min.y, _max.y);
        tf.position = _pos;
    }

    public void MoveQuickTo(Transform tf, Vector3 position)
    {
        var _pos = position;
        _pos.x = Mathf.Clamp(_pos.x, _min.x, _max.x);
        _pos.y = Mathf.Clamp(_pos.y, _min.y, _max.y);
        tf.position = _pos;
    }
}

public enum InputType
{
    Follow,
    Point
}
