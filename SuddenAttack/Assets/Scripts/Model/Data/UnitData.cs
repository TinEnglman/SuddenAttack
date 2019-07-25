using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : ScriptableObject
{
    public int HitPoints { get; set; }
    public float MoveSpeed { get; set; }
    public float FireSpeed { get; set; }
    public float Damage { get; set; }
}
