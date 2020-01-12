using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Data;
using SuddenAttack.Controller.ViewController;

namespace SuddenAttack.Model.Units
{
    public abstract class Unit : IUnit
    {
        protected UnitData _unitData;
        protected GameObject _prefab;
        protected CombatManager _combatManager = default;

        protected float _weaponCooldown = 0.0f;
        protected bool _isAttacking = false;
        protected bool _canFire = true;
        private bool _isUserLocked = false;

        public Unit(CombatManager combatManager)
        {
            _combatManager = combatManager;
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


        public void Stop()
        {
            //_prefab.GetComponent<UnitController>().SetDestination(_prefab.transform.position);
            _combatManager.StopUnit(this);
        }

        public void Select()
        {
            _prefab.GetComponent<UnitController>().Select(); // refactor after input system refactor
        }

        public void Deselect()
        {
            _prefab.GetComponent<UnitController>().Deselect();
        }

        public bool IsMoving
        {
            get { return _combatManager.IsMoving(this); }
        }

        public bool IsUserLocked
        {
            get { return _isUserLocked; } // refactor to controll/input system perhaps?
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

            if (_isUserLocked && !IsMoving && !_isAttacking)
            {
                _isUserLocked = false;
            }

            _prefab.GetComponent<UnitController>().CurrentHelth = Data.HitPoints / Data.MaxHitPoints;
        }

        public void Damage(IUnit other, float damage, float delay)
        {
            _combatManager.Damage(this, other, damage, delay);
        }

        public virtual void Attack(IUnit other)
        {
            _isAttacking = true;
        }
        public virtual void StopAttacking()
        {
            _isAttacking = false;
        }

        public virtual void Move(Vector3 destination)
        {
            //_prefab.GetComponent<UnitController>().SetDestination(destination);
            _combatManager.MoveUnit(this, destination);
        }

        public abstract void Hit(IUnit other);
    }
}