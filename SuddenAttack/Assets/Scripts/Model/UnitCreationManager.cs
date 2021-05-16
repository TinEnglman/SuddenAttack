using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;
using System.Collections.Generic;
namespace SuddenAttack.Model
{
    public class UnitCreationManager
    {
        private Dictionary<IBuilding, UnitCeation> _currentBuildingTime = new Dictionary<IBuilding, UnitCeation>();
        private UnitManager _gameManager;
        private UnitFactoryManager _unitFactoryManager;

        public UnitCreationManager(UnitManager gameManager, UnitFactoryManager unitCreationManager)
        {
            _gameManager = gameManager;
            _unitFactoryManager = unitCreationManager;
        }

        public float GetCompletePercent(IBuilding building)
        {
            if (!_currentBuildingTime.ContainsKey(building))
            {
                return 0f;
            }
            return (building.BuildingData.BuildDuration - _currentBuildingTime[building].BuildCooldown) / building.BuildingData.BuildDuration;
        }

        public bool IsBuilding(IBuilding building)
        {
            return _currentBuildingTime.ContainsKey(building);
        }

        public void StartBuildingUnit(string UnitID, IBuilding building, int teamIndex)
        {
            UnitCeation unitCreation = new UnitCeation(building.BuildingData.BuildDuration, teamIndex, UnitID);
            _currentBuildingTime.Add(building, unitCreation);
        }

        public void UpdateBuilding(float dt)
        {
            Dictionary<IBuilding, IMobileUnit> createdUnits = new Dictionary<IBuilding, IMobileUnit>();
            foreach (var pair in _currentBuildingTime)
            {
                IBuilding currentBuilding = pair.Key;
                UnitCeation unitCeation = pair.Value;
                _currentBuildingTime[currentBuilding].BuildCooldown -= dt;

                if (_currentBuildingTime[currentBuilding].BuildCooldown <= 0)
                {
                    float x = currentBuilding.Position.x + currentBuilding.SpawnOffset.x;
                    float y = currentBuilding.Position.y + currentBuilding.SpawnOffset.y;
                    IMobileUnit createdUnit = _unitFactoryManager.CreateUnit(unitCeation.UnitID, x, y, unitCeation.TeamIndex);
                    createdUnits.Add(currentBuilding, createdUnit);
                }
            }

            foreach (var pair in createdUnits)
            {
                IBuilding building = pair.Key;
                IMobileUnit newUnit = pair.Value;

                _gameManager.AddMobileUnit(newUnit);
                _currentBuildingTime.Remove(building);
            }
        }
    }

    public class UnitCeation
    {
        public float BuildCooldown { get; set; }
        public int TeamIndex { get; set; }
        public string UnitID { get; set; }

        public UnitCeation(float buildTime, int teamIndex, string unitID)
        {
            BuildCooldown = buildTime;
            TeamIndex = teamIndex;
            UnitID = unitID;
        }
    }
}