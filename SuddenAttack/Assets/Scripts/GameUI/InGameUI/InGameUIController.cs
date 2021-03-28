using SuddenAttack.Model;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuddenAttack.Controller.GameUI
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField]
        private List<Button> _gridButtons;
        [SerializeField]
        private TextMeshProUGUI _foundLabel = null;
        [SerializeField]
        private TextMeshProUGUI _unitNameLabel = null;

        private UnitCreationManager _unitCreationManager;
        private UnitManager _unitManager;

        public IUnit SelectedUnit { get; set; }

        [Inject]
        public void Construct(UnitCreationManager unitCreationManager, UnitManager unitManager)
        {
            _unitManager = unitManager;
            _unitCreationManager = unitCreationManager;
        }

        public void Refresh() // called form where?
        {
            foreach (var button in _gridButtons)
            {
                button.gameObject.SetActive(false);
            }

            var selectedBuilding = SelectedUnit as IBuilding;
            if (selectedBuilding != null)
            {
                var unitsId = selectedBuilding.UnitIds;
                int i = 0;
                foreach (var id in unitsId)
                {
                    _gridButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = id;
                    _gridButtons[i].gameObject.SetActive(true);
                }
            }

            
        }

    }
}
