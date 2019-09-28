using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controllers;

namespace SuddenAttack.Model.Units
{
    public class Tank : Unit
    {
        public Tank()
        {
            _unitData = ScriptableObject.CreateInstance<UnitData>();

            _unitData.DisplayName = "Tank";
            _unitData.HitPoints = 100;
            _unitData.MaxHitPoints = _unitData.HitPoints;
            _unitData.MoveSpeed = 2;
            _unitData.WeaponCooldown = 2f;
            _unitData.Damage = 10;
            _unitData.Range = 6;
            _unitData.ProjectileSpeed = 16;
            _unitData.EngageRange = 10;
            _weaponCooldown = _unitData.WeaponCooldown;
        }

        public override void Attack(IUnit other)
        {
            _isAttacking = true;
            Prefab.GetComponentInChildren<TurretController>().Target = other.Prefab;

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
            Prefab.GetComponentInChildren<TurretController>().Target = Prefab;
            Prefab.GetComponentInChildren<BulletContoller>().Target = null;
        }
    }
}