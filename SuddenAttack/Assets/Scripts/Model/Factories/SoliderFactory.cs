using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderFactory : IUnitFactory
{
    public Unit CreateUnit(float x, float y, GameObject prefab)
    {
        var solider = new Solider
        {
            Prefab = Object.Instantiate(prefab)
        };
        return solider;
    }
}
