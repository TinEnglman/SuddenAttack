using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding : IUnit
{
    GameObject UnitPrefab { get; set; }
    bool IsSpawning { get; set; }

    void SetFactory(IUnitFactory factory);
    IUnitFactory GetFactory();
    float GetCompletePercent();
    int GetIncome();
    IUnit Update(float dt);
    IUnit SpawnUnit();
}
