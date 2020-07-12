using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Behavior;

namespace SuddenAttack.Model.Commands
{
    public class AttackMoveCommand : UnitCommandBase
    {
        public AttackMoveCommand(BehaviorManager behaviorManager) : base(behaviorManager) { }
        public Vector2 Destination { get; set; }
        public override void Execute() { }
    }
}