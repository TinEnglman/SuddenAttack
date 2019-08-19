using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    BuildingData Data { get; set; }
    GameObject Prefab { get; set; }
    
    void Update(float dt);
    void SpawnUnit();
}
