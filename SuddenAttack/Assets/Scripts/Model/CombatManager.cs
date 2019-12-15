using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Controllers;

namespace SuddenAttack.Model
{
    public class DelayedDamage
    {
        public IUnit attacker;
        public IUnit attacked;
        public volatile float damage; // refactor
        public volatile float delay;
    }


    public class CombatManager
    {
        private Dictionary<IUnit, IUnit> _attackingUnits = new Dictionary<IUnit, IUnit>();
        private Dictionary<IUnit, List<DelayedDamage>> _delayedDamageMap = new Dictionary<IUnit, List<DelayedDamage>>();

        public void Damage(IUnit attacker, IUnit attacked, float damage, float delay)
        {
            var delayedDamage = new DelayedDamage();
            delayedDamage.damage = damage;
            delayedDamage.delay = delay;
            delayedDamage.attacked = attacked;
            delayedDamage.attacker = attacker;

            if (!_delayedDamageMap.ContainsKey(attacked))
            {
                _delayedDamageMap.Add(attacked, new List<DelayedDamage>());
            }

            _delayedDamageMap[attacked].Add(delayedDamage);
        }

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

        public void MoveUnit(IUnit unit, Vector3 destination)
        {
            unit.Prefab.GetComponent<UnitController>().SetDestination(destination);
        }

        public void StopUnit(IUnit unit)
        {
            unit.Prefab.GetComponent<UnitController>().SetDestination(unit.Prefab.transform.position);
        }

        public bool HasLock(IUnit attacker)
        {
            return _attackingUnits.ContainsKey(attacker);
        }

        public bool IsMoving(IUnit unit)
        {
            return unit.Prefab.GetComponent<UnitController>().IsMoving;
        }

        public void Update(float dt)
        {
            List<IUnit> clearList = new List<IUnit>();
            foreach (var pair in _attackingUnits)
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

            foreach (IUnit unit in clearList)
            {
                ClearAttacker(unit);
            }

            foreach (var delayedDamagePair in _delayedDamageMap)
            {
                List<DelayedDamage> killList = new List<DelayedDamage>();
                foreach (var delayedDamage in delayedDamagePair.Value)
                {
                    delayedDamage.delay -= dt;

                    if (delayedDamage.delay <= 0)
                    {
                        delayedDamage.attacked.Data.HitPoints -= delayedDamage.damage;
                        delayedDamage.attacker.Hit(delayedDamage.attacked);
                        killList.Add(delayedDamage);
                    }
                }

                foreach (var delayedDamage in killList)
                {
                    delayedDamagePair.Value.Remove(delayedDamage);
                }
            }

        }

        private float CalculateDamageDelay(IUnit attacker, IUnit attacked)
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
}