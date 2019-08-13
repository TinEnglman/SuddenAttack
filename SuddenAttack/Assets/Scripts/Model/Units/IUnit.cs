using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    UnitData Data{get; set;}
    GameObject Prefab{get; set;}

    void Move(Vector3 destination);
    void Select();
    void Deselect();
    void Attack(IUnit other);
    bool CanFire();
    void Fire(IUnit other);
    void Update();
}
