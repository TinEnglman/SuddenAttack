using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;
using System.Collections.Generic;
namespace SuddenAttack.Model
{
    public class UnitCreationManager
    {
        private Dictionary<IBuilding, Queue<UnitCeation>> _currentBuildingTime = new Dictionary<IBuilding, Queue<UnitCeation>>();
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
            return (building.BuildingData.BuildDuration - _currentBuildingTime[building].Peek().BuildCooldown) / building.BuildingData.BuildDuration;
        }

        public string GetCurrentlyBuiltUnitID(IBuilding building)
        {
            if (!_currentBuildingTime.ContainsKey(building))
            {
                return "";
            }

            return _currentBuildingTime[building].Peek().UnitID;
        }


        public int GetNumBuilding(IBuilding building)
        {
            if (!_currentBuildingTime.ContainsKey(building))
            {
                return 0;
            }

            return _currentBuildingTime[building].Count;
        }

        public void StartBuildingUnit(string UnitID, IBuilding building, int teamIndex)
        {
            UnitCeation unitCreation = new UnitCeation(building.BuildingData.BuildDuration, teamIndex, UnitID);

            if (!_currentBuildingTime.ContainsKey(building))
            {
                _currentBuildingTime.Add(building, new Queue<UnitCeation>());
            }

            _currentBuildingTime[building].Enqueue(unitCreation);
        }

        public void UpdateBuilding(float dt)
        {
            Dictionary<IBuilding, IMobileUnit> createdUnits = new Dictionary<IBuilding, IMobileUnit>();
            foreach (var pair in _currentBuildingTime)
            {
                IBuilding currentBuilding = pair.Key;
                UnitCeation unitCeation = pair.Value.Peek();
                _currentBuildingTime[currentBuilding].Peek().BuildCooldown -= dt;

                if (_currentBuildingTime[currentBuilding].Peek().BuildCooldown <= 0)
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
                _currentBuildingTime[building].Dequeue();
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