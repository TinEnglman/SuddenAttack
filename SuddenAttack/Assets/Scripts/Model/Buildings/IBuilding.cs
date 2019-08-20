using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    BuildingData Data { get; set; }
    GameObject Prefab { get; set; }
    GameObject UnitPrefab { get; set; }

    void SetFactory(IUnitFactory factory);
    IUnit Update(float dt);
    IUnit SpawnUnit();
}
