﻿using SuddenAttack.Model;
using SuddenAttack.Ui.Menu;
using System.Collections;
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
        private GameMenu _gameMenuController;

        [SerializeField]
        private TextMeshProUGUI _foundLabel = null;
        [SerializeField]
        private TextMeshProUGUI _unitNameLabel = null;
        [SerializeField]
        private Button _buildButton = null;
        [SerializeField]
        private Slider _completedSlider = null;

        private IInputManager _inputManager = default;
        private bool _lockBuildingUI = false; // temp hack; remove when proper UI controll is implemented
        private Texture2D _boxSelectionTexture;
        private bool _drawSelecionBox;

        [Inject]
        public void Construct(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
        {
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;

            var topLeft = Vector3.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector3.Max(screenPosition1, screenPosition2);

            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
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

        private void Update()
        {
            if (_inputManager.IsDown(KeyCode.F10))
            {
                _gameMenuController.gameObject.SetActive(true);
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

        private void OnRightMouseDown()
        {
        }

        private void OnLeftMouseDown()
        {
            _drawSelecionBox = true;
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
