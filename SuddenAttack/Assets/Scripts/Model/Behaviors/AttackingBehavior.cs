using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{
    public class AttackingBehavior : BehaviorBase
    {
        public IUnit Target { get; set; }

        private float _fireCountdoown;

        public override void Update(IUnit unit, float dt) // only creates fire instructions for combat manager
        {
            _fireCountdoown -= dt;
            float distance = (unit.Prefab.transform.position - Target.Prefab.transform.position).magnitude;

            if (distance <= unit.WeaponData.Range && _fireCountdoown <= 0)
            {
                unit.OnFire();
                _fireCountdoown += unit.WeaponData.WeaponCooldown;
            }
        }

        public override void OnBegin(IUnit unit)
        {
            unit.OnAttack(Target);
            _fireCountdoown = unit.WeaponData.WeaponCooldown;
        }

        public override void OnEnd(IUnit unit)
        {
            unit.OnStopAttacking();
        }

        public override bool IsFinished(IUnit unit)
        {
            return Target.HitPoints > 0;
        }
    }
}

