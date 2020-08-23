using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public class SniperFactory : IUnitFactory
    {
        private UnitData _unitData;

        public SniperFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        public IMobileUnit CreateUnit(float x, float y, bool isFriendly)
        {
            Vector3 position = new Vector3(x, y, 0);

            var sniper = new Sniper(_unitData)
            {
                Prefab = Object.Instantiate(_unitData.UnitPrefab) // todo: add unit transform
            };


            sniper.Position = position;
            sniper.IsFriendly = isFriendly;
            sniper.WeaponData = _unitData.PrimaryWeapon; // need base class
            sniper.HitPoints = _unitData.MaxHitPoints;
            sniper.Prefab.transform.SetPositionAndRotation(position, sniper.Prefab.transform.rotation);
            var unitController = sniper.Prefab.GetComponent<UnitController>();
            unitController.Unit = sniper;

            return sniper;
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
