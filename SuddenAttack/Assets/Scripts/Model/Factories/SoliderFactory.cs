using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderFactory : IUnitFactory
{
    public Unit CreateUnit()
    {
        return new Solider();
    }
}
