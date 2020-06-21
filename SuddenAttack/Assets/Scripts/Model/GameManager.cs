﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Model.Units;
using SuddenAttack.Model.Buildings;

namespace SuddenAttack.Model
{
    public class GameManager
    {
        private List<IMobileUnit> _moblieUnits;
        private List<IBuilding> _buildings;
        private List<IUnit> _units;
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

        public List<IMobileUnit> MobileUnits
        {
            get { return _moblieUnits; }
        }

        public List<IBuilding> Buildings
        {
            get { return _buildings; }
        }

        public GameManager()
        {
            _moblieUnits = new List<IMobileUnit>();
            _buildings = new List<IBuilding>();
            _units = new List<IUnit>();
        }

        public void Update(float dt)
        {
            List<IUnit> killList = new List<IUnit>();
            foreach (IUnit unit in _moblieUnits)
            {
                unit.OnUpdate(dt);
                if (unit.HitPoints <= 0)
                {
                    killList.Add(unit);
                }
            }

            foreach (IMobileUnit unit in killList)
            {
                _moblieUnits.Remove(unit);
                unit.OnDie();
            }
        }

        public void AddMobileUnit(IMobileUnit unit)
        {
            _moblieUnits.Add(unit);
            _units.Add(unit);
        }

        public void RemoveMobileUnit(IMobileUnit unit)
        {
            _moblieUnits.Remove(unit);
            _units.Remove(unit);
        }

        public void AddBuilding(IBuilding unit)
        {
            _buildings.Add(unit);
            _units.Add(unit);
        }

        public void RemoveBuilding(IBuilding unit)
        {
            _buildings.Remove(unit);
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

        public List<IUnit> GetTargets(IMobileUnit source) // called form update; slow af; refactor; move to combatManager
        {
            List<IUnit> targets = new List<IUnit>();
            foreach (IUnit unit in _moblieUnits)
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