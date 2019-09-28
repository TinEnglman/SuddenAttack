using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controllers;

namespace SuddenAttack.Model.Units
{
    public abstract class Unit : IUnit
    {
        protected UnitData _unitData;
        protected GameObject _prefab;
        protected float _weaponCooldown = 0.0f;
        protected bool _isAttacking = false;
        protected bool _canFire = true;
        protected List<DelayedDamage> _receavedDamage = new List<DelayedDamage>();
        private bool _isUserLocked = false;

        public Unit()
        {
        }

        public GameObject Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }

        public UnitData Data
        {
            get { return _unitData; }
            set { _unitData = value; }
        }

        public bool IsBuilding()
        {
            return false;
        }

        public void Move(Vector3 destination)
        {
            _prefab.GetComponent<UnitController>().SetDestination(destination);
        }

        public void Stop()
        {
            _prefab.GetComponent<UnitController>().SetDestination(_prefab.transform.position);
        }

        public void Select()
        {
            _prefab.GetComponent<UnitController>().Select();
        }

        public void Deselect()
        {
            _prefab.GetComponent<UnitController>().Deselect();
        }

        public bool IsMoving
        {
            get { return _prefab.GetComponent<UnitController>().IsMoving; }
        }

        public bool IsUserLocked
        {
            get { return _isUserLocked; }
            set { _isUserLocked = value; }
        }

        public bool CanFire()
        {
            return _canFire;
        }

        public void Fire()
        {
            _canFire = false;
        }

        public void Die()
        {
            var bulletController = Prefab.GetComponentInChildren<BulletContoller>();
            if (bulletController != null)
            {
                bulletController.gameObject.SetActive(false);
            }
            Prefab.SetActive(false);
        }

        public void Update()
        {
            if (_isAttacking || !_canFire)
            {
                _weaponCooldown -= Time.deltaTime;

                if (_weaponCooldown <= 0)
                {
                    _weaponCooldown += _unitData.WeaponCooldown;
                    _canFire = true;
                }
            }

            List<int> killList = new List<int>();
            for (int i = 0; i < _receavedDamage.Count; i++)
            {
                _receavedDamage[i].delay -= Time.deltaTime;

                if (_receavedDamage[i].delay <= 0)
                {
                    _receavedDamage[i].attacked.Data.HitPoints -= Data.Damage;
                    _receavedDamage[i].attacker.Hit(_receavedDamage[i].attacked);
                    killList.Add(i);
                }
            }

            for (int index = killList.Count - 1; index >= 0; index--)
            {
                _receavedDamage.RemoveAt(index);
            }

            if (_isUserLocked && !IsMoving && !_isAttacking)
            {
                _isUserLocked = false;
            }

            _prefab.GetComponent<UnitController>().CurrentHelth = Data.HitPoints / Data.MaxHitPoints;

        }

        public void Damage(IUnit other, float damage, float delay)
        {
            var delayedDamage = new DelayedDamage();
            delayedDamage.damage = damage;
            delayedDamage.delay = delay;
            delayedDamage.attacked = other;
            delayedDamage.attacker = this;
            _receavedDamage.Add(delayedDamage);
        }

        public abstract void Attack(IUnit other);
        public abstract void Hit(IUnit other);
        public abstract void StopAttacking();

    }
}