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

            var barracks = new Barracks(_buildingData)
            {
                Prefab = Object.Instantiate(_buildingData.BuildingPrefab) // todo: add unit transform
            };


            barracks.IsFriendly = isFriendly;
            barracks.Prefab.transform.SetPositionAndRotation(position, barracks.Prefab.transform.rotation);
            barracks.Position = position;

            return barracks;
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