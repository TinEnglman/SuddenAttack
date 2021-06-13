using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public class HQFactory : BuildingFactoryBase
    {
        public HQFactory(BuildingData buindingData)
        {
            _buildingData = buindingData;
        }

        protected override IBuilding CreateBuildingInternal(Transform parentTransform)
        {
            var headQuarters = new HeadQuartes(_buildingData)
            {
                Prefab = Object.Instantiate(_buildingData.BuildingPrefab, parentTransform)
            };

            return headQuarters;
        }
    }
}
