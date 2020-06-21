using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{ 
    public interface IBehavior
    {
        void OnBegin();
        void OnEnd();
        bool IsFinished();
    }
}
