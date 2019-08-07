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
