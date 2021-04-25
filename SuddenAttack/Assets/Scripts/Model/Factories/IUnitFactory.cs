using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public interface IUnitFactory
    {
        IMobileUnit CreateUnit(float x, float y, int teamIndex, Transform parentTransform);
        string GetDisplayName();
        int GetCost();
    }
}