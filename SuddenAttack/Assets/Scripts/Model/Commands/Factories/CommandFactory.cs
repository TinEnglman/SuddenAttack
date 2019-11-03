using UnityEngine;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands.Factory
{
    public abstract class CommandFactory : ICommandFactory
    {
        public virtual ICommand CreateAttackMoveCommand(IUnit attacker, Vector2 destination)
        {
            var command = new AttackMoveCommand();
            command.Unit = attacker;
            command.Destination = destination;
            return command;
        }

        public virtual ICommand CreateAttackTargetCommand(IUnit attacker, IUnit attacked)
        {
            var command = new AttackTargetCommand();
            command.Unit = attacker;
            command.AttackedUnit = attacked;
            return command;
        }

        public virtual ICommand CreateMoveCommand(IUnit unit, Vector2 destination)
        {
            var command = new MoveCommand();
            command.Unit = unit;
            command.Destination = destination;
            return command;
        }

        public virtual ICommand CreatePatrolCommand(IUnit unit, Vector2 start, Vector2 destination)
        {
            var command = new PatrolCommand();
            command.Unit = unit;
            command.Destination = destination;
            command.Start = start;
            return command;
        }

        public virtual ICommand CreateStopCommand(IUnit unit, Vector2 destination)
        {
            var command = new StopCommand();
            command.Unit = unit;
            return command;
        }

        public virtual ICommand CreateBuildUnitCommand(IUnit building, int unitIndex)
        {
            var command = new BuildUnitCommand();
            command.Unit = building;
            command.UnitIndex = unitIndex;
            return command;
        }
    }
}