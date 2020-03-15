using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controller.ViewController;

namespace SuddenAttack.Model.Units
{
    public class Tank : Unit
    {
        public Tank() : base ()
        {
            _unitData = ScriptableObject.CreateInstance<UnitData>(); // refactor; load scriptable

            _unitData.DisplayName = "Tank";
            _unitData.MaxHitPoints = 200;
            _unitData.MoveSpeed = 2;
            //_unitData.WeaponCooldown = 3f;
            //_unitData.Damage = 25;
            //_unitData.Range = 6;
            //_unitData.ProjectileSpeed = 8;
            _unitData.EngageRange = 10;
            //_weaponCooldown = _unitData.WeaponCooldown;
        }

        public override void OnUpdate(float dt)
        {
        }

        public override void OnMove(Vector3 destination)
        {
            var unitController = Prefab.GetComponentInChildren<UnitController>();
            var turretController = Prefab.GetComponentInChildren<TurretController>();
            unitController.DefaultTarget.transform.position = destination;
            turretController.Target = unitController.DefaultTarget;
        }

        public override void OnAttack(IUnit other)
        {
            Prefab.GetComponentInChildren<TurretController>().Target = other.Prefab;

            var bulletController = Prefab.GetComponent<BulletContoller>();
            bulletController.Target = other.Prefab;
            bulletController.ProjectileOrigin = Prefab.transform.position + new Vector3(0, 0, 0); // add top of turret; refactor
            bulletController.ProjectileSpeed = _unitData.PrimaryWeapon.ProjectileSpeed;
            bulletController.Fire();
            bulletController.enabled = true;
        }

        public override void OnFire()
        {
        }

        public override void OnHit(IUnit other)
        {
            var bulletController = Prefab.GetComponent<BulletContoller>();
            bulletController.HitTarget();
        }

        public override void OnStopAttacking()
        {
            Prefab.GetComponentInChildren<TurretController>().Target = Prefab.GetComponentInChildren<UnitController>().DefaultTarget; ;
            //Prefab.GetComponentInChildren<BulletContoller>().Target = null; // revisit; refactor
        }

        public override void OnStop()
        {
        }

        public override void OnDie()
        {
            base.OnDie();
        }
    }
}