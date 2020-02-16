using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controller.ViewController;

namespace SuddenAttack.Model.Units
{
    public class Solider : Unit
    {
        public Solider() : base()
        {
            _unitData = ScriptableObject.CreateInstance<UnitData>(); // externalise

            _unitData.DisplayName = "Solider";
            _unitData.HitPoints = 30;
            _unitData.MaxHitPoints = _unitData.HitPoints;
            _unitData.MoveSpeed = 2f;
            _unitData.WeaponCooldown = 0.67f;
            _unitData.Damage = 4;
            _unitData.Range = 3;
            _unitData.ProjectileSpeed = 15;
            _unitData.EngageRange = 10;
            //_weaponCooldown = _unitData.WeaponCooldown;
        }

        public override void OnUpdate(float dt)
        {
        }

        public override void OnMove(Vector3 destination)
        {
        }

        public override void OnAttack(IUnit other)
        {
            var bulletController = Prefab.GetComponent<BulletContoller>();
            bulletController.Target = other.Prefab;
            bulletController.ProjectileOrigin = Prefab.transform.position + new Vector3(0, 0, 0); //refactor; add projectile exit point
            bulletController.ProjectileSpeed = _unitData.ProjectileSpeed;
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
            //Prefab.GetComponentInChildren<BulletContoller>().Target = null; // revisit
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