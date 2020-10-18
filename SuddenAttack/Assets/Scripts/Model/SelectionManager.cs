using SuddenAttack.Model;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Units;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SuddenAttack.Controller.FlowController
{ 
    public class SelectionManager
    {
        private IInputManager _inputManager = default;
        private GameManager _gameManager = null;

        private Vector3 _pressedWorldPosition;
        private Vector3 _pressedScreenPosition;
        private int _localPlayerTeamIndex;

        private List<IUnit> _selectedUnits = new List<IUnit>();

        public SelectionManager(GameManager gameManager, IInputManager inputManager)
        {
            _inputManager = inputManager;
            _gameManager = gameManager;
            _localPlayerTeamIndex = 0;
        }

        public void Update()
        {
            if (_inputManager.IsLeftMouseButtonDown()) // listener candidate
            {
                OnLeftMouseDown();
            }

            if (_inputManager.IsLeftMouseButtonUp())
            {
                OnLeftMouseUp();
            }
        }

        public List<IUnit> GetSelectedUnits()
        {
            return _selectedUnits;
        }

        public List<IBuilding> GetSelectedBuildings()
        {
            List<IBuilding> buildings = new List<IBuilding>();
            foreach(var unit in _selectedUnits)
            {
                IBuilding building = unit as IBuilding;
                if (building != null)
                {
                    buildings.Add(building);
                }
            }
            return buildings;
        }

        private void OnLeftMouseDown()
        {
            _pressedWorldPosition = _inputManager.GetMouseWorldPosition();
            _pressedScreenPosition = _inputManager.GetMouseScreenPosition();
        }

        private void OnLeftMouseUp()
        {
            Vector2 mousePos = _inputManager.GetMouseWorldPosition();
            Vector2 pressedPos = _pressedWorldPosition;

            if (_selectedUnits.Count > 0)
            {
                DeselectUnits();
            }

            bool unitSelected = false;
            foreach (IUnit unit in _gameManager.Units)
            {
                if (unit.TeamIndex != _localPlayerTeamIndex)
                {
                    continue;
                }

                BoxCollider2D colider = unit.Prefab.GetComponent<BoxCollider2D>();
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

        private void SelectUnit(IUnit unit)
        {
            unit.Select();
            _selectedUnits.Add(unit);
        }

        private void DeselectUnits()
        {
            foreach (var unit in _selectedUnits)
            {
                unit.Deselect();
            }
            _selectedUnits.Clear();
        }
    }
}
