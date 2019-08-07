using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : IUnit
{
    protected UnitData _unitData;
    protected GameObject _prefab;

    public GameObject Prefab
    {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public UnitData Data
    {
        get { return _unitData; }
        set { _unitData = value; }
    }

    public void Move(Vector3 destination)
    {
        _prefab.GetComponent<UnitController>().SetDestination(destination);
    }

    public void Select()
    {
        _prefab.GetComponent<UnitController>().Select();
    }

    public void Deselect()
    {
        _prefab.GetComponent<UnitController>().Deselect();
    }
}
