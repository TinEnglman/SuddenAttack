using UnityEngine;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public class TankFactory : MobileUnitFactoryBase
    {
        public TankFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        protected override IMobileUnit CreateUnitInternal(Transform parentTransform)
        {
            var tank = new Tank(_unitData)
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