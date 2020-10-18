using SuddenAttack.Controller.FlowController;
using SuddenAttack.Model;
using SuddenAttack.Ui.Menu;
using SuddenAttack.Model.Buildings;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuddenAttack.Controller.GameUI
{

    public class GameUIController : MonoBehaviour
    {
        [SerializeField]
        private GameMenu _gameMenuController = default;

        [SerializeField]
        private TextMeshProUGUI _foundLabel = null;
        [SerializeField]
        private TextMeshProUGUI _unitNameLabel = null;
        [SerializeField]
        private Button _buildButton = null;
        [SerializeField]
        private Slider _completedSlider = null;

        private IInputManager _inputManager = default;
        private SelectionManager _selectionManager = default;
        private GameManager _gameManager = default;
        private UnitCreationManager _unitCreationManager = default;
        //private bool _lockBuildingUI = false; // temp hack; remove when proper UI controll is implemented
        private Texture2D _boxSelectionTexture;
        private bool _drawSelecionBox;
        private Vector3 _pressedScreenPosition;

        [Inject]
        public void Construct(UnitCreationManager unitCreationManager, IInputManager inputManager, SelectionManager selectionManager, GameManager gameManager)
        {
            _inputManager = inputManager;
            _selectionManager = selectionManager;
            _gameManager = gameManager;
            _unitCreationManager = unitCreationManager;
        }

        private void Awake()
        {
            _buildButton.onClick.AddListener(OnBuildButton);
            HideBuildingUI();
            _drawSelecionBox = false;
            _boxSelectionTexture = new Texture2D(1, 1);
            _boxSelectionTexture.SetPixel(0, 0, Color.green);
            _boxSelectionTexture.Apply();
        }

        private Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
        {
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;

            var topLeft = Vector3.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector3.Max(screenPosition1, screenPosition2);

            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }


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

            _foundLabel.text = "Funds: " + _gameManager.Funds + " $"; // playerManager

            if (_selectionManager.GetSelectedBuildings().Count == 1)
            {
                var selectedBuilding = _selectionManager.GetSelectedBuildings()[0];
                RefreshBuildingUI(selectedBuilding);
                ShowBuildingUI();
                _completedSlider.normalizedValue = _unitCreationManager.GetCompletePercent(selectedBuilding);
                //_completedSlider.normalizedValue = (_selectionManager.GetSelectedBuildings()[0]).GetCompletePercent();
            }
            else
            {
                HideBuildingUI();
            }
        }

        private void RefreshBuildingUI(IBuilding building)
        {
            _unitNameLabel.text = building.BuildingData.DisplayName;
            //_buildButton.GetComponentInChildren<TextMeshProUGUI>().text = building.GetFactory().GetDisplayName() + " : " + building.GetFactory().GetCost() + " $ ";
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

        private void OnBuildButton()
        {
            var building = _selectionManager.GetSelectedBuildings()[0];
            //int cost = building.GetFactory().GetCost();
            int cost = 0;
            if (_gameManager.Funds >= cost && !_unitCreationManager.IsBuilding(building))
            {
                _gameManager.Funds -= cost;
                _unitCreationManager.StartBuildingUnit(building, 0);

            }
        }

        private void Update()
        {
            if (_inputManager.IsPressed(KeyCode.F10))
            {
                _gameMenuController.gameObject.SetActive(true);
            }

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

        private void OnRightMouseDown()
        {
        }

        private void OnLeftMouseDown()
        {
            _drawSelecionBox = true;
            _pressedScreenPosition = _inputManager.GetMouseScreenPosition();
        }

        private void OnRightMouseUp()
        {
        }

        private void OnLeftMouseUp()
        {
            _drawSelecionBox = false;
        }
    }
}
