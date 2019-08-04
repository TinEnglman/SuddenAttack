using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private List<IUnit> _units;
    private List<IBuilding> _buildings;

    private IUnit _selectedUnit;

    public GameManager()
    {
        _selectedUnit = null;
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
}
