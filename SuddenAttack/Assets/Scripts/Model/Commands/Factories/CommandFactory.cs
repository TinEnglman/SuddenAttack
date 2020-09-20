using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Behavior;
using SuddenAttack.Controller.FlowController;

namespace SuddenAttack.Model.Commands.Factory
{
    public class CommandFactory
    {
        private BehaviorManager _behaviorManager;
        private CombatManager _combatManager;

        public CommandFactory(BehaviorManager behaviorManager, CombatManager combatManager)
        {
            _behaviorManager = behaviorManager;
            _combatManager = combatManager;
        }

        public ICommand CreateAttackMoveCommand(IUnit attacker, Vector2 destination)
        {
            var command = new AttackMoveCommand(_behaviorManager);
            command.Unit = attacker;
            command.Destination = destination;
            return command;
        }

        public ICommand CreateAttackTargetCommand(IUnit attacker, IUnit attacked, CommandController commandController)
        {
            var command = new AttackTargetCommand(_behaviorManager, _combatManager, commandController);
            command.Unit = attacker;
            command.Target = attacked;

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