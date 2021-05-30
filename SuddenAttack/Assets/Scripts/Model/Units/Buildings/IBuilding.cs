using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Buildings
{
    public interface IBuilding : IUnit
    {
        BuildingData BuildingData { get; set; }
        Vector2 SpawnOffset { get; set; }
    }
}