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

        public bool IsRightMouseButtonDown()
        {
            return Input.GetMouseButtonDown(RightButtonIndex);
        }

        public bool IsLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(LeftButtonIndex);
        }

        public bool IsRightMouseButtonUp()
        {
            return Input.GetMouseButtonUp(RightButtonIndex);
        }

        public bool IsLeftMouseButtonUp()
        {
            return Input.GetMouseButtonUp(LeftButtonIndex);
        }

        public bool IsPressed(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }

        public bool IsHeld(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }

        public bool IsReleased(KeyCode keyCode)
        {
            return Input.GetKeyUp(keyCode);
        }

    }
}