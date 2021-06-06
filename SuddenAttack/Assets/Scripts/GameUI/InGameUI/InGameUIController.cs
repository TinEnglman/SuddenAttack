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
        private Button _cancleBuildingButton = default;
        [SerializeField]
        private TextMeshProUGUI _fundLabel = default;
        [SerializeField]
        private TextMeshProUGUI _unitNameLabel = default;

        [SerializeField]
        private Image _unitPortrait = default;
        [SerializeField]
        private GameObject _currentlyBuiltParent = default;
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
        private UIManager _uIManager;

        public IUnit SelectedUnit { get; set; }

        [Inject]
        public void Construct(UIManager uiManager, UnitCreationManager unitCreationManager, UnitManager unitManager)
        {
            _unitManager = unitManager;
            _unitCreationManager = unitCreationManager;
            _uIManager = uiManager;
        }

        public void OnMainMenuButton() // linked to button in editor
        {
            _uIManager.GameMenu.ShowGameMenu();
        }

        public void Refresh()
        {
            if (SelectedUnit == null)
            {
                foreach (var button in _gridButtons)
                {
                    button.gameObject.SetActive(false);
                }

                _currentlyBuiltParent.SetActive(false);
                _unitPortrait.enabled = false;
                _unitNameLabel.enabled = false;
            }

            var selectedBuilding = SelectedUnit as IBuilding;
            if (selectedBuilding != null)
            {
                RefreshBuilding(selectedBuilding);
            }

            var selectedMobileUnit = SelectedUnit as IMobileUnit;
            if (selectedMobileUnit != null)
            {
                RefreshMobieUnit(selectedMobileUnit);
            }

            RefreshFunds();
        }

        public void RefreshFunds()
        {
            _fundLabel.text = "Funds: " + _unitManager.Funds + " $";
        }

        public void RefreshMobieUnit(IMobileUnit moblieUnit)
        {
            _unitNameLabel.text = moblieUnit.Data.DisplayName;
            _unitPortrait.enabled = true;
            _unitNameLabel.enabled = true;
        }

        public void RefreshBuilding(IBuilding building)
        {
            _unitNameLabel.text = building.BuildingData.DisplayName;
            _currentlyBuiltParent.SetActive(true);
            _unitPortrait.enabled = true; // todo: replace with actuall potrrait of the unit
            _unitNameLabel.enabled = true;

            var unitsId = building.BuildingData.BuildableUnitList;
            int i = 0;
            foreach (var unitData in unitsId)
            {
                _gridButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = unitData.DisplayName + "\n" + unitData.Cost.ToString(); ;
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
            if (_unitManager.Funds < unitData.Cost)
            {
                return;
            }

            _unitCreationManager.StartBuildingUnit(unitData, building, building.TeamIndex);
            _currentlyBuiltLabel.enabled = true;
            _currentlyBuiltNumberLabel.enabled = true;
            RefreshFunds();
        }

        public void CancelBuilding()
        {
            var selectedBuilding = SelectedUnit as IBuilding;

            if (selectedBuilding == null)
            {
                return;
            }

            _unitCreationManager.CancelBuildingUnit(selectedBuilding);
            RefreshFunds();
        }


        public float GetScreenWidth()
        {
            return _background.sizeDelta.x;
        }

        private void Awake()
        {
            _currentlyBuiltLabel.enabled = false;
            _currentlyBuiltNumberLabel.enabled = false;
            _currentlyBuiltParent.SetActive(false);
            _cancleBuildingButton.onClick.AddListener(CancelBuilding);
            Refresh();
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
