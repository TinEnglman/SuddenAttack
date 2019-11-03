using UnityEngine;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Gui;

namespace SuddenAttack.Controllers
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _heltBarOverlay = null;
        private float _currentHelth = 1;

        private void Awake()
        {
            _selectionCircleController = GetComponentInChildren<SelectionCircleController>(); // create dependency
        }

        private SelectionCircleController _selectionCircleController = null;
        public float CurrentHelth
        {
            set { _currentHelth = value; }
        }

        private IBuilding _building = null;

        public IBuilding Building
        {
            get { return _building; }
            set { _building = value; }
        }

        private void Update()
        {
            var originalScale = _heltBarOverlay.transform.localScale;

            float maxScale = 1f;
            float newScale = _currentHelth * maxScale;
            _heltBarOverlay.transform.localScale = new Vector3(newScale, maxScale, maxScale);
        }

        public void Select()
        {
            _selectionCircleController.Selectct();
        }

        public void Deselect()
        {
            _selectionCircleController.Deselectct();
        }
    }
}