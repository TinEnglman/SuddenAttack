using UnityEngine;
using SuddenAttack.Gui;

namespace SuddenAttack.Controller.ViewController
{
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
        [SerializeField]
        private GameObject _defaultTarget = null; // revisit; unised by solider

        private SelectionCircleController _selectionCircleController = null;
        private bool _isMoving = false;
        private float _currentHelth = 1;
        private float _maxScale = default;

        public GameObject DefaultTarget
        {
            get { return _defaultTarget;  }
        }

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

        private void Awake()
        { 
            _destination = gameObject.transform.position;
            _maxScale = _heltBarOverlay.transform.localScale.x;
            _selectionCircleController = GetComponentInChildren<SelectionCircleController>(); // create dependency
        }

        private void Update()
        {
            float newScale = _currentHelth * _maxScale;
            _heltBarOverlay.transform.localScale = new Vector3(newScale, _maxScale, _maxScale);
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
}