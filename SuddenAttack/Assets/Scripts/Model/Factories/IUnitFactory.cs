using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitFactory
{
    Unit CreateUnit(float x, float y, GameObject prefab);
}
