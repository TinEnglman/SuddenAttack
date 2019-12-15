using UnityEngine;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Gui;

namespace SuddenAttack.Controller.ViewController
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _heltBarOverlay = null;
        private SelectionCircleController _selectionCircleController = null;
        private float _currentHealth = 1;

        public IBuilding Building { get; set; }

        public void Select()
        {
            _selectionCircleController.Selectct();
        }

        public void Deselect()
        {
            _selectionCircleController.Deselectct();
        }

        public float CurrentHelth
        {
            set { _currentHealth = value; }
        }

        private void Awake()
        {
            _selectionCircleController = GetComponentInChildren<SelectionCircleController>(); // create dependency
        }

        private void Update()
        {
            var originalScale = _heltBarOverlay.transform.localScale;

            float maxScale = 1f;
            float newScale = _currentHealth * maxScale;
            _heltBarOverlay.transform.localScale = new Vector3(newScale, maxScale, maxScale);
        }
    }
}