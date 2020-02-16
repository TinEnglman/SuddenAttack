using System.Collections;
using System.Collections.Generic;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Commands;
using UnityEngine;

public class BuildUnitCommand : BuildingCommandBase
{
    public int UnitIndex { get; set; }

    public override void Execute()
    {
        //((IBuilding) Unit).IsSpawning = true; // add index
    }
}
