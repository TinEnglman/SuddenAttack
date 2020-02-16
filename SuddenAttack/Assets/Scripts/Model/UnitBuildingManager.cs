using SuddenAttack.Model.Buildings;
using SuddenAttack.Model.Units;
using System.Collections.Generic;
namespace SuddenAttack.Model
{
    public class UnitBuildingManager
    {
        private Dictionary<IBuilding, UnitCeation> _currentBuildingTime = null;
        private GameManager _gameManager = default;

        public UnitBuildingManager(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public float GetCompletePercent(IBuilding building)
        {
            return _currentBuildingTime[building].BuildCooldown / building.Data.BuildDuration;
        }

        public bool IsBuilding(IBuilding building)
        {
            return _currentBuildingTime.ContainsKey(building);
        }

        public void StartBuildingUnit(IBuilding building, bool isFriendly)
        {
            UnitCeation unitCreation = new UnitCeation(building.Data.BuildDuration, isFriendly);
            _currentBuildingTime.Add(building, unitCreation);
        }

        public void UpdateBuilding(float dt)
        {
            Dictionary<IBuilding, IUnit> createdUnits = new Dictionary<IBuilding, IUnit>();
            foreach (var pair in _currentBuildingTime)
            {
                IBuilding currentBuilding = pair.Key;
                _currentBuildingTime[currentBuilding].BuildCooldown -= dt;

                if (_currentBuildingTime[currentBuilding].BuildCooldown <= 0)
                {
                    float x = currentBuilding.SpawnOffset.x;
                    float y = currentBuilding.SpawnOffset.y;
                    IUnit createdUnit = currentBuilding.GetFactory().CreateUnit(x, y, currentBuilding.Prefab, true);
                    createdUnits.Add(currentBuilding, createdUnit);
                }
            }

            foreach (var pair in createdUnits)
            {
                IBuilding building = pair.Key;
                IUnit newUnit = pair.Value;

                _gameManager.AddUnit(newUnit);
                _currentBuildingTime.Remove(building);
            }
        }
    }

    public class UnitCeation
    {
        public float BuildCooldown { get; set; }
        public bool IsFriendly { get; set; } // refactor to playerID or smt similar

        public UnitCeation(float buildTime, bool isFriendly)
        {
            BuildCooldown = buildTime;
            IsFriendly = isFriendly;
        }
    }
}