using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Commands.Factory;
using SuddenAttack.Controller.FlowController;

namespace SuddenAttack.Model.Commands
{
    public class AttackMoveCommand : UnitCommandBase
    {
        private CommandController _commandController;
        private UnitManager _unitManager;

        public AttackMoveCommand(BehaviorManager behaviorManager, CommandController commandController, UnitManager unitManager) : base(behaviorManager)
        {
            _commandController = commandController;
            _unitManager = unitManager;
        }

        public Vector2 Destination { get; set; }

        public override void Execute()
        {
            var attackMoveBehavior = new AttackMoveBehavior(_commandController, _unitManager);
            attackMoveBehavior.Destination = Destination;
            _behaviorManager.SetBehavior(Unit, attackMoveBehavior);
        }
    }
}