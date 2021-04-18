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
        //UnitData UnitData { get; set; } factiry replaces this
        BuildingData BuildingData { get; set; }
        Vector2 SpawnOffset { get; set; }
        //bool IsSpawning { get; set; }

        //List<string> UnitIds { get; set; }
        //void SetFactory(IUnitFactory factory);
        //IUnitFactory GetFactory();
        //float GetCompletePercent(); // refactor: Create "BuilingManager" or "ProductionManager"
        //int GetIncome();
        //IUnit Update(float dt);
        //IUnit SpawnUnit();
    }
}