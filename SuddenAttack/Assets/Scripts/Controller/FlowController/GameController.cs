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
using SuddenAttack.GameUI.Menu;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Behavior;
using SuddenAttack.GameUI;

namespace SuddenAttack.Controller.FlowController
{
    public class GameController : MonoBehaviour
    {
        private const int LOCAL_PLAYER_TEAM_INDEX = 0;
        private const int PROTOTYPE_AI_TEAM_INDEX = 1;

        private BuildingFactoryManager _buildingFactoryManager;
        private MobileUnitFactoryManager _unitFactoryManager;
        private UnitManager _unitManager;
        private CommandManager _commandManager;
        private CombatManager _combatManager;
        private SelectionManager _selectionManager;
        private UnitCreationManager _unitBuildingManager;
        private BehaviorManager _behaviorManager;
        private LocalPlayerCommandController _localPlayerCommandController;
        private UIManager _uiManager;
        private IInputManager _inputManager;

        private float _incomeFrequency = 9;
        private float _incomeCountdown = 0;

       [Inject]
        public void Construct(UIManager uiManager, BehaviorManager behaviorManager, MobileUnitFactoryManager unitFactoryManager, BuildingFactoryManager buildingFactoryManager, UnitManager gameManager, UnitCreationManager unitBuildingManager, CombatManager combatManager, IInputManager inputManager, CommandManager commandManager, SelectionManager selectionManager, LocalPlayerCommandController localPlayerCommandController)
        {
            _buildingFactoryManager = buildingFactoryManager;
            _unitFactoryManager = unitFactoryManager;
            _unitManager = gameManager;
            _unitBuildingManager = unitBuildingManager;
            _behaviorManager = behaviorManager;
            _combatManager = combatManager;
            _inputManager = inputManager;
            _commandManager = commandManager;
            _selectionManager = selectionManager;
            _localPlayerCommandController = localPlayerCommandController;
            _uiManager = uiManager;
        }

        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Setup();
        }

        private void SetupLevel()
        {
            var building = _buildingFactoryManager.CreateBuilding(-9, -15, "Barracks", LOCAL_PLAYER_TEAM_INDEX);
            var building2 = _buildingFactoryManager.CreateBuilding(-4, -15, "HQ", LOCAL_PLAYER_TEAM_INDEX);
            var unit = _unitFactoryManager.CreateUnit("Tank", - 12, -16, LOCAL_PLAYER_TEAM_INDEX);
            var unit2 = _unitFactoryManager.CreateUnit("Panzer", -16, -16, LOCAL_PLAYER_TEAM_INDEX);
            var unit3 = _unitFactoryManager.CreateUnit("Solider", -18, -16, LOCAL_PLAYER_TEAM_INDEX);

            var aiBuilding = _buildingFactoryManager.CreateBuilding(-9, -5, "Barracks", PROTOTYPE_AI_TEAM_INDEX);
            var aiUnit = _unitFactoryManager.CreateUnit("Solider", -12, -6, PROTOTYPE_AI_TEAM_INDEX);
            var aiUnit2 = _unitFactoryManager.CreateUnit("Solider", -16, -6, PROTOTYPE_AI_TEAM_INDEX);

            _unitManager.AddBuilding(building);
            _unitManager.AddBuilding(building2);
            _unitManager.AddMobileUnit(unit);
            _unitManager.AddMobileUnit(unit2);
            _unitManager.AddMobileUnit(unit3);

            _unitManager.AddBuilding(aiBuilding);
            _unitManager.AddMobileUnit(aiUnit);
            _unitManager.AddMobileUnit(aiUnit2);
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
            _unitManager.Update(dt);
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

            foreach (var building in _unitManager.Buildings)
            {
                if (building.TeamIndex == LOCAL_PLAYER_TEAM_INDEX)
                {
                    _unitManager.Funds += building.BuildingData.FundsGeneration; //building.GetIncome(); // refactor
                    _uiManager.InGameUIController.RefreshFunds();
                }
            }
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
    }
}