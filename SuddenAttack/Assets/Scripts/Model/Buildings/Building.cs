using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : IBuilding
{
    protected BuildingData _buildingData;
    protected GameObject _prefab;

    float _currentCountdown = 0;

    public BuildingData Data
    {
        get { return _buildingData; }
        set { _buildingData = value; }
    }

    public GameObject Prefab
    {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public void Update(float dt)
    {
        if (_currentCountdown <= 0)
        {
            _currentCountdown = Data.BuildCooldown;
        }

        _currentCountdown -= dt;

    }

    public abstract void SpwanUnit();
}
