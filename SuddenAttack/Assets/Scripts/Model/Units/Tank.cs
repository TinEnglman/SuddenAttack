using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controller.ViewController.Units;
using SuddenAttack.Controller.ViewController.Units.Tanks;

namespace SuddenAttack.Model.Units
{
    public class Tank : MobileUnit
    {
        public Tank(UnitData unitData) : base ()
        {
            _unitData = unitData;
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
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
        }

        public override void OnFire()
        {
            Prefab.GetComponent<FireController>().OnFire();

            var bulletController = Prefab.GetComponent<BulletContoller>();
            bulletController.ProjectileOrigin = Prefab.transform.position + new Vector3(0, 0, 0); // add top of turret; refactor
            bulletController.ProjectileSpeed = _unitData.PrimaryWeapon.ProjectileSpeed;
            bulletController.Fire();
            bulletController.enabled = true;
        }

        public override void OnHit(IUnit other)
        {
            var bulletController = Prefab.GetComponent<BulletContoller>();
            bulletController.HitTarget();
        }

        public override void OnStopAttacking()
        {
            Prefab.GetComponentInChildren<TurretController>().Target = Prefab.GetComponentInChildren<UnitController>().DefaultTarget;
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