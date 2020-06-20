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
        //protected UnitData _unitData;
        protected BuildingData _buildingData;
        protected WeaponData _weaponData;    
        protected GameObject _prefab;
        List<string> _unitIds;
        public Vector2 SpawnOffset { get; set; }
        public float HitPoints { get; set; }
        public bool IsFriendly { get; set; }

        //protected float _currentCountdown = 0;
        protected IUnitFactory _unitFactory; // refactor?
        //protected List<DelayedDamage> _receavedDamage = new List<DelayedDamage>(); // refactor

        public List<string> UnitIds { get { return _unitIds;  } set { _unitIds = value; } }

        public AbstractBuilding()
        {
            SpawnOffset = new Vector3(1, 1, 0);
        }

        public void SetFactory(IUnitFactory factory)
        {
            _unitFactory = factory;
        }

        public IUnitFactory GetFactory()
        {
            return _unitFactory;
        }

        public void Select()
        {
            _prefab.GetComponent<BuildingController>().Select();
        }

        public void Deselect()
        {
            _prefab.GetComponent<BuildingController>().Deselect();
        }

        public WeaponData WeaponData
        {
            get { return _weaponData; }
            set { _weaponData = value; }
        }


        public BuildingData BuildingData
        {
            get { return _buildingData; }
            set { _buildingData = value; }
        }

        public GameObject Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }

        /*
        public UnitData Data // replaced with unit id list
        {
            get { return _unitData; }
            set { _unitData = value; }
        }
        */


        //public bool CanFire() { return false; }
        //public void Fire() { }

        public abstract void OnUpdate(float dt);
        public abstract void OnMove(Vector3 destination);
        public abstract void OnAttack(IUnit other);
        public abstract void OnFire();
        public abstract void OnHit(IUnit other);
        public abstract void OnStopAttacking();
        public abstract void OnStop();

        /*
        public void Damage(IUnit other, float damage, float delay)
        {
            var delayedDamage = new DelayedDamage();
            delayedDamage.damage = damage;
            delayedDamage.delay = delay;
            delayedDamage.attacked = other;
            delayedDamage.attacker = this;
            _receavedDamage.Add(delayedDamage);
        }
        */
        public virtual void OnDie()
        {
            Prefab.SetActive(false);
        }

       
        public void Update()
        {
            /* // refactor; moveto combat manager
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
            */
        }

        /* refactor; Create "BuilingManager" or "ProductionManager"
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
        */
    }
}