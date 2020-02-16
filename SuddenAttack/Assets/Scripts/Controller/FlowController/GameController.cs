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

namespace SuddenAttack.Controller.FlowController
{
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
      
        private GameManager _gameManager = null;
        private CommandManager _commandManager = null;
        private CombatManager _combatManager = default;
        private SelectionManager _selectionManager = default;
        private UnitBuildingManager _unitBuildingManager = default;
        private ICommandFactory _commandFactory = default;
        private IInputManager _inputManager = default;
        private IUnitFactory _soliderFactory = default;
        private IUnitFactory _tankFactory = null;

        private float _incomeFrequency = 9;
        private float _incomeCountdown = 0;

        private List<IBuilding> _buildings = new List<IBuilding>();

        [Inject]
        public void Construct(GameManager gameManager, UnitBuildingManager unitBuildingManager, CombatManager combatManager, TankFactory tankFactory, SoliderFactory soliderFactory, IInputManager inputManager, CommandManager commandManager, ICommandFactory commandFactory, SelectionManager selectionManager)
        {
            _gameManager = gameManager;
            _unitBuildingManager = unitBuildingManager;
            _combatManager = combatManager;
            _soliderFactory = soliderFactory;
            _tankFactory = tankFactory;
            _inputManager = inputManager;
            _commandManager = commandManager;
            _commandFactory = commandFactory;
            _selectionManager = selectionManager;
        }

        public void AddUnit(IUnit unit)
        {
            _gameManager.AddUnit(unit);
        }

        private void Awake()
        {
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

            newSolider = _soliderFactory.CreateUnit(0f, 1, _soliderPrefab, false);
            AddUnit(newSolider);

            newTank = _tankFactory.CreateUnit(-1.5f, -0, _tankPrefab, false);
            AddUnit(newTank);
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
                if (building.Data.IsFriendly)
                {
                    _gameManager.Funds += 25; //building.GetIncome(); // refactor
                }
            }
        }

        // Update is called once per frame
        private void Update()
        {
            float dt = Time.deltaTime;
            _combatManager.Update(dt);
            _gameManager.Update(dt);
            _unitBuildingManager.UpdateBuilding(dt);
            _selectionManager.Update();
            UpdateIncome(dt);
            UpdateAI();

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


        private void OnRightMouseDown()
        {
        }

        private void OnLeftMouseDown()
        {
        }

        private void OnRightMouseUp()
        {
            Vector2 mouseWorldPos = _inputManager.GetMouseWorldPosition();
            Vector2 mouseScreenPos = _inputManager.GetMouseScreenPosition();

            IUnit target = _selectionManager.GetUnitUnderPointer(mouseWorldPos);


            foreach (IUnit selectedUnit in _selectionManager.GetSelectedUnits())
            {
                if (target != selectedUnit && target != null)
                {
                    var command = _commandFactory.CreateAttackTargetCommand(selectedUnit, target);
                    _commandManager.AddCommand(command);

                    //AttackTarget(target);
                    //selectedUnit.IsUserLocked = true;
                }
                else
                {
                    MoveSelected(mouseWorldPos);
                    StopAttacking(selectedUnit);
                    //selectedUnit.IsUserLocked = true;
                }
            }
        }

        private void OnLeftMouseUp()
        {
        }

        private void UpdateAI()
        {
            if (_hqRed.Building.Data.HitPoints > 0)
            {
                //_hqRed.Building.IsSpawning = true; // AI Cheats
                _unitBuildingManager.StartBuildingUnit(_hqRed.Building, false);
            }

            if (_barracksRed.Building.Data.HitPoints > 0)
            {
                //_barracksRed.Building.IsSpawning = true; // AI Cheats
                _unitBuildingManager.StartBuildingUnit(_barracksRed.Building, false);
            }


            foreach (IUnit unit in _gameManager.Units)
            {
                //if (unit.IsUserLocked && unit.Data.IsFriendly)
                if (unit.Data.IsFriendly)
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

      
        private void MoveSelected(Vector2 mousePos)
        {
            //_gameManager.MoveSelected(mousePos);
        }

        private void AttackTarget(IUnit other)
        {
            foreach (IUnit unit in _selectionManager.GetSelectedUnits())
            {
                _combatManager.LockTarget(unit, other);
            }

        }

        private void StopAttacking(IUnit unit)
        {
            _combatManager.ClearAttacker(unit);
        }
    }
}