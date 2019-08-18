using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactory : IUnitFactory
{
    public Unit CreateUnit(float x, float y, GameObject prefab, bool isFriendly)
    {
        Vector3 position = new Vector3(x, y, 0);
        var tank = new Tank
        {
            Prefab = Object.Instantiate(prefab)
        };

        tank.Data.IsFriendly = isFriendly;
        tank.Prefab.transform.SetPositionAndRotation(position, tank.Prefab.transform.rotation);
        tank.Prefab.GetComponent<UnitController>().SetDestination(position);
        tank.Prefab.GetComponent<BulletContoller>().enabled = false;
        tank.Prefab.GetComponent<BulletContoller>().ProjectileOrigin = tank.Prefab.transform.position;

        return tank;
    }
}
