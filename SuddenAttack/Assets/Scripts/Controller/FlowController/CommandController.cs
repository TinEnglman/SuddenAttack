using SuddenAttack.Model;
using SuddenAttack.Model.Commands.Factory;
using SuddenAttack.Model.Units;
using UnityEngine;

namespace SuddenAttack.Controller.FlowController
{
    public class CommandController
    {
        private CommandManager _commandManager;
        private CommandFactory _commandFactory;

        public CommandController(CommandManager commandManager, CommandFactory commandFactory)
        {
            _commandManager = commandManager;
            _commandFactory = commandFactory;
        }

        public void SetMoveCommand(IUnit unit, Vector2 destination)
        {
            IMobileUnit mobileUnit = unit as IMobileUnit;
            if (mobileUnit == null)
            {
                return; 
            }                

            var moveCommand = _commandFactory.CreateMoveCommand(mobileUnit, destination);
            _commandManager.SetCommand(moveCommand);
        }

        public void AddMoveCommand(IUnit unit, Vector2 destination)
        {
            IMobileUnit mobileUnit = unit as IMobileUnit;
            if (mobileUnit == null)
            {
                return;
            }

            var moveCommand = _commandFactory.CreateMoveCommand(mobileUnit, destination);
            _commandManager.PushCommand(moveCommand);
        }

        public void SetAttackTargetCommand(IUnit unit, IUnit attacked)
        {
            var attackTargetCommand = _commandFactory.CreateAttackTargetCommand(unit, attacked, this);
            _commandManager.SetCommand(attackTargetCommand);
        }

        public void AddAttackTargetCommand(IUnit unit, IUnit attacked)
        {
            var attackTargetCommand = _commandFactory.CreateAttackTargetCommand(unit, attacked, this);
            _commandManager.PushCommand(attackTargetCommand);
        }

        public void SetAttackMoveCommand(IUnit unit, Vector2 destination)
        {
            IMobileUnit mobileUnit = unit as IMobileUnit;
            if (mobileUnit == null)
            {
                return;
            }

            var attackMoveCommand = _commandFactory.CreateAttackMoveCommand(mobileUnit, destination);
            _commandManager.SetCommand(attackMoveCommand);
        }

        public void AddAttackMoveCommand(IUnit unit, Vector2 destination)
        {
            IMobileUnit mobileUnit = unit as IMobileUnit;
            if (mobileUnit == null)
            {
                return;
            }

            var attackMoveCommand = _commandFactory.CreateAttackMoveCommand(mobileUnit, destination);
            _commandManager.PushCommand(attackMoveCommand);
        }
    }
}
