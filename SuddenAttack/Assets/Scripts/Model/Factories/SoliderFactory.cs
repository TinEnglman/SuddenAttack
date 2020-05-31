using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public class SoliderFactory : IUnitFactory
    {
        private UnitData _unitData;

        public SoliderFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        public Unit CreateUnit(float x, float y, bool isFriendly)
        {
            Vector3 position = new Vector3(x, y, 0);

            var solider = new Solider(_unitData)
            {
                Prefab = Object.Instantiate(_unitData.UnitPrefab) // todo: add unit transform
            };


            solider.IsFriendly = isFriendly;
            solider.Prefab.transform.SetPositionAndRotation(position, solider.Prefab.transform.rotation);
            var unitController = solider.Prefab.GetComponent<UnitController>();
            unitController.SetDestination(position);

            return solider;
        }

        public string GetDisplayName()
        {
            return _unitData.DisplayName; // refactor
        }

        public int GetCost() // refactor
        {
            return _unitData.Cost;
        }
    }
}
