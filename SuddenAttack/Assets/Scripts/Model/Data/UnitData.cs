using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Data
{
    public class UnitData : ScriptableObject
    {
        public string DisplayName { get; set; }
        public float HitPoints { get; set; }
        public float MaxHitPoints { get; set; }
        public bool IsFriendly { get; set; }
        public float MoveSpeed { get; set; }
        public float WeaponCooldown { get; set; }
        public float Damage { get; set; }
        public float Range { get; set; }
        public float ProjectileSpeed { get; set; }
        public float EngageRange { get; set; }
        public int BuildCooldown { get; set; }
        public int Cost { get; set; }
    }
}