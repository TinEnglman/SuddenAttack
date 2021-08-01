using UnityEngine;
using SuddenAttack.Controller.ViewController.Units;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public class SoliderFactory : MobileUnitFactoryBase
    {
        public SoliderFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        protected override IMobileUnit CreateUnitInternal(Transform parentTransform)
        {
            var solider = new Solider(_unitData)
            {
                Prefab = Object.Instantiate(_unitData.UnitPrefab, parentTransform)
            };

    
            var unitController = solider.Prefab.GetComponent<UnitController>();
            unitController.Unit = solider;

            return solider;
        }
    }
}
