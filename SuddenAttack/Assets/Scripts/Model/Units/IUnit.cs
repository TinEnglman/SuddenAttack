using SuddenAttack.Model.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Units
{
    public interface IUnit
    {
        UnitData Data { get; set; }
        bool IsUserLocked { get; set; }
        GameObject Prefab { get; set; }

        bool IsMoving { get; }
        void Select();
        void Deselect();
        void Update();

        void StopAttacking();
        void Attack(IUnit other);
        bool CanFire();
        void Fire();
        void Hit(IUnit other);
        void Damage(IUnit other, float damage, float delay);
        void Move(Vector3 destination);
        void Stop();
        void Die();
        bool IsBuilding(); // hacky
    }
}