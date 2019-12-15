using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controller.ViewController;

namespace SuddenAttack.Model.Units
{
    public class Tank : Unit
    {
        public Tank(CombatManager combatManager) : base (combatManager)
        {
            _unitData = ScriptableObject.CreateInstance<UnitData>();

            _unitData.DisplayName = "Tank";
            _unitData.HitPoints = 200;
            _unitData.MaxHitPoints = _unitData.HitPoints;
            _unitData.MoveSpeed = 2;
            _unitData.WeaponCooldown = 3f;
            _unitData.Damage = 25;
            _unitData.Range = 6;
            _unitData.ProjectileSpeed = 8;
            _unitData.EngageRange = 10;
            _weaponCooldown = _unitData.WeaponCooldown;
        }

        public override void Attack(IUnit other)
        {
            base.Attack(other);

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
            base.StopAttacking();
            Prefab.GetComponentInChildren<TurretController>().Target = Prefab;
            Prefab.GetComponentInChildren<BulletContoller>().Target = null;
        }
    }
}