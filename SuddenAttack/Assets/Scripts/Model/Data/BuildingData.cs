using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Create Building")]
    public class BuildingData : ScriptableObject
    {
        public GameObject BuildingPrefab;
        public WeaponData PrimaryWeapon;
        public string DisplayName;
        public float MaxHitPoints;
        public int BuildDuration;
        public int Cost;
    }
}