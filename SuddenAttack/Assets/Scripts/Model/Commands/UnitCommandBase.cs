
using SuddenAttack.Model.Behavior;

namespace SuddenAttack.Model.Commands
{
    public class UnitCommandBase : CommandBase
    {
        public UnitCommandBase(BehaviorManager behaviorManager) : base(behaviorManager) { }
        public override void Execute() { }
    }
}