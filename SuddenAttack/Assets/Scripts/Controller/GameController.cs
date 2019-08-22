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
    //private IUnit _selectedUnit;
    private List<IUnit> _selectedUnits;
    private Vector3 _pressedPosition;
    private bool _drawSelecionBox;

    private List<IBuilding> _buildings = new List<IBuilding>();
    private Texture2D _boxSelectionTexture;

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
        _drawSelecionBox = false;
        _boxSelectionTexture = new Texture2D(1, 1);
        _boxSelectionTexture.SetPixel(0, 0, Color.green);
        _boxSelectionTexture.Apply();

        InitLevel();
        _selectedUnits = new List<IUnit>();
    }

    private void DrawBorder(Rect rect)
    {
        GUI.color = Color.green;
        GUI.DrawTexture(rect, _boxSelectionTexture);
    }

    private void DrawSelectionBox(Rect rect, float thickness)
    {
        DrawBorder(new Rect(rect.xMin, rect.yMin, rect.width, thickness));
        DrawBorder(new Rect(rect.xMin, rect.yMin, thickness, rect.height));
        DrawBorder(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height));
        DrawBorder(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness));
    }

    private void OnGUI()
    {
        if (_drawSelecionBox == true)
        {
            Vector2 mousePos = Input.mousePosition;
            DrawSelectionBox(GetScreenRect(_pressedPosition, mousePos), 2);
        }
    }

    public Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;

        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);

        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    private void InitLevel()
    {
        var hqRed = new HeadQuartes(false);
        hqRed.SetFactory(_tankFactory);
        hqRed.UnitPrefab = _tankPrefab;
        hqRed.Prefab = _hqRedPrefab;
        _buildings.Add(hqRed);
        _hqRed.Building = hqRed;
        AddUnit((IUnit)hqRed);

        var hqBlue = new HeadQuartes(true);
        hqBlue.SetFactory(_tankFactory);
        hqBlue.UnitPrefab = _tankPrefab;
        hqBlue.Prefab = _hqBluePrefab;
        _buildings.Add(hqBlue);
        _hqBlue.Building = hqBlue;
        AddUnit((IUnit)hqBlue);

        var barracksRed = new Barracks(false);
        barracksRed.SetFactory(_soliderFactory);
        barracksRed.UnitPrefab = _soliderPrefab;
        barracksRed.Prefab = _barracksRedPrefab;
        _buildings.Add(barracksRed);
        _barracksRed.Building = barracksRed;
        AddUnit((IUnit)barracksRed);

        var barracksBlue = new Barracks(true);
        barracksBlue.SetFactory(_soliderFactory);
        barracksBlue.UnitPrefab = _soliderPrefab;
        barracksBlue.Prefab = _barracksBluePrefab;
        _buildings.Add(barracksBlue);
        _barracksBlue.Building = barracksBlue;
        AddUnit((IUnit)barracksBlue);


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
        _pressedPosition = Input.mousePosition;
        _drawSelecionBox = true;
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

        //if (_selectedUnit != null) 
        foreach(IUnit selectedUnit in _selectedUnits)
        {
            if (target != selectedUnit && target != null)
            {
                AttackTarget(target);
                selectedUnit.IsUserLocked = true;
            }
            else
            {
                MoveSelected(mousePos);
                StopAttacking(selectedUnit);
                selectedUnit.IsUserLocked = true;
            }
        }
    }

    private void OnLeftMouseUp()
    {
        _drawSelecionBox = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pressedPos = Camera.main.ScreenToWorldPoint(_pressedPosition);

        if (_selectedUnits.Count > 0)
        {
            DeselectUnits();
        }

        bool unitSelected = false;
        foreach (IUnit unit in _gameManager.Units)
        {
            if (!unit.Data.IsFriendly)
            {
                continue;
            }
            BoxCollider2D colider = unit.Prefab.GetComponent<BoxCollider2D>(); // fragile construct
            Rect selectionRect = new Rect(pressedPos.x, pressedPos.y, mousePos.x - pressedPos.x, mousePos.y - pressedPos.y);
            Vector2 unitCenter = new Vector2(unit.Prefab.transform.position.x, unit.Prefab.transform.position.y);
            if (selectionRect.Contains(unitCenter, true) || colider.bounds.Contains(pressedPos))
            {
                if (!_selectedUnits.Contains(unit))
                {
                    SelectUnit(unit);
                    unitSelected = true;
                }
            }
        }

        if (!unitSelected)
        {
            DeselectUnits();
        }
    }

    private void UpdateAI()
    {
        foreach (IUnit unit in _gameManager.Units)
        {
            if (unit.IsUserLocked && unit.Data.IsFriendly)
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
        _selectedUnits.Add(unit);
        _gameManager.SelectUnit(unit);
    }

    private void DeselectUnits()
    {
        _gameManager.DeselectUnits();
        _selectedUnits.Clear();
    }

    private void MoveSelected(Vector2 mousePos)
    {
        _gameManager.MoveSelected(mousePos);
    }

    private void AttackTarget(IUnit other)
    {
        foreach(IUnit unit in _selectedUnits)
        { 
            _combatManager.LockTarget(unit, other);
        }

    }

    private void StopAttacking(IUnit unit)
    {
        _combatManager.ClearAttacker(unit);
    }


}
