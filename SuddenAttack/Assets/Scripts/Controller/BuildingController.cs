using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private IBuilding _building = null; // unnecessary?

    public IBuilding Building
    {
        get { return _building; }
        set { _building = value; }
    }
}
