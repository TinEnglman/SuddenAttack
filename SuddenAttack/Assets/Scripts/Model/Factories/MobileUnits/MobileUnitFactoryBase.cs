using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public abstract class MobileUnitFactoryBase : IMobileUnitFactory
    {
        protected UnitData _unitData;

        public IMobileUnit CreateUnit(float x, float y, int teamIndex, Transform parentTransform)
        {
            Vector3 position = new Vector3(x, y, 0);

            var unit = CreateUnitInternal(parentTransform);

            unit.Position = position;
            unit.TeamIndex = teamIndex;
            unit.WeaponData = _unitData.PrimaryWeapon; // need base class
            unit.WeaponCooldown = 0;
            unit.HitPoints = _unitData.MaxHitPoints;
            unit.Prefab.transform.SetPositionAndRotation(position, unit.Prefab.transform.rotation);

            return unit;
        }

        protected abstract IMobileUnit CreateUnitInternal(Transform prarentTransform);

        public string GetDisplayName()
        {
            return _unitData.DisplayName;
        }

        public int GetCost()
        {
            return _unitData.Cost;
        }
    }
}
