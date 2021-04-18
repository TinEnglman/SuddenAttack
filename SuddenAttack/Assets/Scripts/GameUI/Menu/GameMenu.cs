using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuddenAttack.GameUI.Menu
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _returnButton = default;
        [SerializeField]
        private Button _mainMenuButton = default;
        [SerializeField]
        private Button _exitrButton = default;

        private void Awake()
        {
            _returnButton.onClick.AddListener(OnReturnButton);
            _mainMenuButton.onClick.AddListener(OMainMenuButton);
            _exitrButton.onClick.AddListener(OnExitButton);
        }

        public void OnReturnButton()
        {
            gameObject.SetActive(false);
        }

        public void OMainMenuButton()
        {
            SceneManager.LoadScene(0);
        }

        public void OnExitButton()
        {
            Application.Quit();
        }
    }
}