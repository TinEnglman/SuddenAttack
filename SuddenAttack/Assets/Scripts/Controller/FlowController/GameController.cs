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
        /*
        [SerializeField]
        private GameMenu _gameMenuController;

        [SerializeField]
        private TextMeshProUGUI _foundLabel = null;
        [SerializeField]
        private TextMeshProUGUI _unitNameLabel = null;
        [SerializeField]
        private Button _buildButton = null;
        [SerializeField]
        private Slider _completedSlider = null;
        */
        private GameManager _gameManager = null;
        private CommandManager _commandManager = null;
        private CombatManager _combatManager = default;
        private SelectionManager _selectionManager = default;
        private ICommandFactory _commandFactory = default;
        private IInputManager _inputManager = default;
        private IUnitFactory _soliderFactory = default;
        private IUnitFactory _tankFactory = null;
        //private List<IUnit> _selectedUnits;
        //private Vector3 _pressedWorldPosition;
        //private Vector3 _pressedScreenPosition;
        //private bool _drawSelecionBox;
        private float _incomeFrequency = 9;
        private float _incomeCountdown = 0;

        //private bool _lockBuildingUI = false; // temp hack; remove when proper UI controll is implemented


        private List<IBuilding> _buildings = new List<IBuilding>();
        //private Texture2D _boxSelectionTexture;

        [Inject]
        public void Construct(GameManager gameManager, CombatManager combatManager, TankFactory tankFactory, SoliderFactory soliderFactory, IInputManager inputManager, CommandManager commandManager, ICommandFactory commandFactory)
        {
            _gameManager = gameManager;
            _combatManager = combatManager;
            _soliderFactory = soliderFactory;
            _tankFactory = tankFactory;
            _inputManager = inputManager;
            _commandManager = commandManager;
            _commandFactory = commandFactory;
        }

        private void Awake()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            //_drawSelecionBox = false;
            //_boxSelectionTexture = new Texture2D(1, 1);
            //_boxSelectionTexture.SetPixel(0, 0, Color.green);
            //_boxSelectionTexture.Apply();

            InitLevel();
            //_selectedUnits = new List<IUnit>();

            //_buildButton.onClick.AddListener(OnBuildButton);
            //HideBuildingUI();
        }

        /*
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
                Vector2 mousePos = _inputManager.GetMouseScreenPosition();
                DrawSelectionBox(GetScreenRect(_pressedScreenPosition, mousePos), 2);
            }

            _foundLabel.text = "Funds: " + _gameManager.Funds + " $";

            if (_selectedUnits.Count == 1)
            {
                if (_selectedUnits[0].IsBuilding())
                {
                    _completedSlider.normalizedValue = ((IBuilding)_selectedUnits[0]).GetCompletePercent();
                }
            }
        }

        private void ShowBuildingUI()
        {
            _completedSlider.gameObject.SetActive(true);
            _buildButton.gameObject.SetActive(true);
            _unitNameLabel.gameObject.SetActive(true);
        }

        private void HideBuildingUI()
        {
            _completedSlider.gameObject.SetActive(false);
            _buildButton.gameObject.SetActive(false);
            _unitNameLabel.gameObject.SetActive(false);
        }
     

        public Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
        {
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;

            var topLeft = Vector3.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector3.Max(screenPosition1, screenPosition2);

            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

     
        public void OnBuildButton()
        {
            var building = ((IBuilding)_selectedUnits[0]);
            int cost = building.GetFactory().GetCost();
            if (_gameManager.Funds >= cost && !building.IsSpawning)
            {
                _gameManager.Funds -= cost;
                building.IsSpawning = true;
                _lockBuildingUI = true;
            }
        }
        */

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

        // Update is called once per frame
        private void Update()
        {
            float dt = Time.deltaTime;
            _combatManager.Update(dt);
            _gameManager.Update(dt);
            _selectionManager.Update();
            UpdateIncome(dt);
            UpdateAI();

            foreach (IBuilding builing in _buildings)
            {
                IUnit newUnit = builing.Update(dt);
                if (newUnit != null)
                {
                    AddUnit(newUnit);
                }
            }

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
                    _gameManager.Funds += building.GetIncome();
                }
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
                    selectedUnit.IsUserLocked = true;
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
                _hqRed.Building.IsSpawning = true; // AI Cheats
            }

            if (_barracksRed.Building.Data.HitPoints > 0)
            {
                _barracksRed.Building.IsSpawning = true; // AI Cheats
            }


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

        /*
        private void SelectUnit(IUnit unit)
        {
            _selectedUnits.Add(unit);
            _gameManager.SelectUnit(unit);

            if (unit.IsBuilding())
            {
                //refactor selection
                //_unitNameLabel.text = unit.Data.DisplayName;
                //_buildButton.GetComponentInChildren<TextMeshProUGUI>().text = ((IBuilding)unit).GetFactory().GetDisplayName() + " : " + ((IBuilding)unit).GetFactory().GetCost() + " $ ";
                //ShowBuildingUI();
            }
        }

        private void DeselectUnits()
        {
            _gameManager.DeselectUnits();
            _selectedUnits.Clear();
            //HideBuildingUI();
        }
        */
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