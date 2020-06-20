using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Data;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{
    public class BuildingFactoryManager : MonoBehaviour
    {
        [SerializeField] private BuildingData _hqData = default;
        [SerializeField] private BuildingData _barracksData = default;

        private Dictionary<string, IBuildingFactory> _buildingFactories = new Dictionary<string, IBuildingFactory>();

        private void Init()
        {
            _buildingFactories.Clear();
            _buildingFactories.Add("HQ", new HQFactory(_hqData));
            _buildingFactories.Add("Barracks", new BarracksFactory(_barracksData));
        }

        private void Start()
        {
            Init();
        }

        public IBuilding CreateBuilding(float x, float y, string buildingId, bool isFriendly)
        {
            if (!_buildingFactories.ContainsKey(buildingId))
            {
                return null; /// todo error msg
            }

            return _buildingFactories[buildingId].CreateBuilding(x, y, isFriendly);
        }
    }
}