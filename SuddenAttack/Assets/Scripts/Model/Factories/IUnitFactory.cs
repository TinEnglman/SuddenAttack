using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public interface IUnitFactory
    {
        IUnit CreateUnit(float x, float y, bool isFriendly);
        string GetDisplayName();
        int GetCost();
    }
}