﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuddenAttack.Controller.ViewController;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Factories
{
    public class SoliderFactory : IUnitFactory
    {
        private int _cost = default;
        private CombatManager _combatManager = default;

        public SoliderFactory(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public Unit CreateUnit(float x, float y, GameObject prefab, bool isFriendly)
        {
            Vector3 position = new Vector3(x, y, 0);

            var solider = new Solider(_combatManager)
            {
                Prefab = Object.Instantiate(prefab)
            };

            _cost = 10;
            solider.Data.Cost = _cost;
            solider.Data.IsFriendly = isFriendly;
            solider.Prefab.transform.SetPositionAndRotation(position, solider.Prefab.transform.rotation);
            solider.Prefab.GetComponent<UnitController>().SetDestination(position);

            return solider;
        }

        public string GetDisplayName()
        {
            return "Solider"; // refactor
        }

        public int GetCost()
        {
            return _cost;
        }
    }
}
