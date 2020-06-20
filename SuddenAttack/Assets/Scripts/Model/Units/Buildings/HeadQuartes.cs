using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Buildings
{
    public class HeadQuartes : AbstractBuilding
    {
        public HeadQuartes(BuildingData buildingData) : base()
        {
            _buildingData = buildingData;
            HitPoints = _buildingData.MaxHitPoints;
            //Data = ScriptableObject.CreateInstance<UnitData>();
            //Data.DisplayName = "Head Quarters";
            //Data.BuildDuration = 30;
            //Data.HitPoints = 1000;
            //Data.MaxHitPoints = 1000;
            //IsFriendly = isFriendly;
            //_currentCountdown = Data.BuildCooldown;

        }

        /*
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
        */

        public override void OnUpdate(float dt)
        {
        }

        public override void OnMove(Vector3 destination)
        {
        }

        public override void OnAttack(IUnit other)
        {
        }

        public override void OnFire()
        {
        }

        public override void OnHit(IUnit other)
        {
        }

        public override void OnStopAttacking()
        {
        }

        public override void OnStop()
        {
        }
    }
}