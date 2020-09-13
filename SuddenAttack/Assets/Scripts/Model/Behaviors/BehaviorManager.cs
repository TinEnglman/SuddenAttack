using SuddenAttack.Model.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{
    public class BehaviorManager
    {
        private Dictionary<IUnit, IBehavior> _activeBehaviorDict = new Dictionary<IUnit, IBehavior>();
        private List<IUnit> _killList = new List<IUnit>();

        private CommandManager _commandManager;

        public BehaviorManager(CommandManager commandManager)
        {
            _commandManager = commandManager;
        }

        public void SetBehavior(IUnit unit, IBehavior behavior)
        {
            if (_activeBehaviorDict.ContainsKey(unit))
            {
                StopBehavior(unit);
                UpdateBehaviors(0);
            }

            _activeBehaviorDict.Add(unit, behavior);
            behavior.OnBegin(unit);
        }

        public void StopBehavior(IUnit unit)
        {
            var behavior = _activeBehaviorDict[unit];
            behavior.OnEnd(unit);
            _killList.Add(unit);
            _commandManager.PopQueuedCommand(unit);
        }

        public void UpdateBehaviors(float dt)
        {
            foreach (var pair in _activeBehaviorDict)
            {
                var unit = pair.Key;
                var behavior = pair.Value;

                behavior.Update(unit, dt);

                if (behavior.IsFinished(unit))
                {
                    StopBehavior(unit);
                }
            }

            foreach (var unit in _killList)
            {
                _activeBehaviorDict.Remove(unit);
            }

            if (_killList.Count > 0)
            {
                _killList.Clear();
            }
        }

 

    }
}