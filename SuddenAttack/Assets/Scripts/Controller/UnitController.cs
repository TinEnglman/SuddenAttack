using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField]
    private float _currentAngle = 0f;
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private Vector3 _destination;
    [SerializeField]
    private float _moveSpeed = 1.0f;
    [SerializeField]
    private GameObject _heltBarOverlay = null;

    private SelectionCircleController _selectionCircleController = null;
    private bool _isMoving = false;
    private float _currentHelth = 1;

    public bool IsMoving
    {
        get { return _isMoving; }
    }

    public float CurrentHelth
    {
        set { _currentHelth = value; }
    }

    public void Select()
    {
        _selectionCircleController.Selectct();
    }

    public void Deselect()
    {
        _selectionCircleController.Deselectct();
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    void Awake()
    {
        _destination = GetComponent<Transform>().position;
        _selectionCircleController = GetComponentInChildren<SelectionCircleController>(); // create dependency
    }

    void Update()
    {

        var originalScale = _heltBarOverlay.transform.localScale;

        float maxScale = 2;
        float newScale = _currentHelth * maxScale;
        _heltBarOverlay.transform.localScale = new Vector3(newScale, maxScale, maxScale);
        _animator.SetFloat("Angle", _currentAngle);

        if (_destination != transform.position)
        {
            _isMoving = true;
            Vector3 direction = (_destination - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
            if (Mathf.Abs(direction.x) == 0)
            { 
                if (direction == Vector3.up)
                {
                    _currentAngle = 0.0f;
                }
                else
                {
                    _currentAngle = 180.0f;
                }
            }
            else
            { 
                _currentAngle = Vector3.Angle(direction, Vector3.up) * (direction.x / Mathf.Abs(direction.x));
            }

            if ((_destination - transform.position).sqrMagnitude < 0.01f)
            {
                transform.position = _destination;
            }
        }
        else
        {
            _isMoving = false;
        }
    }
}
