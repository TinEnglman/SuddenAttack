using SuddenAttack.GameUI.Menu;
using SuddenAttack.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SuddenAttack.GameUI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        GameUIController _gameUIController = default;
        [SerializeField]
        InGameUIController _inGameUIController = default;
        [SerializeField]
        private GameMenu _gameMenuController = default;

        private IInputManager _inputManager;

        public GameUIController GameUIContorller { get { return _gameUIController; } set { _gameUIController = value; } }
        public InGameUIController InGameUIController { get { return _inGameUIController; } set { _inGameUIController = value; } }
        public GameMenu GameMenu { get { return _gameMenuController; } set { _gameMenuController = value; } }

        [Inject]
        public void Construct(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Update()
        {
            if (_inputManager.IsPressed(KeyCode.F10))
            {
                GameMenu.ShowGameMenu();
            }

        }

    }
}
