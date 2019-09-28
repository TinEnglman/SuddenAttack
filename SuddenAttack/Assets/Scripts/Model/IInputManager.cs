using UnityEngine;

public interface IInputManager
{
    Vector2 GetMouseScreenPosition();
    Vector2 GetMouseWorldPosition();
    bool IsDown(KeyCode keyCode, bool ignoreUICheck = false);
    bool IsHeld(KeyCode keyCode, bool ignoreUICheck = false);
    bool IsUp(KeyCode keyCode, bool ignoreUICheck = false);
    bool isRightMouseButtonDown();
    bool isLeftMouseButtonDown();
    bool isRightMouseButtonUp();
    bool isLeftMouseButtonUp();
}
