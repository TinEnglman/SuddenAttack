using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;

namespace SuddenAttack.Model
{
    public class InputHandler : MonoBehaviour, IInputManager
    {
        private Camera _mainCamera;
        private const int LeftButtonIndex = 0;
        private const int RightButtonIndex = 1;

        public class DoubleClick
        {
            public string currentAction;
            public GameObject currentTarget;
            public float startTime;

            public DoubleClick() { }

            public DoubleClick(GameObject currentTarget,
                float startTime)
            {
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

        public Vector2 GetMouseScreenPosition()
        {
            return Input.mousePosition;
        }

        public Vector2 GetMouseWorldPosition()
        {
            return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public bool isRightMouseButtonDown()
        {
            return Input.GetMouseButtonDown(RightButtonIndex);
        }

        public bool isLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(LeftButtonIndex);
        }

        public bool isRightMouseButtonUp()
        {
            return Input.GetMouseButtonUp(RightButtonIndex);
        }

        public bool isLeftMouseButtonUp()
        {
            return Input.GetMouseButtonUp(LeftButtonIndex);
        }

        public bool IsDown(KeyCode keyCode, bool ignoreUICheck = false)
        {
            if (!ValidatePointerKeyCode(keyCode, ignoreUICheck))
            {
                return false;
            }

            return Input.GetKeyDown(keyCode);
        }

        public bool IsHeld(KeyCode keyCode, bool ignoreUICheck = false)
        {
            if (!ValidatePointerKeyCode(keyCode, ignoreUICheck))
            {
                return false;
            }

            return Input.GetKey(keyCode);
        }

        public bool IsUp(KeyCode keyCode, bool ignoreUICheck = false)
        {
            if (!ValidatePointerKeyCode(keyCode, ignoreUICheck))
            {
                return false;
            }

            return Input.GetKeyUp(keyCode);
        }

        private void SetDoubleClickValues(DoubleClick doubleClick)
        {
            _doubleClick.currentAction = doubleClick.currentAction;
            _doubleClick.currentTarget = doubleClick.currentTarget;
            _doubleClick.startTime = doubleClick.startTime;
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
}