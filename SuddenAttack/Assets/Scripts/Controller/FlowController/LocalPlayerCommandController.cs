﻿using SuddenAttack.Model;
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
        private GameManager _gameManager;
        private CommandController _commandController;
        private IInputManager _inputManager;
        private SelectionManager _selectionManager;

        private Vector3 _pressedWorldPosition;
        private Vector3 _pressedScreenPosition;

        private bool _queueActive;

        public LocalPlayerCommandController(GameManager gameManager, CommandController commandController, IInputManager inputManager, SelectionManager selectionManager)
        {
            _gameManager = gameManager;
            _commandController = commandController;
            _inputManager = inputManager;
            _selectionManager = selectionManager;
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

            IUnit target = GetUnitUnderPointer(mouseWorldPos);

            foreach (IUnit selectedUnit in _selectionManager.GetSelectedUnits())
            {
                if (target != selectedUnit && target != null)
                {
                    
                    if (target.TeamIndex != selectedUnit.TeamIndex)
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
                    else
                    {
                        // add logic for right clicking on a friendly unit
                    }
                }
                else
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
