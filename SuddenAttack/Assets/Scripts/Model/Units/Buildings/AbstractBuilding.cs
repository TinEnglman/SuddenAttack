using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controller.ViewController.Buildings;
using SuddenAttack.Model.Data;
using SuddenAttack.Model.Factories;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Buildings
{
    public abstract class AbstractBuilding : IBuilding
    {
        protected BuildingData _buildingData;
        protected WeaponData _weaponData;    
        protected GameObject _prefab;
        protected Vector2 _position;
        List<string> _unitIds;
        public Vector2 SpawnOffset { get; set; }
        public float WeaponCooldown { get; set; }
        public float HitPoints { get; set; }
        public int TeamIndex { get; set; }

        public AbstractBuilding()
        {
            SpawnOffset = new Vector3(1, 1, 0);
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

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual void OnUpdate(float dt)
        {
            if (WeaponCooldown > 0)
            {
                WeaponCooldown -= dt;
            }
        }

        public abstract void OnMove(Vector3 destination);
        public abstract void OnAttack(IUnit other);
        public abstract void OnFire();
        public abstract void OnHit(IUnit other);
        public abstract void OnStopAttacking();
        public abstract void OnStop();

        public virtual void OnDie()
        {
            Prefab.SetActive(false);
        }
    }
}