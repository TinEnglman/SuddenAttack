using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Buildings
{
    public class HeadQuartes : AbstractBuilding
    {
        public HeadQuartes(bool isFriendly)
        {
            Data = ScriptableObject.CreateInstance<UnitData>();
            Data.DisplayName = "Head Quarters";
            Data.BuildCooldown = 30;
            Data.HitPoints = 1000;
            Data.MaxHitPoints = Data.HitPoints;
            Data.IsFriendly = isFriendly;
            _currentCountdown = Data.BuildCooldown;

        }

        public override IUnit SpawnUnit()
        {
            float x = Prefab.transform.position.x + _spawnOffset.x + Random.value;
            float y = Prefab.transform.position.y + _spawnOffset.y + Random.value;
            IUnit unit = _unitFactory.CreateUnit(x, y, _unitPrefab, Data.IsFriendly);
            return unit;

        }

        public override int GetIncome()
        {
            return 25;
        }
    }
}