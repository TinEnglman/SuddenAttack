using SuddenAttack.Controller.ViewController.Buildings;
using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Data;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public abstract class BuildingFactoryBase : IBuildingFactory
    {
        protected BuildingData _buildingData;

        public IBuilding CreateBuilding(float x, float y, int teamIndex, Transform parentTransform)
        {
            Vector3 position = new Vector3(x, y, 0);

            var building = CreateBuildingInternal(parentTransform);

            building.TeamIndex = teamIndex;
            building.Prefab.transform.SetPositionAndRotation(position, building.Prefab.transform.rotation);
            building.Position = position;
            var buildingController = building.Prefab.GetComponent<BuildingController>();
            buildingController.Building = building;

            return building;
        }

        protected abstract IBuilding CreateBuildingInternal(Transform prarentTransform);

        public string GetDisplayName()
        {
            return _buildingData.DisplayName;
        }

        public int GetCost()
        {
            return _buildingData.Cost;
        }
    }
}