using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controller.ViewController.Units;

namespace SuddenAttack.Model.Units
{
    public class Solider : MobileUnit
    {
        public Solider(UnitData unitData) : base()
        {
            _unitData = unitData;
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
        }

        public override void OnMove(Vector3 destination)
        {
        }

        public override void OnAttack(IUnit other)
        {
            var bulletController = Prefab.GetComponent<BulletContoller>();
            bulletController.Target = other.Prefab;
            bulletController.enabled = true;
        }

        public override void OnFire()
        {
            var bulletController = Prefab.GetComponent<BulletContoller>();

            bulletController.ProjectileOrigin = Prefab.transform.position + new Vector3(0, 0, 0); //refactor; add projectile exit point
            bulletController.ProjectileSpeed = _unitData.PrimaryWeapon.ProjectileSpeed;
            bulletController.Fire();
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