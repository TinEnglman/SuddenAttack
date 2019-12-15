using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Buildings
{
    public abstract class AbstractBuilding : IBuilding
    {
        protected UnitData _unitData;
        protected GameObject _unitPrefab;
        protected GameObject _prefab;
        protected float _currentCountdown = 0;
        protected IUnitFactory _unitFactory;
        protected Vector2 _spawnOffset;
        protected List<DelayedDamage> _receavedDamage = new List<DelayedDamage>();

        public AbstractBuilding()
        {
            IsSpawning = false;
            _spawnOffset = new Vector3(1, 1, 0);

        }

        public void SetFactory(IUnitFactory factory)
        {
            _unitFactory = factory;
        }

        public IUnitFactory GetFactory()
        {
            return _unitFactory;
        }

        public bool IsSpawning
        {
            get; set;
        }

        public bool CanFire() { return false; }
        public void Fire() { }

        public void StopAttacking() { }
        public void Attack(IUnit other) { }
        public void Hit(IUnit other) { }
        public void Move(Vector3 destination) { }
        public void Stop() { }

        public void Damage(IUnit other, float damage, float delay)
        {
            var delayedDamage = new DelayedDamage();
            delayedDamage.damage = damage;
            delayedDamage.delay = delay;
            delayedDamage.attacked = other;
            delayedDamage.attacker = this;
            _receavedDamage.Add(delayedDamage);
        }

        public void Die()
        {
            Prefab.SetActive(false);
            IsSpawning = false;
        }

        public bool IsBuilding()
        {
            return true;
        }

        public bool IsUserLocked
        {
            get { return false; }
            set { }
        }

        public bool IsMoving
        {
            get { return false; }
            set { }
        }

        public void Select()
        {
            _prefab.GetComponent<BuildingController>().Select();
        }

        public void Deselect()
        {
            _prefab.GetComponent<BuildingController>().Deselect();
        }

        public GameObject UnitPrefab
        {
            get { return _unitPrefab; }
            set { _unitPrefab = value; }
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

        public void Update()
        {
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

            _prefab.GetComponent<BuildingController>().CurrentHelth = Data.HitPoints / Data.MaxHitPoints;
        }

        public IUnit Update(float dt)
        {
            IUnit newUnit = null;
            if (IsSpawning)
            {
                _currentCountdown -= dt;
                if (_currentCountdown <= 0)
                {
                    _currentCountdown = Data.BuildCooldown;
                    newUnit = SpawnUnit();
                    IsSpawning = false;
                }
            }
            return newUnit;
        }

        public float GetCompletePercent()
        {
            return 1 - _currentCountdown / Data.BuildCooldown;
        }

        public abstract IUnit SpawnUnit();
        public abstract int GetIncome();
    }
}