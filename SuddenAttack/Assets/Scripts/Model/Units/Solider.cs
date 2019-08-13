using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : Unit
{
    public Solider()
    {
        _unitData = ScriptableObject.CreateInstance<UnitData>();

        _unitData.HitPoints = 20;
        _unitData.MoveSpeed = 1.5f;
        _unitData.FireSpeed = 0.33f;
        _unitData.Damage = 4;
        _unitData.Range = 4;
        _weaponCooldown = _unitData.FireSpeed;
    }

    public override void Attack(IUnit other)
    {
        _isAttacking = true;
    }
}
