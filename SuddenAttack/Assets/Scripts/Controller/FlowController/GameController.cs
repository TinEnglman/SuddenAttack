using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SuddenAttack.Model;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Commands.Factory;
using SuddenAttack.Model.Units;
using SuddenAttack.Ui.Menu;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Behavior;

namespace SuddenAttack.Controller.FlowController
{
    public class GameController : MonoBehaviour
    {
        private BuildingFactoryManager _buildingFactoryManager;
        private UnitFactoryManager _unitFactoryManager;
        private GameManager _gameManager;
        private CommandManager _commandManager;
        private CombatManager _combatManager;
        private SelectionManager _selectionManager;
        private UnitCreationManager _unitBuildingManager;
        private BehaviorManager _behaviorManager;
        private LocalPlayerCommandController _localPlayerCommandController;
        //private ICommandFactory _commandFactory;
        private IInputManager _inputManager;

        private float _incomeFrequency = 9;
        private float _incomeCountdown = 0;

        private List<IBuilding> _buildings = new List<IBuilding>();

        [Inject]
        public void Construct(BehaviorManager behaviorManager, UnitFactoryManager unitFactoryManager, BuildingFactoryManager buildingFactoryManager, GameManager gameManager, UnitCreationManager unitBuildingManager, CombatManager combatManager, IInputManager inputManager, CommandManager commandManager, SelectionManager selectionManager, LocalPlayerCommandController localPlayerCommandController)
        {
            _buildingFactoryManager = buildingFactoryManager;
            _unitFactoryManager = unitFactoryManager;
            _gameManager = gameManager;
            _unitBuildingManager = unitBuildingManager;
            _behaviorManager = behaviorManager;
            _combatManager = combatManager;
            _inputManager = inputManager;
            _commandManager = commandManager;
            _selectionManager = selectionManager;
            _localPlayerCommandController = localPlayerCommandController;
        }

        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Setup();
        }

        private void SetupLevel()
        {
            var building = _buildingFactoryManager.CreateBuilding(-9, -15, "Barracks", true);
            var unit = _unitFactoryManager.CreateUnit("Solider", - 12, -16, true);

            _gameManager.AddBuilding(building);
            _gameManager.AddMobileUnit(unit);
        }

        private void Setup()
        {
            _buildingFactoryManager.Setup();
            _unitFactoryManager.Setup();
            SetupLevel();
        }

        // Update is called once per frame
        private void Update()
        {
            float dt = Time.deltaTime;
            _combatManager.Update(dt);
            _gameManager.Update(dt);
            _unitBuildingManager.UpdateBuilding(dt);
            _behaviorManager.UpdateBehaviors(dt);
            _commandManager.Update();
            _localPlayerCommandController.Update();

            _selectionManager.Update();

            UpdateIncome(dt);
            UpdateAI();

            if (_inputManager.IsLeftMouseButtonDown())
            {
                OnLeftMouseDown();
            }

            if (_inputManager.IsLeftMouseButtonUp())
            {
                OnLeftMouseUp();
            }

            if (_inputManager.IsRightMouseButtonDown())
            {
                OnRightMouseDown();
            }

            if (_inputManager.IsRightMouseButtonUp())
            {
                OnRightMouseUp();
            }
        }

        private void UpdateIncome(float dt)
        {
            _incomeCountdown -= dt;

            if (_incomeCountdown <= 0)
            {
                _incomeCountdown += _incomeFrequency;
            }
            else
            {
                return;
            }

            foreach (var building in _buildings)
            {
                if (building.IsFriendly)
                {
                    _gameManager.Funds += 25; //building.GetIncome(); // refactor
                }
            }
        }

        public void AddMobileUnit(IMobileUnit unit)
        {
            _gameManager.AddMobileUnit(unit);
        }

        private void OnRightMouseDown()
        {
        }

        private void OnLeftMouseDown()
        {
        }

        private void OnRightMouseUp()
        {
        }

        private void OnLeftMouseUp()
        {
        }

        private void UpdateAI()
        {
            //if (_hqRed.Building.HitPoints > 0)
            {
                //_hqRed.Building.IsSpawning = true; // AI Cheats
                //_unitBuildingManager.StartBuildingUnit(_hqRed.Building, false);
            }

            //if (_barracksRed.Building.HitPoints > 0)
            {
                //_barracksRed.Building.IsSpawning = true; // AI Cheats
                //_unitBuildingManager.StartBuildingUnit(_barracksRed.Building, false);
            }

            /*
            foreach (IUnit unit in _gameManager.Units)
            {
                //if (unit.IsUserLocked && unit.Data.IsFriendly)
                if (unit.IsFriendly)
                {
                    continue;
                }

                var targets = _gameManager.GetTargets(unit as IMobileUnit);
                float distance = 0;
                IUnit closestTarget = null;
                foreach (IUnit target in targets)
                {
                    if (target.IsFriendly != unit.IsFriendly)
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
                    //_combatManager.LockTarget(unit, closestTarget);
                }
               
            }
             */
        }


        private void MoveSelected(Vector2 mousePos)
        {
            //_gameManager.MoveSelected(mousePos);
        }

        private void AttackTarget(IUnit other)
        {
            foreach (IUnit unit in _selectionManager.GetSelectedUnits())
            {
                //_combatManager.LockTarget(unit, other);
            }

        }

        private void StopAttacking(IUnit unit)
        {
            //_combatManager.StopAttacking(unit);
        }
    }
}