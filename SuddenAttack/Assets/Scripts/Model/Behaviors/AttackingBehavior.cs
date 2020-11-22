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
        private float _fireCountdoown;

        public AttackingBehavior(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public override void Update(IUnit unit, float dt) // only creates fire instructions for combat manager
        {
            _fireCountdoown -= dt;
            float distance = (unit.Prefab.transform.position - Target.Prefab.transform.position).magnitude;

            if (distance <= unit.WeaponData.Range && _fireCountdoown <= 0)
            {
                unit.OnAttack(Target);
                _combatManager.Damage(unit, Target);
                _fireCountdoown += unit.WeaponData.WeaponCooldown;
            }
            else
            {
                // add pursuit
            }
        }

        public override void OnBegin(IUnit unit)
        {
            unit.OnAttack(Target);
            _combatManager.Damage(unit, Target);
            _fireCountdoown = unit.WeaponData.WeaponCooldown;
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

