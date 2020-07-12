using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.FlowController
{
    public interface ICommandController
    {
        void SetMoveCommand(IUnit unit, Vector2 destination);
    }
}
