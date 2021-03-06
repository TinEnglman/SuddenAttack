﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDamage
{
    public ICombatUnit attacker;
    public IUnit attacked;
    public volatile float damage; // refactor
    public volatile float delay;
}


public class CombatManager
{
    private Dictionary<IUnit, IUnit> _attackingUnits = new Dictionary<IUnit, IUnit>();

    public void LockTarget(IUnit attacker, IUnit attacked)
    {
        _attackingUnits[attacker] = attacked;
    }

    public void ClearAttacker(IUnit attacker)
    {
        _attackingUnits.Remove(attacker);
        attacker.StopAttacking();
        attacker.IsUserLocked = false;
    }

    public bool HasLock(IUnit attacker)
    {
        return _attackingUnits.ContainsKey(attacker);
    }

    public void Update(float dt)
    {
        List<IUnit> clearList = new List<IUnit>();
        foreach (KeyValuePair<IUnit,IUnit> pair in _attackingUnits)
        {
            var attacker = pair.Key;
            var attacked = pair.Value;
            float distance = (attacker.Prefab.transform.position - attacked.Prefab.transform.position).magnitude;

            if (attacker.CanFire() && distance < attacker.Data.Range)
            {
                attacker.Stop();
                attacker.Fire();
                float delay = CalculateDamageDelay(attacker, attacked);
                attacker.Attack(attacked);
                attacker.Damage(attacked, attacker.Data.Damage, delay);
            }

            if (distance > attacker.Data.Range)
            {
                attacker.Move(attacked.Prefab.transform.position);
            }

            if (attacked.Data.HitPoints <= 0)
            {
                clearList.Add(attacker);
            }
        }

        foreach(IUnit unit in clearList)
        {
            ClearAttacker(unit);
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
