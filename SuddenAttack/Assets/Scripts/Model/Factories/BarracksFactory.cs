using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public class BarracksFactory : IBuildingFactory
    {
        private BuildingData _buildingData;

        public BarracksFactory(BuildingData buindingData)
        {
            _buildingData = buindingData;
        }

        public IBuilding CreateBuilding(float x, float y, bool isFriendly)
        {
            Vector3 position = new Vector3(x, y, 0);

            var headQuarters = new HeadQuartes(_buildingData)
            {
                Prefab = Object.Instantiate(_buildingData.BuildingPrefab) // todo: add unit transform
            };


            headQuarters.IsFriendly = isFriendly;
            headQuarters.Prefab.transform.SetPositionAndRotation(position, headQuarters.Prefab.transform.rotation);

            return headQuarters;
        }

        public string GetDisplayName()
        {
            return _buildingData.DisplayName; // refactor
        }

        public int GetCost() // refactor
        {
            return _buildingData.Cost;
        }
    }
}