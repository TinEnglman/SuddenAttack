using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuilding : IBuilding
{
    protected BuildingData _buildingData;
    protected GameObject _prefab;
    protected GameObject _unitPrefab;
    protected float _currentCountdown = 0;
    protected IUnitFactory _unitFactory;
    protected Vector2 _spawnOffset;

    public AbstractBuilding()
    {
        _spawnOffset = new Vector3(1, 1, 0);
    }

    public void SetFactory(IUnitFactory factory)
    {
        _unitFactory = factory;
    }

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

    public GameObject UnitPrefab
    {
        get { return _unitPrefab; }
        set { _unitPrefab = value; }
    }

    public IUnit Update(float dt)
    {
        _currentCountdown -= dt;
        IUnit newUnit = null;
        if (_currentCountdown <= 0)
        {
            _currentCountdown = Data.BuildCooldown;
            newUnit = SpawnUnit();
        }
        return newUnit;
    }

    public abstract IUnit SpawnUnit();
}
