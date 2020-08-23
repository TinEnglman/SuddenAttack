using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Controller.ViewController;

namespace SuddenAttack.Model
{
    public class DelayedDamage
    {
        public IUnit attacker;
        public IUnit attacked;
        public volatile float damage;
        public volatile float delay;
    }

    public class CombatManager
    {
        private Dictionary<IUnit, List<DelayedDamage>> _delayedDamageMap = new Dictionary<IUnit, List<DelayedDamage>>();

        public void Damage(IUnit attacker, IUnit attacked)
        {
            var delayedDamage = new DelayedDamage();
            delayedDamage.damage = attacker.WeaponData.Damage;
            delayedDamage.delay = CalculateDamageDelay(attacker, attacked);
            delayedDamage.attacked = attacked;
            delayedDamage.attacker = attacker;

            if (!_delayedDamageMap.ContainsKey(attacked))
            {
                _delayedDamageMap.Add(attacked, new List<DelayedDamage>());
            }

            _delayedDamageMap[attacked].Add(delayedDamage);
        }


        public void Update(float dt)
        {

            foreach (var delayedDamagePair in _delayedDamageMap)
            {
                List<DelayedDamage> killList = new List<DelayedDamage>();
                foreach (var delayedDamage in delayedDamagePair.Value)
                {
                    delayedDamage.delay -= dt;

                    if (delayedDamage.delay <= 0)
                    {
                        delayedDamage.attacked.HitPoints -= delayedDamage.damage;
                        delayedDamage.attacker.OnHit(delayedDamage.attacked);
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
            if (attacker.WeaponData.ProjectileSpeed > 0)
            {
                return distance / attacker.WeaponData.ProjectileSpeed;
            }
            else
            {
                return 0;
            }
        }
    }
}