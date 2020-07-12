
using SuddenAttack.Model.Behavior;

namespace SuddenAttack.Model.Commands
{
    public class BuildingCommandBase : CommandBase
    {
        public BuildingCommandBase(BehaviorManager behaviorManager) : base(behaviorManager) { }
        public override void Execute() { }
    }
}