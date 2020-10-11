using SuddenAttack.Model.Commands.Factory;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SuddenAttack.Model.Behavior
{
    public class AttackMoveBehavior : BehaviorBase
    {
        private float SIGHT_RADIUS = 15; // todo add to unit definition

        private CombatManager _combatManager;
        private MovingBehavior _movingBehavior;
        private AttackingBehavior _attackingBehavior;

        public Vector2 Destination { get; set; }

        public AttackMoveBehavior(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public override void Update(IUnit unit, float dt)
        {
            if (_attackingBehavior.Target == null)
            { 
                var units = GetTargets(unit.Position, SIGHT_RADIUS);

                if (units.Count > 0)
                {
                    IUnit target = units[0];
                    _attackingBehavior.Target = target;
                    _attackingBehavior.OnBegin(unit);
                }
                else
                {
                    
                }
            }
            else
            {
                _movingBehavior.Update(unit, dt);
            }
        }

        public override void OnBegin(IUnit unit)
        {
            _movingBehavior = new MovingBehavior();
            _movingBehavior.Destination = Destination;
            _attackingBehavior = new AttackingBehavior(_combatManager);
            _attackingBehavior.Target = null;

            var units = GetTargets(unit.Position, SIGHT_RADIUS);

            if (units.Count == 0)
            {
                _movingBehavior.OnBegin(unit);
            }
            else
            {
                IUnit target = units[0];
                _attackingBehavior.Target = target;
                _attackingBehavior.OnBegin(unit);
            }
        }

        public override void OnEnd(IUnit unit)
        {
           
        }

        public override bool IsFinished(IUnit unit)
        {
            return false;
        }

        private List<IUnit> GetTargets(Vector2 center, float radius)
        {
            return new List<IUnit>(); // todo implement
        }
    }
}

