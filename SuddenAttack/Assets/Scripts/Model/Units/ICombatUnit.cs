using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatUnit
{
    UnitData Data { get; set; }
    bool IsUserLocked { get; set; }
    GameObject Prefab { get; set; }

    void StopAttacking();
    void Attack(IUnit other);
    bool CanFire();
    void Fire();
    void Hit(IUnit other);
    void Damage(IUnit other, float damage, float delay);
    void Move(Vector3 destination);
    void Stop();
    void Die();
}
