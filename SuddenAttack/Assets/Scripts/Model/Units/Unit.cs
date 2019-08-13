using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : IUnit
{
    protected UnitData _unitData;
    protected GameObject _prefab;
    protected float _weaponCooldown = 0.0f;
    protected bool _isAttacking = false;
    protected bool _canFire = true;

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

    public void Move(Vector3 destination)
    {
        _prefab.GetComponent<UnitController>().SetDestination(destination);
    }

    public void Select()
    {
        _prefab.GetComponent<UnitController>().Select();
    }

    public void Deselect()
    {
        _prefab.GetComponent<UnitController>().Deselect();
    }

    public bool CanFire()
    {
        return _canFire;
    }

    public void Fire(IUnit other)
    {
        _canFire = false;
        other.Data.HitPoints -= Data.Damage;
    }

    public void Update()
    {
        if (_isAttacking)
        {
            _weaponCooldown -= Time.deltaTime;

            if (_weaponCooldown <= 0)
            {
                _weaponCooldown += _unitData.FireSpeed;
                _canFire = true;
            }
        }
    }

    public abstract void Attack(IUnit other);

}
