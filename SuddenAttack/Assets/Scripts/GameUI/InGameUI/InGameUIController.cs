using SuddenAttack.Model;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuddenAttack.GameUI
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField]
        private List<Button> _gridButtons = new List<Button>();
        [SerializeField]
        private TextMeshProUGUI _foundLabel = default;
        [SerializeField]
        private TextMeshProUGUI _unitNameLabel = default;

        private UnitCreationManager _unitCreationManager;
        private UnitManager _unitManager;

        public IUnit SelectedUnit { get; set; }

        [Inject]
        public void Construct(UnitCreationManager unitCreationManager, UnitManager unitManager)
        {
            _unitManager = unitManager;
            _unitCreationManager = unitCreationManager;
        }

        public void Refresh()
        {
            foreach (var button in _gridButtons)
            {
                button.gameObject.SetActive(false);
            }

            var selectedBuilding = SelectedUnit as IBuilding;
            if (selectedBuilding != null)
            {
                RefreshBuilding(selectedBuilding);
            }
        }

        public void RefreshBuilding(IBuilding building)
        {
            _unitNameLabel.text = building.BuildingData.DisplayName;

            var unitsId = building.BuildingData.BuildableUnitList;
            int i = 0;
            foreach (var unitData in unitsId)
            {
                _gridButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = unitData.DisplayName;
                _gridButtons[i].gameObject.SetActive(true);
                i++;
            }
        }

    }
}
