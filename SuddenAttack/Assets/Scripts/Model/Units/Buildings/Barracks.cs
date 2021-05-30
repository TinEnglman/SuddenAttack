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
        }


        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
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