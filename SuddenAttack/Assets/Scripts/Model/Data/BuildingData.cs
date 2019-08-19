using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : ScriptableObject
{
    public int HitPoints { get; set; }
    public bool IsFriendly { get; set; }
    public int BuildCooldown { get; set; }
}
