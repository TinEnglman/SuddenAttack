using UnityEngine;
using SuddenAttack.Model.Buildings;
using SuddenAttack.GameUI;

namespace SuddenAttack.Controller.ViewController.Buildings
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _heltBarOverlay = default;

        private SelectionCircleController _selectionCircleController;
        private float _maxScale;

        public IBuilding Building { get; set; }

        public void Select()
        {
            _selectionCircleController.Selectct();
        }

        public void Deselect()
        {
            _selectionCircleController.Deselectct();
        }

        private void Awake()
        {
            _maxScale = _heltBarOverlay.transform.localScale.x;
            _selectionCircleController = GetComponentInChildren<SelectionCircleController>(); // create dependency
        }

        private void Update()
        {
            float newScale = (Building.HitPoints / Building.BuildingData.MaxHitPoints) * _maxScale;
            _heltBarOverlay.transform.localScale = new Vector3(newScale, _maxScale, _maxScale);
        }
    }
}