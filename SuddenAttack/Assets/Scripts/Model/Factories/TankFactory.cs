using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public class TankFactory : UnitFactoryBase
    {
        public TankFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        protected override IMobileUnit CreateUnitInternal(UnitData unitData, Transform parentTransform)
        {
            var tank = new Solider(_unitData)
            {
                Prefab = Object.Instantiate(_unitData.UnitPrefab, parentTransform)
            };

            tank.Prefab.GetComponent<UnitController>().Unit = tank;
            tank.Prefab.GetComponent<BulletContoller>().enabled = false;
            tank.Prefab.GetComponent<BulletContoller>().ProjectileOrigin = tank.Prefab.transform.position;

            return tank;
        }
    }
}