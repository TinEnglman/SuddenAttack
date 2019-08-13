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
            var attacker = pair.Key;
            var attacked = pair.Value;

            if (attacker.CanFire())
            {
                attacker.Fire();
                float delay = CalculateDamageDelay(attacker, attacked);
                attacker.Damage(attacked, attacker.Data.Damage, delay);

            }
        }
    }

    public float CalculateDamageDelay(IUnit attacker, IUnit attacked)
    {
        float distance = (attacker.Prefab.transform.position - attacked.Prefab.transform.position).magnitude;
        if (attacker.Data.ProjectileSpeed > 0)
        {
            return distance / attacker.Data.ProjectileSpeed;
        }
        else
        {
            return 0;
        }
    }
}
