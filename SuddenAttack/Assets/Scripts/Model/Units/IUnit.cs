using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Units
{
    public interface IUnit : ICombatUnit
    {
        bool IsMoving { get; }

        void Select();
        void Deselect();
        void Update();
        bool IsBuilding(); // hacky
    }
}