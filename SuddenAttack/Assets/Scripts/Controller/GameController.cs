using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _tankPrefab;
    [SerializeField]
    private GameObject _soliderPrefab;

    private const int LeftButtonIndex = 0;
    private const int RightButtonIndex = 1;
    private GameManager _gameManager = null;
    private IUnitFactory _soliderFactory = null;
    private IUnitFactory _tankFactory = null;
    private IUnit _selectedUnit;

    void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = new GameManager(); // refactor for the love of god
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
        var newTank = _tankFactory.CreateUnit(-8, -20, _tankPrefab);

    }

    // Update is called once per frame
    void Update()
    {
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

    public void AddUnit(IUnit unit, GameObject unitPrefab) // need factory
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
        _gameManager.MoveSelected(mousePos);

    }

    private void OnLeftMouseUp()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _gameManager.SelectUnit(_selectedUnit);
    }

    private void SelectUnit(IUnit unit)
    {
        _gameManager.SelectUnit(unit);
    }

    private void DeselectUnit()
    {
        _gameManager.DeselectUnit();
    }

    private void MoveUnit()
    {

    }

    


}
