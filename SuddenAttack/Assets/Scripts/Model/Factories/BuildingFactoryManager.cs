using SuddenAttack.Model.Data;
using System.Collections;
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
            //_unitFactories.Add("Sniper", new SniperFactory(_soliderData));
        }

        void Start()
        {
            Init();
        }
    }
}