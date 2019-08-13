using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : ScriptableObject
{
    public float HitPoints { get; set; }
    public float MoveSpeed { get; set; }
    public float WeaponCooldown { get; set; }
    public float Damage { get; set; }
    public float Range { get; set; }
    public float ProjectileSpeed { get; set; }
}
