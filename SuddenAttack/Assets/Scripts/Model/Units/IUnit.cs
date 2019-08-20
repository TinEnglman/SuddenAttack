using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    UnitData Data{get; set;}
    GameObject Prefab{get; set;}
    bool IsMoving { get; }
    bool IsUserLocked { get; set; }

    void Move(Vector3 destination);
    void Stop();
    void StopAttacking();
    void Select();
    void Deselect();
    void Attack(IUnit other);
    bool CanFire();
    void Fire();
    void Hit(IUnit other);
    void Damage(IUnit other, float damage, float delay);
    void Update();
}
