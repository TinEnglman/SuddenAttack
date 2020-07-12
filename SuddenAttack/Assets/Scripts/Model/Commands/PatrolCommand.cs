using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Behavior;

namespace SuddenAttack.Model.Commands
{
    public class PatrolCommand : UnitCommandBase
    {
        public Vector2 Destination { get; set; }
        public Vector2 Start { get; set; }

        public PatrolCommand(BehaviorManager behaviorManager) : base(behaviorManager) { }

        public override void Execute() { }
    }
}