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

        public void Select()
        {
            _prefab.GetComponent<UnitController>().Select(); // refactor after input system refactor
        }

        public void Deselect()
        {
            _prefab.GetComponent<UnitController>().Deselect();
        }

        public abstract void OnUpdate(float dt);
        public abstract void OnMove(Vector3 destination);
        public abstract void OnAttack(IUnit other);
        public abstract void OnFire();
        public abstract void OnHit(IUnit other);
        public abstract void OnStopAttacking();
        public abstract void OnStop();

        public virtual void OnDie()
        {
            var bulletController = Prefab.GetComponentInChildren<BulletContoller>(); // refactor; combat controller should handle projectiles
            if (bulletController != null)
            {
                bulletController.gameObject.SetActive(false);
            }

            Prefab.SetActive(false);
        }
    }
}