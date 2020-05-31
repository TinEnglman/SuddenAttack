using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Create Unit")]
    public class UnitData : ScriptableObject
    {
        public string UnitId;
        public GameObject UnitPrefab;
        public WeaponData PrimaryWeapon;
        public string DisplayName;
        public float MaxHitPoints;
        public float MoveSpeed;
        public float EngageRange;
        public int BuildDuration;
        public int Cost;
    }
}