using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Data;

namespace SuddenAttack.Model.Factories
{
    public class TankFactory : IUnitFactory
    {
        private UnitData _unitData = default;

        public TankFactory(UnitData unitData)
        {
            _unitData = unitData;
        }

        public IUnit CreateUnit(float x, float y, bool isFriendly)
        {
            Vector3 position = new Vector3(x, y, 0);
            var tank = new Tank()
            {
                Prefab = Object.Instantiate(_unitData.UnitPrefab)
            };


            tank.IsFriendly = isFriendly;
            tank.Prefab.transform.SetPositionAndRotation(position, tank.Prefab.transform.rotation);
            tank.Prefab.GetComponent<UnitController>().SetDestination(position);
            tank.Prefab.GetComponent<BulletContoller>().enabled = false;
            tank.Prefab.GetComponent<BulletContoller>().ProjectileOrigin = tank.Prefab.transform.position;

            return tank;
        }

        public string GetDisplayName()
        {
            return _unitData.DisplayName; // refactor
        }

        public int GetCost()
        {
            return _unitData.Cost;
        }
    }
}