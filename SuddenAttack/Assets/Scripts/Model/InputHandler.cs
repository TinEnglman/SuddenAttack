using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;


public class InputHandler : MonoBehaviour, IInputManager
{
    [SerializeField] private LayerMask _layerMask = default;
    [SerializeField] private float _doubleClickDelay = 0.25f;

    public Vector3 PointerWorldPosition { get; private set; }

    private Camera _mainCamera;
    private bool _isDownWithIgnoringUI;
    private bool _isHeldWithIgnoringUI;
    private RaycastHit _hit;

    public class DoubleClick
    {
        public string currentAction;
        public GameObject currentTarget;
        public float startTime;

        public DoubleClick() { }

        public DoubleClick(string currentAction, GameObject currentTarget,
            float startTime)
        {
            this.currentAction = currentAction;
            this.currentTarget = currentTarget;
            this.startTime = startTime;
        }
    }
    private DoubleClick _doubleClick = new DoubleClick();

    [Inject]
    public void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    void Update()
    {
        if (IsDown("Fire1", true))
        {
            _isDownWithIgnoringUI = true;
        }

        if (IsHeld("Fire1", true))
        {
            _isHeldWithIgnoringUI = true;
        }

        if (IsUp("Fire1", true))
        {
            _isHeldWithIgnoringUI = false;
        }
    }

    void FixedUpdate()
    {
        GetPointerWorldPosition();
    }

    public bool IsDown(string action, bool ignoreUICheck = false)
    {
        if (!ValidatePointerAction(action, ignoreUICheck))
        {
            return false;
        }

        return Input.GetButtonDown(action);
    }

    public bool IsDown(KeyCode keyCode, bool ignoreUICheck = false)
    {
        if (!ValidatePointerKeyCode(keyCode, ignoreUICheck))
        {
            return false;
        }

        return UnityEngine.Input.GetKeyDown(keyCode);
    }

    public bool IsHeld(string action, bool ignoreUICheck = false)
    {
        if (!ValidatePointerAction(action, ignoreUICheck))
        {
            return false;
        }

        return UnityEngine.Input.GetButton(action);
    }

    public bool IsHeld(KeyCode keyCode, bool ignoreUICheck = false)
    {
        if (!ValidatePointerKeyCode(keyCode, ignoreUICheck))
        {
            return false;
        }

        return UnityEngine.Input.GetKey(keyCode);
    }

    public bool IsUp(string action, bool ignoreUICheck = false)
    {
        if (!ValidatePointerAction(action, ignoreUICheck))
        {
            return false;
        }

        return UnityEngine.Input.GetButtonUp(action);
    }

    public bool IsUp(KeyCode keyCode, bool ignoreUICheck = false)
    {
        if (!ValidatePointerKeyCode(keyCode, ignoreUICheck))
        {
            return false;
        }

        return UnityEngine.Input.GetKeyUp(keyCode);
    }

    private void SetDoubleClickValues(DoubleClick doubleClick)
    {
        _doubleClick.currentAction = doubleClick.currentAction;
        _doubleClick.currentTarget = doubleClick.currentTarget;
        _doubleClick.startTime = doubleClick.startTime;
    }

    public bool IsDouble(string action, out RaycastHit hit, LayerMask layerMask, bool ignoreUICheck = false)
    {
        hit = default;

        if (_doubleClick.currentAction != null &&
            Time.time - _doubleClick.startTime > _doubleClickDelay)
        {
            SetDoubleClickValues(new DoubleClick());
            return false;
        }

        if (IsUp(action, ignoreUICheck))
        {
            if (_doubleClick.currentAction != null &&
                _doubleClick.currentAction != action)
            {
                SetDoubleClickValues(new DoubleClick());
                return false;
            }

            if (_doubleClick.currentTarget != null &&
                GetRaycastHit(out hit, layerMask) &&
                _doubleClick.currentTarget != hit.transform.gameObject)
            {
                SetDoubleClickValues(new DoubleClick());
                return false;
            }

            if (_doubleClick.currentAction == action &&
                GetRaycastHit(out hit, layerMask) &&
                _doubleClick.currentTarget == hit.transform.gameObject &&
                Time.time - _doubleClick.startTime <= _doubleClickDelay)
            {
                SetDoubleClickValues(new DoubleClick());
                return true;
            }

            if (_doubleClick.currentAction == null &&
                GetRaycastHit(out hit, layerMask))
            {
                SetDoubleClickValues(new DoubleClick(action, hit.transform.gameObject, Time.time));
            }
        }

        return false;
    }

    public float GetAxisRaw(string action)
    {
        return Input.GetAxisRaw(action);
    }

    public float GetAxis(string action)
    {
        return Input.GetAxis(action);
    }

    private void GetPointerWorldPosition()
    {
        if (_isDownWithIgnoringUI)
        {
            _isDownWithIgnoringUI = false;
            if (GetRaycastHit(out _hit, _layerMask))
            {
                PointerWorldPosition = _hit.point;
            }
        }

        if (_isHeldWithIgnoringUI)
        {
            if (GetRaycastHit(out _hit, _layerMask))
            {
                PointerWorldPosition = _hit.point;
            }
        }
    }

    public bool IsRaycasted(LayerMask layerMask)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, 1000, layerMask);
    }

    public bool GetRaycastHit(out RaycastHit hitInfo, LayerMask layerMask)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo, 1000, layerMask);
    }

    private bool ValidatePointerAction(string action, bool ignoreUICheck)
    {
        if (ignoreUICheck)
        {
            return true;
        }

        if (action == "Fire1")
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return false;
            }
        }
        return true;
    }

    private bool ValidatePointerKeyCode(KeyCode keyCode, bool ignoreUICheck)
    {
        if (ignoreUICheck)
        {
            return true;
        }

        if (keyCode == KeyCode.Mouse0)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return false;
            }
        }
        return true;
    }
}
