using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private List<IUnit> _units = null;
    private List<IBuilding> _buildings = null;

    private IUnit _selectedUnit;

    public List<IUnit> Units
    {
        get { return _units; }
    }

    public List<IBuilding> Buildings
    {
        get { return _buildings; }
    }

    public GameManager()
    {
        _selectedUnit = null;
        _units = new List<IUnit>();
        _buildings = new List<IBuilding>();
    }

    public void Update(float dt)
    {
        List<IUnit> killList = new List<IUnit>();
        foreach (IUnit unit in _units)
        {
            unit.Update();
            if (unit.Data.HitPoints <= 0)
            {
                killList.Add(unit);
            }
        }

        foreach(IUnit unit in killList)
        {
            _units.Remove(unit);
            unit.Prefab.SetActive(false);
        }
    }

    public void SelectUnit(IUnit unit)
    {

        _selectedUnit = unit;
        _selectedUnit.Select();
    }

    public void DeselectUnit()
    {

        _selectedUnit.Deselect();
        _selectedUnit = null;
    }

    public void AddUnit(IUnit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(IUnit unit)
    {
        _units.Remove(unit);
    }

    public void MoveSelected(Vector3 destination)
    {
        _selectedUnit.Move(destination);
    }

    public IUnit GetSelected()
    {
        return _selectedUnit;
    }

    public List<IUnit> GetTargets(IUnit source) // called form update; slow af; refactor
    {
        List<IUnit> targets = new List<IUnit>();
        foreach (IUnit unit in _units)
        {
            if (source.Data.EngageRange > (unit.Prefab.transform.position - source.Prefab.transform.position).magnitude)
            {
                targets.Add(unit);
            }
        }

        return targets;
    }
}
