using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Buildings
{
    public class Barracks : AbstractBuilding
    {
        public Barracks(BuildingData buildingData) : base ()
        {

            _buildingData = buildingData;
            HitPoints = _buildingData.MaxHitPoints;
            //Data = ScriptableObject.CreateInstance<UnitData>();
            //Data.DisplayName = "Barracks";
            //Data.BuildDuration = 10;
            //Data.MaxHitPoints = 800;
            //IsFriendly = isFriendly;
            //_currentCountdown = Data.BuildCooldown;
        }


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
            return 0;
        }
        */

    }
}