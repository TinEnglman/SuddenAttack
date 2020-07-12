using UnityEngine;
using SuddenAttack.Gui;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Controller.ViewController
{
    public class UnitController : MonoBehaviour // move moving to behavior
    {

        [SerializeField]
        private float _currentAngle = 0f;
        [SerializeField]
        private Animator _animator = null;
        [SerializeField]
        private GameObject _heltBarOverlay = null;
        [SerializeField]
        private GameObject _defaultTarget = null; // revisit; unised by solider; refactor

        private SelectionCircleController _selectionCircleController = null;
        private bool _isMoving = false;
        private float _currentHelth = 1;
        private float _maxScale = default;

        public IMobileUnit Unit { get; set; }

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

        private void Awake()
        { 
            _maxScale = _heltBarOverlay.transform.localScale.x;
            _selectionCircleController = GetComponentInChildren<SelectionCircleController>(); // create dependency
        }

        private void Update()
        {
            float newScale = _currentHelth * _maxScale;
            _heltBarOverlay.transform.localScale = new Vector3(newScale, _maxScale, _maxScale);
            _animator.SetFloat("Angle", _currentAngle);
            Vector2 currentPosition = Vector2.one * transform.position;

            if (Unit.Position != currentPosition)
            {
                _isMoving = true;
                Vector2 direction = (Unit.Position - currentPosition).normalized;

                if (Mathf.Abs(direction.x) == 0)
                {
                    if (direction == Vector2.up)
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
                    _currentAngle = Vector2.Angle(direction, Vector2.up) * (direction.x / Mathf.Abs(direction.x));
                }

                transform.position = Unit.Position;
            }
            else
            {
                _isMoving = false;
            }
        }
    }
}