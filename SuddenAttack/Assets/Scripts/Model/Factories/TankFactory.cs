using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactory : IUnitFactory
{
    public Unit CreateUnit(float x, float y)
    {
        return new Tank();
    }
}
