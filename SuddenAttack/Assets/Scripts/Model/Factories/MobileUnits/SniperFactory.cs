using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public class SniperFactory : MobileUnitFactoryBase
    {
        public SniperFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        protected override IMobileUnit CreateUnitInternal(Transform parentTransform)
        {
            var sniper = new Sniper(_unitData)
            {
                Prefab = Object.Instantiate(_unitData.UnitPrefab, parentTransform)
            };

            var unitController = sniper.Prefab.GetComponent<UnitController>();
            unitController.Unit = sniper;

            return sniper;
        }
    }
}
