using UnityEngine;

public interface IInputManager
{
    Vector3 PointerWorldPosition { get; }
    bool IsDown(string action, bool ignoreUICheck = false);
    bool IsDown(KeyCode keyCode, bool ignoreUICheck = false);
    bool IsHeld(string action, bool ignoreUICheck = false);
    bool IsHeld(KeyCode keyCode, bool ignoreUICheck = false);
    bool IsUp(string action, bool ignoreUICheck = false);
    bool IsUp(KeyCode keyCode, bool ignoreUICheck = false);
    bool IsDouble(string action, out RaycastHit hit, LayerMask layerMask, bool ignoreUICheck = false);
    float GetAxisRaw(string actionName);
    float GetAxis(string actionName);
    bool IsRaycasted(LayerMask layerMask);
    bool GetRaycastHit(out RaycastHit hit, LayerMask layermask);
}
