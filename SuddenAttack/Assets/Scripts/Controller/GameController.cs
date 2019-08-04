using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameManager _gameManager = null;
    private Dictionary<IUnit, GameObject> _unitPrefabs;
    private IUnit _selectedUnit;

    void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = new GameManager(); // refactor for the love of god
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddUnit(IUnit unit, GameObject unitPrefab) // need factory
    {
        _unitPrefabs[unit] = unitPrefab;
        _gameManager.AddUnit(unit);

    }

    private void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void OnMouseUp()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _gameManager.MoveSelected(mousePos);
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
