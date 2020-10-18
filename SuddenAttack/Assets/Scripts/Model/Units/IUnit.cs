using SuddenAttack.Model.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Model.Units
{
    public interface IUnit
    {
        GameObject Prefab { get; set; }

        float HitPoints { get; set; }
        int TeamIndex { get; set; }
        WeaponData WeaponData { get; set; }
        Vector2 Position { get; set; }

        void Select();
        void Deselect();
        void OnUpdate(float dt);
        void OnMove(Vector3 destination);
        void OnAttack(IUnit other);
        void OnFire();
        void OnHit(IUnit other);
        void OnStopAttacking();
        void OnStop();
        void OnDie();

        //void StopAttacking();
        //void Attack(IUnit other);
        //bool CanFire();
        //void Fire();
        //void Hit(IUnit other);
        //void Damage(IUnit other, float damage, float delay);
        //void Move(Vector3 destination);
        //void Stop();
        //void Die();
        //bool IsBuilding(); // hacky
    }
}