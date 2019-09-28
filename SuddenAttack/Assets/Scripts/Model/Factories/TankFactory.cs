using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controllers;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Factories
{
    public class TankFactory : IUnitFactory
    {
        private int _cost;

        public Unit CreateUnit(float x, float y, GameObject prefab, bool isFriendly)
        {
            Vector3 position = new Vector3(x, y, 0);
            var tank = new Tank
            {
                Prefab = Object.Instantiate(prefab)
            };

            _cost = 40;
            tank.Data.Cost = _cost;
            tank.Data.IsFriendly = isFriendly;
            tank.Prefab.transform.SetPositionAndRotation(position, tank.Prefab.transform.rotation);
            tank.Prefab.GetComponent<UnitController>().SetDestination(position);
            tank.Prefab.GetComponent<BulletContoller>().enabled = false;
            tank.Prefab.GetComponent<BulletContoller>().ProjectileOrigin = tank.Prefab.transform.position;

            return tank;
        }

        public string GetDisplayName()
        {
            return "Tank"; // refactor
        }

        public int GetCost()
        {
            return _cost;
        }
    }
}