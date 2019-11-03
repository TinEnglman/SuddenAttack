using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public interface ICommand
    {
        IUnit Unit { get; set; }
        void Execute();
    }
}