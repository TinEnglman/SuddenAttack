using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    UnitData Data{get; set;}

    void Move(Vector3 destination);
    void Select();
    void Deselect();
}
