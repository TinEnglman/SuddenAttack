using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding : IUnit
{
    GameObject UnitPrefab { get; set; }

    void SetFactory(IUnitFactory factory);
    IUnit Update(float dt);
    IUnit SpawnUnit();
}
