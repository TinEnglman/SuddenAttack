using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Buildings
{
    public interface IBuilding : IUnit
    {
        GameObject UnitPrefab { get; set; }
        Vector2 SpawnOffset { get; set; }
        //bool IsSpawning { get; set; }

        void SetFactory(IUnitFactory factory);
        IUnitFactory GetFactory();
        //float GetCompletePercent(); // refactor: Create "BuilingManager" or "ProductionManager"
        //int GetIncome();
        //IUnit Update(float dt);
        //IUnit SpawnUnit();
    }
}