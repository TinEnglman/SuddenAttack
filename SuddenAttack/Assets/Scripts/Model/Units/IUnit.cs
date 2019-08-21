using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit : ICombatUnit
{
    bool IsMoving { get; }

    void Select();
    void Deselect();
    void Update();
}
