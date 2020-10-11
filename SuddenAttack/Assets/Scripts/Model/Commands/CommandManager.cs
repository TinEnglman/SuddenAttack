using System.Collections.Generic;
using SuddenAttack.Model.Commands;
using SuddenAttack.Model.Units;


namespace SuddenAttack.Model
{
    public class CommandManager
    {
        private Dictionary<IUnit, Queue<ICommand>> _queuedCommands = new Dictionary<IUnit, Queue<ICommand>>();
        private Dictionary<IUnit, ICommand> _activeCommand = new Dictionary<IUnit, ICommand>();
        private List<ICommand> _commandKillList = new List<ICommand>();

        public void PushCommand(ICommand command)
        {
            if (!_queuedCommands.ContainsKey(command.Unit))
            {
                _queuedCommands.Add(command.Unit, new Queue<ICommand>());
            }

            _queuedCommands[command.Unit].Enqueue(command);
        }

        public void SetCommand(ICommand command)
        {
            InternalSetCommand(command);
        }

        public void PopQueuedCommand(IUnit unit)
        {
            if (!_queuedCommands.ContainsKey(unit) || _queuedCommands[unit].Count == 0)
            {
                return;
            }

            SetCommand(_queuedCommands[unit].Dequeue());
        }


        public void Update()
        {
            foreach (var commmandListtPair in _activeCommand)
            {
                var unit = commmandListtPair.Key;
                var command = commmandListtPair.Value;
                _commandKillList.Add(command);
            }

            foreach(var command in _commandKillList)
            {
                command.Execute();
                _activeCommand.Remove(command.Unit);
            }

            if (_commandKillList.Count > 0)
            {
                _commandKillList.Clear();
            }
        }

        private void InternalSetCommand(ICommand command)
        {
            if (_activeCommand.ContainsKey(command.Unit))
            {

                _activeCommand[command.Unit] = command;
            }
            else
            {
                _activeCommand.Add(command.Unit, command);
            }
        }
    }
}