using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : Unit
{
    public Solider()
    {
        _unitData = ScriptableObject.CreateInstance<UnitData>();

        _unitData.HitPoints = 30;
        _unitData.MoveSpeed = 2f;
        _unitData.WeaponCooldown = 0.33f;
        _unitData.Damage = 4;
        _unitData.Range = 3;
        _unitData.ProjectileSpeed = 28;
        _unitData.EngageRange = 10;
        _weaponCooldown = _unitData.WeaponCooldown;
    }

    public override void Attack(IUnit other)
    {
        _isAttacking = true;


        var bulletController = Prefab.GetComponent<BulletContoller>();
        bulletController.Target = other.Prefab;
        bulletController.ProjectileOrigin = Prefab.transform.position + new Vector3(0, 0, 0);
        bulletController.ProjectileSpeed = _unitData.ProjectileSpeed;
        bulletController.Fire();
        bulletController.enabled = true;
    }

    public override void Hit(IUnit other)
    {
        var bulletController = Prefab.GetComponent<BulletContoller>();
        bulletController.HitTarget();
    }

    public override void StopAttacking()
    {
        _isAttacking = false;
        Prefab.GetComponentInChildren<BulletContoller>().Target = null;
    }
}
