using SuddenAttack.Model.Units;
using UnityEngine;

namespace SuddenAttack.Model.Commands.Factory
{ 
    public interface ICommandFactory
    {
        ICommand CreateAttackMoveCommand(IUnit attacker, Vector2 destination);
        ICommand CreateAttackTargetCommand(IUnit attacker, IUnit attacked);
        ICommand CreateMoveCommand(IUnit unit, Vector2 destination);
        ICommand CreatePatrolCommand(IUnit unit, Vector2 start, Vector2 destination);
        ICommand CreateStopCommand(IUnit unit, Vector2 destination);
        ICommand CreateBuildUnitCommand(IUnit building, int unitIndex);
    }
}
