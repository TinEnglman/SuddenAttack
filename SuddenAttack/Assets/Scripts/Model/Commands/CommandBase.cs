using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected BehaviorManager _behaviorManager;

        public CommandBase(BehaviorManager behaviorManager)
        {
            _behaviorManager = behaviorManager;
        }

        public IUnit Unit { get; set; }
        public abstract void Execute();
    }
}