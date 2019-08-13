using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    public Tank()
    {
        _unitData = ScriptableObject.CreateInstance<UnitData>();

        _unitData.HitPoints = 100;
        _unitData.MoveSpeed = 2;
        _unitData.WeaponCooldown = 4f;
        _unitData.Damage = 25;
        _unitData.Range = 8;
        _unitData.ProjectileSpeed = 2;
        _weaponCooldown = _unitData.WeaponCooldown;
    }

    public override void Attack(IUnit other)
    {
        _isAttacking = true;
        Prefab.GetComponentInChildren<TurretController>().Target = other.Prefab;
    }
}
