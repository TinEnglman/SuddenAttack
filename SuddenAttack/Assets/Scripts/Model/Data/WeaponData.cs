using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SuddenAttack.Model.Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Create Weapon")]
    public class WeaponData : ScriptableObject
    {
        public float WeaponCooldown;
        public float Damage;
        public float Range;
        public float ProjectileSpeed;
    }
}
