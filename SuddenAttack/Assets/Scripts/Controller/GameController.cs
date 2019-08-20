using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _tankPrefab = null;
    [SerializeField]
    private GameObject _soliderPrefab = null;
    [SerializeField]
    private GameObject _hqRedPrefab = null;
    [SerializeField]
    private GameObject _hqBluePrefab = null;
    [SerializeField]
    private GameObject _barracksRedPrefab = null;
    [SerializeField]
    private GameObject _barracksBluePrefab = null;


    [SerializeField]
    private BuildingController _hqBlue = null;
    [SerializeField]
    private BuildingController _hqRed = null;
    [SerializeField]
    private BuildingController _barracksBlue = null;
    [SerializeField]
    private BuildingController _barracksRed = null;

    private const int LeftButtonIndex = 0;
    private const int RightButtonIndex = 1;
    private GameManager _gameManager = null;
    private CombatManager _combatManager = null;
    private IUnitFactory _soliderFactory = null;
    private IUnitFactory _tankFactory = null;
    private IUnit _selectedUnit;

    private List<IBuilding> _buildings = new List<IBuilding>();

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
        var hqRed = new HeadQuartes(false);
        hqRed.SetFactory(_tankFactory);
        hqRed.UnitPrefab = _tankPrefab;
        hqRed.Prefab = _hqRedPrefab;
        _buildings.Add(hqRed);
        _hqRed.Building = hqRed;

        var hqBlue = new HeadQuartes(true);
        hqBlue.SetFactory(_tankFactory);
        hqBlue.UnitPrefab = _tankPrefab;
        hqBlue.Prefab = _hqBluePrefab;
        _buildings.Add(hqBlue);
        _hqBlue.Building = hqBlue;

        var barracksRed = new Barracks(false);
        barracksRed.SetFactory(_soliderFactory);
        barracksRed.UnitPrefab = _soliderPrefab;
        barracksRed.Prefab = _barracksRedPrefab;
        _buildings.Add(barracksRed);
        _barracksRed.Building = barracksRed;

        var barracksBlue = new Barracks(true);
        barracksBlue.SetFactory(_soliderFactory);
        barracksBlue.UnitPrefab = _soliderPrefab;
        barracksBlue.Prefab = _barracksBluePrefab;
        _buildings.Add(barracksBlue);
        _barracksBlue.Building = barracksBlue;


        var newTank = _tankFactory.CreateUnit(-9, -15, _tankPrefab, true);
        AddUnit(newTank);

        newTank = _tankFactory.CreateUnit(-8, -17, _tankPrefab, true);
        AddUnit(newTank);

        newTank = _tankFactory.CreateUnit(-8.5f, -13, _tankPrefab, true);
        AddUnit(newTank);

        var newSolider = _soliderFactory.CreateUnit(-8f, -15, _soliderPrefab, true);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -15, _soliderPrefab, true);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -3, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -2, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -1, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-5.5f, -1, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-4.5f, -1, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-3.5f, -1, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-2.5f, -1, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-2.5f, -8, _soliderPrefab, false);
        AddUnit(newSolider);

        newSolider = _soliderFactory.CreateUnit(-6.5f, -1, _soliderPrefab, false);
        AddUnit(newSolider);

        newTank = _tankFactory.CreateUnit(-1.5f, -4, _tankPrefab, false);
        AddUnit(newTank);

        newSolider = _soliderFactory.CreateUnit( 0f, 1, _soliderPrefab, false);
        AddUnit(newSolider);

        newTank = _tankFactory.CreateUnit(-1.5f, -0, _tankPrefab, false);
        AddUnit(newTank);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        _combatManager.Update(dt);
        _gameManager.Update(dt);
        UpdateAI();

    
        foreach (IBuilding builing in _buildings)
        {
            IUnit newUnit = builing.Update(dt);
            if (newUnit != null)
            {
                AddUnit(newUnit);
            }
        }

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
            if (unit.IsMoving && unit.Data.IsFriendly || _combatManager.HasLock(unit) && unit.Data.IsFriendly)
            {
                continue;
            }

            var targets = _gameManager.GetTargets(unit);
            float distance = 0;
            IUnit closestTarget = null; 
            foreach (IUnit target in targets)
            {
                if (target.Data.IsFriendly != unit.Data.IsFriendly)
                {
                    if (distance == 0)
                    { 
                        distance = (target.Prefab.transform.position - unit.Prefab.transform.position).magnitude;
                        closestTarget = target;
                    }

                    float currentDistance = (target.Prefab.transform.position - unit.Prefab.transform.position).magnitude;
                    if (currentDistance < distance)
                    {
                        closestTarget = target;
                        currentDistance = distance;
                    }
                }
            }
            if (closestTarget != null)
            {
                _combatManager.LockTarget(unit, closestTarget);
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
