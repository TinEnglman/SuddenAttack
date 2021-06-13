using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;
using System.Collections.Generic;
namespace SuddenAttack.Model
{
    public class UnitCreationManager
    {
        private Dictionary<IBuilding, Queue<UnitCeation>> _currentBuildingTime = new Dictionary<IBuilding, Queue<UnitCeation>>();
        private UnitManager _unitManager;
        private MobileUnitFactoryManager _unitFactoryManager;

        public UnitCreationManager(UnitManager unitManager, MobileUnitFactoryManager unitCreationManager)
        {
            _unitManager = unitManager;
            _unitFactoryManager = unitCreationManager;
        }

        public float GetCompletePercent(IBuilding building)
        {
            if (!_currentBuildingTime.ContainsKey(building))
            {
                return 0f;
            }

            if (_currentBuildingTime[building].Count == 0)
            {
                return 0f;
            }

            return (_currentBuildingTime[building].Peek().UnitData.BuildDuration - _currentBuildingTime[building].Peek().BuildCooldown) / _currentBuildingTime[building].Peek().UnitData.BuildDuration;
        }

        public string GetCurrentlyBuiltUnitID(IBuilding building)
        {
            if (!_currentBuildingTime.ContainsKey(building))
            {
                return "";
            }


            if (_currentBuildingTime[building].Count == 0)
            {
                return "";
            }

            return _currentBuildingTime[building].Peek().UnitData.UnitId;
        }


        public int GetNumBuilding(IBuilding building)
        {
            if (!_currentBuildingTime.ContainsKey(building))
            {
                return 0;
            }

            return _currentBuildingTime[building].Count;
        }

        public void StartBuildingUnit(UnitData unitData, IBuilding building, int teamIndex)
        {
            _unitManager.Funds -= unitData.Cost;
            UnitCeation unitCreation = new UnitCeation(unitData, teamIndex);

            if (!_currentBuildingTime.ContainsKey(building))
            {
                _currentBuildingTime.Add(building, new Queue<UnitCeation>());
            }

            _currentBuildingTime[building].Enqueue(unitCreation);
        }

        public void CancelBuildingUnit(IBuilding building)
        {
            if (_currentBuildingTime[building].Count == 0)
            {
                return;
            }

            _unitManager.Funds += _currentBuildingTime[building].Peek().UnitData.Cost;
            _currentBuildingTime[building].Dequeue();
        }

        public void UpdateBuilding(float dt)
        {
            Dictionary<IBuilding, IMobileUnit> createdUnits = new Dictionary<IBuilding, IMobileUnit>();
            foreach (var pair in _currentBuildingTime)
            {
                IBuilding currentBuilding = pair.Key;

                if (pair.Value.Count == 0)
                {
                    continue;
                }

                UnitCeation unitCeation = pair.Value.Peek();
                _currentBuildingTime[currentBuilding].Peek().BuildCooldown -= dt;

                if (_currentBuildingTime[currentBuilding].Peek().BuildCooldown <= 0)
                {
                    float x = currentBuilding.Position.x + currentBuilding.SpawnOffset.x;
                    float y = currentBuilding.Position.y + currentBuilding.SpawnOffset.y;
                    IMobileUnit createdUnit = _unitFactoryManager.CreateUnit(unitCeation.UnitData.UnitId, x, y, unitCeation.TeamIndex);
                    createdUnits.Add(currentBuilding, createdUnit);
                }
            }

            foreach (var pair in createdUnits)
            {
                IBuilding building = pair.Key;
                IMobileUnit newUnit = pair.Value;

                _unitManager.AddMobileUnit(newUnit);
                _currentBuildingTime[building].Dequeue();
            }
        }
    }

    public class UnitCeation
    {
        public float BuildCooldown { get; set; }

        public UnitData UnitData { get; private set; }
        public int TeamIndex { get; private set; }

        public UnitCeation(UnitData unitData, int teamIndex)
        {
            UnitData = unitData;
            BuildCooldown = UnitData.BuildDuration;
            TeamIndex = teamIndex;
        }
    }
}