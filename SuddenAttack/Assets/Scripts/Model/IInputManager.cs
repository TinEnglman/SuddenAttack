using UnityEngine;
namespace SuddenAttack.Model
{
    public interface IInputManager
    {
        Vector2 GetMouseScreenPosition();
        Vector2 GetMouseWorldPosition();
        bool IsPressed(KeyCode keyCod);
        bool IsHeld(KeyCode keyCode);
        bool IsReleased(KeyCode keyCode);
        bool IsRightMouseButtonDown();
        bool IsLeftMouseButtonDown();
        bool IsRightMouseButtonUp();
        bool IsLeftMouseButtonUp();
    }
}