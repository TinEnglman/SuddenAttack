using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public abstract class CommandBase : ICommand
    {
        public IUnit Unit { get; set; }
        public abstract void Execute();
    }
}