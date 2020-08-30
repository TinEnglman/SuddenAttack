using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.FlowController
{
    public interface ICommandController
    {
        void SetMoveCommand(IUnit unit, Vector2 destination);
        void AddMoveCommand(IUnit unit, Vector2 destination);
        void SetAttackTargetCommand(IUnit unit, IUnit attacked);
        void AddAttackTargetCommand(IUnit unit, IUnit attacked);
    }
}
