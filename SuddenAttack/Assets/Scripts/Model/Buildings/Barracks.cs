using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : AbstractBuilding
{
    public Barracks(bool isFriendly)
    {
        Data = ScriptableObject.CreateInstance<BuildingData>();
        Data.BuildCooldown = 10;
        Data.HitPoints = 800;
        Data.IsFriendly = isFriendly;
        _currentCountdown = Data.BuildCooldown;

    }

    public override IUnit SpawnUnit()
    {
        float x = Prefab.transform.position.x + _spawnOffset.x + Random.value;
        float y = Prefab.transform.position.y + _spawnOffset.y + Random.value;
        IUnit unit = _unitFactory.CreateUnit(x, y, _unitPrefab, Data.IsFriendly);
        return unit;
    }
}
