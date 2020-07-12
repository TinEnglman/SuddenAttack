using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Behavior;

namespace SuddenAttack.Model.Commands.Factory
{
    public class CommandFactory
    {
        private BehaviorManager _behaviorManager;

        public CommandFactory(BehaviorManager behaviorManager)
        {
            _behaviorManager = behaviorManager;
        }

        public ICommand CreateAttackMoveCommand(IUnit attacker, Vector2 destination)
        {
            var command = new AttackMoveCommand(_behaviorManager);
            command.Unit = attacker;
            command.Destination = destination;
            return command;
        }

        public ICommand CreateAttackTargetCommand(IUnit attacker, IUnit attacked)
        {
            var command = new AttackTargetCommand(_behaviorManager);
            command.Unit = attacker;
            command.AttackedUnit = attacked;
            return command;
        }

        public ICommand CreateMoveCommand(IUnit unit, Vector2 destination)
        {
            var command = new MoveCommand(_behaviorManager);
            command.Unit = unit;
            command.Destination = destination;
            return command;
        }

        public ICommand CreatePatrolCommand(IUnit unit, Vector2 start, Vector2 destination)
        {
            var command = new PatrolCommand(_behaviorManager);
            command.Unit = unit;
            command.Destination = destination;
            command.Start = start;
            return command;
        }

        public ICommand CreateStopCommand(IUnit unit, Vector2 destination)
        {
            var command = new StopCommand(_behaviorManager);
            command.Unit = unit;
            return command;
        }

        public ICommand CreateBuildUnitCommand(IUnit building, int unitIndex)
        {
            var command = new BuildUnitCommand(_behaviorManager);
            command.Unit = building;
            command.UnitIndex = unitIndex;
            return command;
        }
    }
}