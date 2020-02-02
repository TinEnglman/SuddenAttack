﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Buildings;

namespace SuddenAttack.Model
{
    public class GameManager
    {
        private List<IUnit> _units = null;
        private List<IBuilding> _buildings = null;
        private int _currentFunds = 0;


        public int Funds
        {
            get { return _currentFunds; }
            set { _currentFunds = value; }
        }

        public List<IUnit> Units
        {
            get { return _units; }
        }

        public List<IBuilding> Buildings
        {
            get { return _buildings; }
        }

        public GameManager()
        {
            _units = new List<IUnit>();
            _buildings = new List<IBuilding>();
        }

        public void Update(float dt)
        {
            List<IUnit> killList = new List<IUnit>();
            foreach (IUnit unit in _units)
            {
                unit.Update();
                if (unit.Data.HitPoints <= 0)
                {
                    killList.Add(unit);
                }
            }

            foreach (IUnit unit in killList)
            {
                _units.Remove(unit);
                unit.Die();
            }
        }

        public void AddUnit(IUnit unit)
        {
            _units.Add(unit);
        }

        public void RemoveUnit(IUnit unit)
        {
            _units.Remove(unit);
        }

        /*
        public void MoveSelected(Vector3 destination) // move to command
        {
            foreach (IUnit unit in _selectedUnits)
            {
                unit.Move(destination);
            }
        }
        */

        public List<IUnit> GetTargets(IUnit source) // called form update; slow af; refactor
        {
            List<IUnit> targets = new List<IUnit>();
            foreach (IUnit unit in _units)
            {
                if (source.Data.EngageRange > (unit.Prefab.transform.position - source.Prefab.transform.position).magnitude)
                {
                    targets.Add(unit);
                }
            }

            return targets;
        }
    }
}