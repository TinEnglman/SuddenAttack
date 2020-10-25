using System.Collections.Generic;
using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Commands;
using SuddenAttack.Model.Units;


namespace SuddenAttack.Model
{
    public class CommandManager
    {
        private Dictionary<IUnit, LinkedList<ICommand>> _queuedCommands = new Dictionary<IUnit, LinkedList<ICommand>>();
        private Dictionary<IUnit, ICommand> _activeCommand = new Dictionary<IUnit, ICommand>();
        private List<ICommand> _commandKillList = new List<ICommand>();

        public void PushCommand(ICommand command)
        {
            if (!_queuedCommands.ContainsKey(command.Unit))
            {
                _queuedCommands.Add(command.Unit, new LinkedList<ICommand>());
            }

            _queuedCommands[command.Unit].AddFirst(command);
        }

        public void InjectCommand(ICommand command)
        {
            if (!_queuedCommands.ContainsKey(command.Unit))
            {
                _queuedCommands.Add(command.Unit, new LinkedList<ICommand>());
            }

            _queuedCommands[command.Unit].AddLast(command);
        }

        public void SetCommand(ICommand command)
        {
            InternalSetCommand(command);
            _queuedCommands.Clear();
        }

        public void PopQueuedCommand(IUnit unit)
        {
            if (!_queuedCommands.ContainsKey(unit) || _queuedCommands[unit].Count == 0)
            {
                return;
            }

            InternalSetCommand(_queuedCommands[unit].Last.Value);

            if (_queuedCommands[unit].Count > 0)
            { 
                _queuedCommands[unit].RemoveLast();
            }
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