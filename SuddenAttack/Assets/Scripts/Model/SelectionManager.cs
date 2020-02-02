using SuddenAttack.Model;
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

        private List<IUnit> _selectedUnits = new List<IUnit>();

        public SelectionManager(GameManager gameManager, IInputManager inputManager)
        {
            _inputManager = inputManager;
            _gameManager = gameManager;
        }

        public List<IUnit> GetSelectedUnits()
        {
            return _selectedUnits;
        }

        public void Update()
        {
            if (_inputManager.isLeftMouseButtonDown())
            {
                OnLeftMouseDown();
            }

            if (_inputManager.isLeftMouseButtonUp())
            {
                OnLeftMouseUp();
            }

            if (_inputManager.isRightMouseButtonDown())
            {
                OnRightMouseDown();
            }

            if (_inputManager.isRightMouseButtonUp())
            {
                OnRightMouseUp();
            }
        }

        public IUnit GetUnitUnderPointer(Vector3 mouseWorldPos)
        {
            IUnit target = null;
            foreach (IUnit unit in _gameManager.Units)
            {
                BoxCollider2D colider = unit.Prefab.GetComponent<BoxCollider2D>(); // fragile construct
                if (colider.bounds.Contains(new Vector3(mouseWorldPos.x, mouseWorldPos.y, colider.bounds.center.z)))
                {
                    target = unit;
                }
            }
            return target;
        }

        private void OnRightMouseDown()
        {
        }

        private void OnLeftMouseDown()
        {
            _pressedWorldPosition = _inputManager.GetMouseWorldPosition();
            _pressedScreenPosition = _inputManager.GetMouseScreenPosition();
        }

        private void OnRightMouseUp()
        {
            Vector2 mouseWorldPos = _inputManager.GetMouseWorldPosition();
            Vector2 mouseScreenPos = _inputManager.GetMouseScreenPosition();
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

        private void SelectUnit(IUnit unit)
        {
            _selectedUnits.Add(unit);
        }

        private void DeselectUnits()
        {
            _selectedUnits.Clear();
        }
    }
}
