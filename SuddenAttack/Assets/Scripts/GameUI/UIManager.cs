using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.GameUI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        GameUIController _gameUIController = default;
        [SerializeField]
        InGameUIController _inGameUIController = default;


        public GameUIController GameUIContorller { get { return _gameUIController; } set { _gameUIController = value; } }
        public InGameUIController InGameUIController { get { return _inGameUIController; } set { _inGameUIController = value; } }

    }
}
