using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuartes : AbstractBuilding
{
    public HeadQuartes(bool isFriendly)
    {
        Data = ScriptableObject.CreateInstance<BuildingData>();
        Data.BuildCooldown = 30;
        Data.HitPoints = 1000;
        Data.IsFriendly = isFriendly;
        _currentCountdown = Data.BuildCooldown;

    }

    public override IUnit SpawnUnit()
    {
        float x = Prefab.transform.position.x + _spawnOffset.x;
        float y = Prefab.transform.position.y + _spawnOffset.y;
        IUnit unit =_unitFactory.CreateUnit(x, y, _unitPrefab, Data.IsFriendly);
        return unit;
        
    }
}
