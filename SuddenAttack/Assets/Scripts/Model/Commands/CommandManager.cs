using System.Collections.Generic;
using SuddenAttack.Model.Commands;
using SuddenAttack.Model.Units;


namespace SuddenAttack.Model
{
    public class CommandManager
    {
        private Dictionary<IUnit, List<ICommand>> _queuedCommands = new Dictionary<IUnit, List<ICommand>>(); // replace with queue
        private Dictionary<IUnit, ICommand> _activeCommand = new Dictionary<IUnit, ICommand>();
        private List<ICommand> _commandKillList = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            if (!_queuedCommands.ContainsKey(command.Unit))
            {
                _queuedCommands.Add(command.Unit, new List<ICommand>());
            }

            _queuedCommands[command.Unit].Add(command);
        }

        public void SetCommand(ICommand command)
        {
            if (_activeCommand.ContainsKey(command.Unit))
            {
                if (_activeCommand[command.Unit] != command)
                {
                    InternalSetCommand(command);
                }
            }
            else
            {
                _activeCommand.Add(command.Unit, command);
            }
        }

        public ICommand PopQueuedCommand(IUnit unit)
        {
            return _queuedCommands[unit][0];
        }


        public void Update()
        {
            foreach (var commmandListtPair in _activeCommand)
            {
                var unit = commmandListtPair.Key;
                var command = commmandListtPair.Value;
                command.Execute();
                _commandKillList.Add(command);
            }

            foreach(var command in _commandKillList)
            {
                _activeCommand.Remove(command.Unit);
            }
        }

        private void InternalSetCommand(ICommand command)
        {
            _activeCommand[command.Unit] = command;
        }
    }
}