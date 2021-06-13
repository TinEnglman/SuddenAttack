using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Data;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public class BarracksFactory : BuildingFactoryBase
    {
        public BarracksFactory(BuildingData buindingData)
        {
            _buildingData = buindingData;
        }

        protected override IBuilding CreateBuildingInternal(Transform parentTransform)
        {
            var barracks = new Barracks(_buildingData)
            {
                Prefab = Object.Instantiate(_buildingData.BuildingPrefab, parentTransform)
            };

            return barracks;
        }

    }
}