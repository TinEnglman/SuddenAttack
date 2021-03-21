using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuddenAttack.Controller.GameUI
{
    public class BuildingUIController : MonoBehaviour
    {
        [SerializeField]
        private Button _buildButton = null;
        [SerializeField]
        private Slider _completedSlider = null;


        private void Awake()
        {
            _buildButton.onClick.AddListener(OnBuildButton);
        }

        private void OnBuildButton()
        {
            /*
            var building = _selectionManager.GetSelectedBuildings()[0];
            //int cost = building.GetFactory().GetCost();
            int cost = 0;
            if (_gameManager.Funds >= cost && !_unitCreationManager.IsBuilding(building))
            {
                _gameManager.Funds -= cost;
                _unitCreationManager.StartBuildingUnit(building, 0);

            }
            */
        }

        private void ShowBuildingUI()
        {
            _completedSlider.gameObject.SetActive(true);
            _buildButton.gameObject.SetActive(true);
           
        }

        private void HideBuildingUI()
        {
            _completedSlider.gameObject.SetActive(false);
            _buildButton.gameObject.SetActive(false);
        }


        private void OnGUI()
        {
            /*
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
            */
        }
    }
}