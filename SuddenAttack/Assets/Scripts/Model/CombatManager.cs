using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{
    private Dictionary<IUnit, IUnit> _attackingUnits = new Dictionary<IUnit, IUnit>();

    public void LockTarget(IUnit attacker, IUnit attacked)
    {
        _attackingUnits[attacker] = attacked;
        attacker.Attack(attacked);
    }

    public void ClearTarget(IUnit attacker)
    {
        _attackingUnits[attacker] = null;
    }

    public void Update(float dt)
    {
        foreach(KeyValuePair<IUnit,IUnit> pair in _attackingUnits)
        {
            var attaker = pair.Key;
            var attaked = pair.Value;

            float attackerFireSpeed = attaker.Data.FireSpeed;

            if (attaker.CanFire())
            { 
                pair.Key.Fire(pair.Value);
            }
        }
    }
}
