using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public class StopCommand : CommandBase
    {
        public StopCommand(BehaviorManager behaviorManager) : base(behaviorManager) { }

        public override void Execute()
        {
            Unit.OnStop();
        }
    }
}