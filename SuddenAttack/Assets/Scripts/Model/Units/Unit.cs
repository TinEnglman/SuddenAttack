using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : IUnit
{
    protected UnitData _unitData;
    protected GameObject _prefab; 

    public UnitData Data
    {
        get { return _unitData; }
        set { _unitData = value; }
    }

    public GameObject Prefab
    {
        get { return _prefab; }
        set { _prefab = value; }
    }
}
