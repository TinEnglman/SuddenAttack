using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Units;
using UnityEngine;

namespace SuddenAttack.Model.Commands
{
    public class MoveCommand : UnitCommandBase
    {
        public Vector2 Destination { get; set; }

        public MoveCommand(BehaviorManager behaviorManager) : base(behaviorManager) { }

        public override void Execute()
        {
            var movingBehavior = new MovingBehavior();
            movingBehavior.Destination = Destination;
            _behaviorManager.SetBehavior(Unit, movingBehavior);
        }
    }
}