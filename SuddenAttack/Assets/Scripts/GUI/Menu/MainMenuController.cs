using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuddenAttack.Ui.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private Button _campaignButton = default;
        [SerializeField]
        private Button _skirmishButton = default;
        [SerializeField]
        private Button _multiplayerButton = default;
        [SerializeField]
        private Button _optionsButton = default;
        [SerializeField]
        private Button _exitButton = default;

        private void Awake()
        {
            _campaignButton.onClick.AddListener(OnCampaignButton);
            _skirmishButton.onClick.AddListener(OnSkirmishButton);
            _multiplayerButton.onClick.AddListener(OnMultiplayerButton);
            _optionsButton.onClick.AddListener(OnOptionsButton);
            _exitButton.onClick.AddListener(OnExitButton);

            //until implemented
            _skirmishButton.interactable = false;
            _multiplayerButton.interactable = false;
            _optionsButton.interactable = false;
        }

        public void OnCampaignButton()
        {
            SceneManager.LoadScene(1);
        }

        public void OnSkirmishButton()
        {

        }

        public void OnMultiplayerButton()
        {

        }

        public void OnOptionsButton()
        {

        }

        public void OnExitButton()
        {
            Application.Quit();
        }

    }
}