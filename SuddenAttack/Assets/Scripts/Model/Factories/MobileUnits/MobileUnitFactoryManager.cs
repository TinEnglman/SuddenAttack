using SuddenAttack.Model.Data;
using SuddenAttack.Model.Units;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Factories
{ 
    public class MobileUnitFactoryManager : MonoBehaviour
    {
        [SerializeField] private Transform _buildingsParentTransfrom = default;

        [SerializeField] private UnitData _soliderData = default;
        [SerializeField] private UnitData _tankData = default;
        [SerializeField] private UnitData _sniperData = default;
        [SerializeField] private UnitData _panzetData = default;

        private Dictionary<string, IMobileUnitFactory> _unitFactories = new Dictionary<string, IMobileUnitFactory>();

        public void Setup()
        {
            _unitFactories.Clear();
            _unitFactories.Add("Solider", new SoliderFactory(_soliderData));
            _unitFactories.Add("Tank", new TankFactory(_tankData));
            _unitFactories.Add("Sniper", new SniperFactory(_sniperData));
            _unitFactories.Add("Panzer", new TankFactory(_panzetData));
        }

        public IMobileUnit CreateUnit(string unitId, float x, float y, int teamIndex)
        {
            if (!_unitFactories.ContainsKey(unitId))
            {
                return null; /// todo error msg
            }

            return _unitFactories[unitId].CreateUnit(x, y, teamIndex, _buildingsParentTransfrom);
        }
    }
}