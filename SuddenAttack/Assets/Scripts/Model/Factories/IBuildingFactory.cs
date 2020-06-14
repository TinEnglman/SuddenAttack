using SuddenAttack.Model.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public interface IBuildingFactory
    {
        IBuilding CreateBuilding(float x, float y, bool isFriendly);
        string GetDisplayName();
        int GetCost();
    }
}