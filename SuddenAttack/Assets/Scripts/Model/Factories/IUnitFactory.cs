using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Factories
{
    public interface IUnitFactory
    {
        Unit CreateUnit(float x, float y, GameObject prefab, bool isFriendly);
        string GetDisplayName();
        int GetCost();
    }
}