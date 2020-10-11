using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Commands.Factory;

namespace SuddenAttack.Model.Commands
{
    public class AttackMoveCommand : UnitCommandBase
    {
        private CombatManager _combatManager;

        public AttackMoveCommand(BehaviorManager behaviorManager, CombatManager combatManager) : base(behaviorManager)
        {
            _combatManager = combatManager;
        }

        public Vector2 Destination { get; set; }

        public override void Execute()
        {
            var attackMoveBehavior = new AttackMoveBehavior(_combatManager);
            attackMoveBehavior.Destination = Destination;
            _behaviorManager.SetBehavior(Unit, attackMoveBehavior);
        }
    }
}