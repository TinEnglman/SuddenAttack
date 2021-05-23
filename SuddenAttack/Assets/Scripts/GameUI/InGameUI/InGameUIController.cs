using SuddenAttack.Model;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Data;
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
        [SerializeField]
        private TextMeshProUGUI _currentlyBuiltLabel = default;
        [SerializeField]
        private TextMeshProUGUI _currentlyBuiltNumberLabel = default;
        [SerializeField]
        RectTransform _background = default;
        [SerializeField]
        private Slider _completedSlider = null;

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
                _gridButtons[i].onClick.RemoveAllListeners();
                _gridButtons[i].onClick.AddListener(
                    () =>
                    {
                        StartBuilding(building, unitData);
                    });

                _gridButtons[i].gameObject.SetActive(true);
                i++;
            }
        }

        public void StartBuilding(IBuilding building, UnitData unitData)
        {
            _unitCreationManager.StartBuildingUnit(unitData.UnitId, building, building.TeamIndex);
            _currentlyBuiltLabel.enabled = true;
            _currentlyBuiltNumberLabel.enabled = true;
        }

        public float GetScreenWidth()
        {
            return _background.sizeDelta.x;
        }

        private void Awake()
        {
            _currentlyBuiltLabel.enabled = false;
            _currentlyBuiltNumberLabel.enabled = false;
        }

        private void Update()
        {
            var selectedBuilding = SelectedUnit as IBuilding;

            if (selectedBuilding != null)
            {
                _completedSlider.normalizedValue = _unitCreationManager.GetCompletePercent(selectedBuilding);
                _currentlyBuiltLabel.text = _unitCreationManager.GetCurrentlyBuiltUnitID(selectedBuilding);
                _currentlyBuiltNumberLabel.text = _unitCreationManager.GetNumBuilding(selectedBuilding).ToString();
            }
        }
    }
}
