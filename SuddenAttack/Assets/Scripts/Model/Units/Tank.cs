using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    public Tank()
    {
        _unitData = ScriptableObject.CreateInstance<UnitData>();

        _unitData.HitPoints = 100;
        _unitData.MoveSpeed = 2;
        _unitData.FireSpeed = 1;
        _unitData.Damage = 25;
        _unitData.Range = 8;
    }
}
