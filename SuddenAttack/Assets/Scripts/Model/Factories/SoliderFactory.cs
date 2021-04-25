using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public class SoliderFactory : UnitFactoryBase
    {
        public SoliderFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        protected override IMobileUnit CreateUnitInternal(UnitData unitData, Transform parentTransform)
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
