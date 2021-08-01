using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{
    public class AttackingBehavior : BehaviorBase
    {
        public IUnit Target { get; set; }

        private CombatManager _combatManager;        

        public AttackingBehavior(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public override void Update(IUnit unit, float dt) // only creates fire instructions for combat manager
        {
             float distance = (unit.Prefab.transform.position - Target.Prefab.transform.position).magnitude;

            if (distance <= unit.WeaponData.Range && unit.WeaponCooldown <= 0)
            {
                unit.OnFire();
                _combatManager.Damage(unit, Target);
                unit.WeaponCooldown += unit.WeaponData.WeaponCooldown;
            }
            else
            {
                // add pursuit
            }
        }

        public override void OnBegin(IUnit unit)
        {
            unit.OnAttack(Target);
        }

        public override void OnEnd(IUnit unit)
        {
            unit.OnStopAttacking();
        }

        public override bool IsFinished(IUnit unit)
        {
            return Target.HitPoints <= 0;
        }
    }
}

