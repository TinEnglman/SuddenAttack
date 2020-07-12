using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{ 
    public interface IBehavior
    {
        void Update(IUnit unit, float dt);
        void OnBegin(IUnit unit);
        void OnEnd(IUnit unit);
        bool IsFinished(IUnit unit);
    }
}
