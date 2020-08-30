using SuddenAttack.Model;
using SuddenAttack.Model.Commands;
using SuddenAttack.Model.Commands.Factory;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.FlowController
{
    public class LocalPlayerCommandController : ICommandController
    {
        private CommandManager _commandManager;
        private CommandFactory _commandFactory;

        public LocalPlayerCommandController(CommandManager commandManager, CommandFactory commandFactory)
        {
            _commandManager = commandManager;
            _commandFactory = commandFactory;
        }

        public void SetMoveCommand(IUnit unit, Vector2 destination)
        {
            var moveCommand = _commandFactory.CreateMoveCommand(unit, destination);
            _commandManager.SetCommand(moveCommand);
        }

        public void AddMoveCommand(IUnit unit, Vector2 destination)
        {
            var moveCommand = _commandFactory.CreateMoveCommand(unit, destination);
            _commandManager.PushCommand(moveCommand);
        }

        public void SetAttackTargetCommand(IUnit unit, IUnit attacked)
        {
            var attackTargetCommand = _commandFactory.CreateAttackTargetCommand(unit, attacked);
            _commandManager.SetCommand(attackTargetCommand);
        }

        public void AddAttackTargetCommand(IUnit unit, IUnit attacked)
        {
            var attackTargetCommand = _commandFactory.CreateAttackTargetCommand(unit, attacked);
            _commandManager.PushCommand(attackTargetCommand);
        }

    }
}
