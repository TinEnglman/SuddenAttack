using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Data;
using UnityEngine;

namespace SuddenAttack.Model.Units
{
    public class Sniper : MobileUnit
    {
        public Sniper(UnitData unitData) : base()
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
            bulletController.ProjectileOrigin = Prefab.transform.position + new Vector3(0, 0, 0); //refactor; add projectile exit point
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