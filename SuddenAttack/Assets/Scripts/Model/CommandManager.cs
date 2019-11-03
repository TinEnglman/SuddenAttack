using System.Collections.Generic;
using SuddenAttack.Model.Commands;
using SuddenAttack.Model.Units;


namespace SuddenAttack.Model
{
    public class CommandManager
    {
        private Dictionary<IUnit, List<ICommand>> _activeCommands = new Dictionary<IUnit, List<ICommand>>();

        public void AddCommand(ICommand command)
        {
            if (!_activeCommands.ContainsKey(command.Unit))
            {
                _activeCommands.Add(command.Unit, new List<ICommand>());
            }

            _activeCommands[command.Unit].Add(command);
        }
    }
}