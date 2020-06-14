using SuddenAttack.Model.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Units
{
    public interface IMobileUnit : IUnit
    {
        UnitData Data { get; set; }
    }
}