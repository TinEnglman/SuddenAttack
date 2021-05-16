using SuddenAttack.GameUI;
using SuddenAttack.Model;
using SuddenAttack.Model.Commands;
using SuddenAttack.Model.Commands.Factory;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.FlowController
{
    public class LocalPlayerCommandController
    {
        private UnitManager _gameManager;
        private CommandController _commandController;
        private IInputManager _inputManager;
        private UIManager _uiManager;
        private SelectionManager _selectionManager;

        private Vector3 _pressedWorldPosition;
        private Vector3 _pressedScreenPosition;

        private bool _queueActive;
        private bool _attackMoveActive;

        public LocalPlayerCommandController(UnitManager gameManager, CommandController commandController, IInputManager inputManager, SelectionManager selectionManager, UIManager uiManager)
        {
            _gameManager = gameManager;
            _commandController = commandController;
            _inputManager = inputManager;
            _selectionManager = selectionManager;
            _uiManager = uiManager;
        }

        public void Update()
        {
            if (_inputManager.IsRightMouseButtonDown()) // listener candidate
            {
                OnRightMouseDown();
            }

            if (_inputManager.IsRightMouseButtonUp())
            {
                OnRightMouseUp();
            }

            if (_inputManager.IsPressed(KeyCode.LeftShift) || _inputManager.IsPressed(KeyCode.RightShift))
            {
                _queueActive = true;
            }

            if (_inputManager.IsReleased(KeyCode.LeftShift) || _inputManager.IsReleased(KeyCode.RightShift))
            {
                _queueActive = false;
            }

            if (_inputManager.IsPressed(KeyCode.A))
            {
                _attackMoveActive = true;
            }
        }

        private void OnRightMouseDown()
        {
            _pressedWorldPosition = _inputManager.GetMouseWorldPosition();
            _pressedScreenPosition = _inputManager.GetMouseScreenPosition();
        }

        private void OnRightMouseUp()
        {
            Vector2 mouseWorldPos = _inputManager.GetMouseWorldPosition();
            Vector2 mouseScreenPos = _inputManager.GetMouseScreenPosition();

            if (mouseScreenPos.x < _uiManager.InGameUIController.GetScreenWidth())
            {
                return;
            }

            IUnit target = GetUnitUnderPointer(mouseWorldPos);

            foreach (IUnit selectedUnit in _selectionManager.GetSelectedUnits())
            {
                bool hasTarget = target != selectedUnit && target != null;
                bool isHostile = hasTarget && target.TeamIndex != selectedUnit.TeamIndex;

                if (hasTarget)
                {
                    if (isHostile)
                    { 
                        if (_queueActive)
                        {
                            _commandController.AddAttackTargetCommand(selectedUnit, target);
                        }
                        else
                        {
                            _commandController.SetAttackTargetCommand(selectedUnit, target);
                        }
                    }
                    else // target is friendly
                    {
                        if (_queueActive)
                        {
                            _commandController.AddMoveCommand(selectedUnit, mouseWorldPos);
                        }
                        else
                        {
                            _commandController.SetMoveCommand(selectedUnit, mouseWorldPos);
                        }
                    }
                }
                else // does not have target
                {
                    if (_queueActive)
                    {
                        if (_attackMoveActive)
                        {
                            _commandController.AddAttackMoveCommand(selectedUnit, mouseWorldPos);
                        }
                        else
                        { 
                            _commandController.AddMoveCommand(selectedUnit, mouseWorldPos);
                        }
                    }
                    else
                    {
                        if (_attackMoveActive)
                        {
                            _commandController.SetAttackMoveCommand(selectedUnit, mouseWorldPos);
                        }
                        else
                        { 
                            _commandController.SetMoveCommand(selectedUnit, mouseWorldPos);
                        }
                    }
                }
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
    }
}
