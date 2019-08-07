using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderFactory : IUnitFactory
{
    public Unit CreateUnit(float x, float y, GameObject prefab)
    {
        Vector3 position = new Vector3(x, y, 0);
        var solider = new Solider
        {
            Prefab = Object.Instantiate(prefab)
        };

        solider.Prefab.transform.SetPositionAndRotation(position, solider.Prefab.transform.rotation);
        solider.Prefab.GetComponent<UnitController>().SetDestination(position);

        return solider;
    }
}
