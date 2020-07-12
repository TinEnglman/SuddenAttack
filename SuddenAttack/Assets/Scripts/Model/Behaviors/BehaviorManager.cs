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

        public void SetBehavior(IUnit unit, IBehavior behavior)
        {
            _activeBehaviorDict.Add(unit, behavior);
            behavior.OnBegin(unit);
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
                    behavior.OnEnd(unit);
                    _killList.Add(unit);
                }
            }

            foreach(var unit in _killList)
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