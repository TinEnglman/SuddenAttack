using System.Collections.Generic;
using SuddenAttack.Model.Commands;
using SuddenAttack.Model.Units;


namespace SuddenAttack.Model
{
    public class CommandManager
    {
        private Dictionary<IUnit, List<ICommand>> _activeCommands = new Dictionary<IUnit, List<ICommand>>();

        public void AddCommand(IUnit unit, ICommand command)
        {
            if (!_activeCommands.ContainsKey(unit))
            {
                _activeCommands.Add(unit, new List<ICommand>());
            }

            _activeCommands[unit].Add(command);
        }
    }
}