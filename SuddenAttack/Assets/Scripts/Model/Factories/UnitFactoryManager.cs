using SuddenAttack.Model.Data;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{ 
    public class UnitFactoryManager : MonoBehaviour
    {
        [SerializeField] private UnitData _soliderData = default;
        [SerializeField] private UnitData _tankData = default;
        [SerializeField] private UnitData _sniperData = default;

        private Dictionary<string, IUnitFactory> _unitFactories = new Dictionary<string, IUnitFactory>();

        public IUnit CreateUnit(string unitId, float x, float y, bool isFriendly)
        {
            return _unitFactories[unitId].CreateUnit(x, y, isFriendly);
        }

        private void Init()
        {
            _unitFactories.Clear();
            _unitFactories.Add("Solider", new SoliderFactory(_soliderData));
            _unitFactories.Add("Tank", new TankFactory(_tankData));
            //_unitFactories.Add("Sniper", new SniperFactory(_soliderData));
        }

        private void Start()
        {
            Init();
        }
    }
}