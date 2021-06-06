using SuddenAttack.Controller.FlowController;
using SuddenAttack.Model;
using SuddenAttack.GameUI.Menu;
using SuddenAttack.Model.Buildings;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuddenAttack.GameUI
{

    public class GameUIController : MonoBehaviour
    {
        private IInputManager _inputManager;
        private Texture2D _boxSelectionTexture;
        private bool _drawSelecionBox;
        private Vector3 _pressedScreenPosition;

        [Inject]
        public void Construct(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }


        private void Awake()
        {
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
        }

        private void Update()
        {
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
