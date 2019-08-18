using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _tankPrefab = null;
    [SerializeField]
    private GameObject _soliderPrefab = null;

    private const int LeftButtonIndex = 0;
    private const int RightButtonIndex = 1;
    private GameManager _gameManager = null;
    private CombatManager _combatManager = null;
    private IUnitFactory _soliderFactory = null;
    private IUnitFactory _tankFactory = null;
    private IUnit _selectedUnit;

    void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = new GameManager(); // refactor for the love of god
        }

        if (_combatManager == null)
        {
            _combatManager = new CombatManager();
        }

        if (_tankFactory == null)
        {
            _tankFactory = new TankFactory();
        }

        if (_soliderFactory == null)
        {
            _soliderFactory = new SoliderFactory();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        InitLevel();
    }

    private void InitLevel()
    {
        var newTank = _tankFactory.CreateUnit(-9, -15, _tankPrefab, true);
        AddUnit(newTank);

        newTank = _tankFactory.CreateUnit(-8, -17, _tankPrefab, true);
        AddUnit(newTank);

        newTank = _tankFactory.CreateUnit(-8.5f, -13, _tankPrefab, true);
        AddUnit(newTank);

        var newSolider = _soliderFactory.CreateUnit(-5.5f, -10, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -10, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -11, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -10, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-4.5f, -10, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-3.5f, -10, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-2.5f, -10, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-2.5f, -8, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-6.5f, -10, _soliderPrefab, false);
        AddUnit(newSolider);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        _combatManager.Update(dt);
        _gameManager.Update(dt);
        UpdateAI();


        if (Input.GetMouseButtonDown(LeftButtonIndex))
        {
            OnLeftMouseDown();
        }

        if (Input.GetMouseButtonUp(LeftButtonIndex))
        {
            OnLeftMouseUp();
        }

        if (Input.GetMouseButtonDown(RightButtonIndex))
        {
            OnRightMouseDown();
        }

        if (Input.GetMouseButtonUp(RightButtonIndex))
        {
            OnRightMouseUp();
        }
    }

    public void AddUnit(IUnit unit)
    {
        _gameManager.AddUnit(unit);
    }

    private void OnRightMouseDown()
    {
    }

    private void OnLeftMouseDown()
    {
    }

    private void OnRightMouseUp()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        IUnit target = null;
        foreach (IUnit unit in _gameManager.Units)
        {
            BoxCollider2D colider = unit.Prefab.GetComponent<BoxCollider2D>(); // fragile construct
            if (colider.bounds.Contains(new Vector3(mousePos.x, mousePos.y, colider.bounds.center.z)))
            {
                target = unit;
            }
        }

        if (_selectedUnit != null) 
        {
            if (target != _selectedUnit && target != null)
            {
                AttackTarget(target);
            }
            else
            {
                MoveSelected(mousePos);
                StopAttacking(_selectedUnit);
            }
        }
    }

    private void OnLeftMouseUp()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_selectedUnit != null)
        {
            DeselectUnit();
        }

        foreach (IUnit unit in _gameManager.Units)
        {
            BoxCollider2D colider = unit.Prefab.GetComponent<BoxCollider2D>(); // fragile construct
            if (colider.bounds.Contains(new Vector3(mousePos.x, mousePos.y, colider.bounds.center.z)))
            {
                if (_selectedUnit != unit)
                {
                    SelectUnit(unit);
                }
                else
                {
                    DeselectUnit();
                }
            }
        }
    }

    private void UpdateAI()
    {
        foreach (IUnit unit in _gameManager.Units)
        {
           
            var targets = _gameManager.GetTargets(unit);
            foreach (IUnit target in targets)
            {
                if (target.Data.IsFriendly != unit.Data.IsFriendly)
                {
                    _combatManager.LockTarget(unit, target);
                }
            }
        }
    }

    private void SelectUnit(IUnit unit)
    {
        _selectedUnit = unit;
        _gameManager.SelectUnit(unit);
    }

    private void DeselectUnit()
    {
        _gameManager.DeselectUnit();
        _selectedUnit = null;
    }

    private void MoveSelected(Vector2 mousePos)
    {
        _gameManager.MoveSelected(mousePos);
    }

    private void AttackTarget(IUnit other)
    {
        _combatManager.LockTarget(_selectedUnit, other);

    }

    private void StopAttacking(IUnit unit)
    {
        _combatManager.ClearAttacker(unit);
    }


}
