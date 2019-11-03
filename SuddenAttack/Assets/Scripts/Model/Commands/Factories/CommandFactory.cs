using UnityEngine;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands.Factory
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateAttackMoveCommand(IUnit attacker, Vector2 destination)
        {
            var command = new AttackMoveCommand();
            command.Unit = attacker;
            command.Destination = destination;
            return command;
        }

        public ICommand CreateAttackTargetCommand(IUnit attacker, IUnit attacked)
        {
            var command = new AttackTargetCommand();
            command.Unit = attacker;
            command.AttackedUnit = attacked;
            return command;
        }

        public ICommand CreateMoveCommand(IUnit unit, Vector2 destination)
        {
            var command = new MoveCommand();
            command.Unit = unit;
            command.Destination = destination;
            return command;
        }

        public ICommand CreatePatrolCommand(IUnit unit, Vector2 start, Vector2 destination)
        {
            var command = new PatrolCommand();
            command.Unit = unit;
            command.Destination = destination;
            command.Start = start;
            return command;
        }

        public ICommand CreateStopCommand(IUnit unit, Vector2 destination)
        {
            var command = new StopCommand();
            command.Unit = unit;
            return command;
        }

        public ICommand CreateBuildUnitCommand(IUnit building, int unitIndex)
        {
            var command = new BuildUnitCommand();
            command.Unit = building;
            command.UnitIndex = unitIndex;
            return command;
        }
    }
}