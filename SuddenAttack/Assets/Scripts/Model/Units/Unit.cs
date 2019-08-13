using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : IUnit
{
    public class DelayedDamage
    {
        public IUnit other;
        public volatile float damage; // refactor
        public volatile float delay;
    }

    protected UnitData _unitData;
    protected GameObject _prefab;
    protected float _weaponCooldown = 0.0f;
    protected bool _isAttacking = false;
    protected bool _canFire = true;
    protected List<DelayedDamage> _receavedDamage = new List<DelayedDamage>();


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

    public void Fire()
    {
        _canFire = false;
    }

    public void Update()
    {
        if (_isAttacking)
        {
            _weaponCooldown -= Time.deltaTime;

            if (_weaponCooldown <= 0)
            {
                _weaponCooldown += _unitData.WeaponCooldown;
                _canFire = true;
            }
        }

        List<int> killList = new List<int>();
        for (int i = 0; i < _receavedDamage.Count; i++)
        {
            _receavedDamage[i].delay -= Time.deltaTime;

            if (_receavedDamage[i].delay <= 0)
            {
                _receavedDamage[i].other.Data.HitPoints -= Data.Damage;
                killList.Add(i);
            }
        }

        for(int index = killList.Count - 1; index > 0; index--)
        {
            _receavedDamage.RemoveAt(index);
        }

    }

    public void Damage(IUnit other, float damage, float delay)
    {
        var delayedDamage = new DelayedDamage();
        delayedDamage.damage = damage;
        delayedDamage.delay = delay;
        delayedDamage.other = other;
        _receavedDamage.Add(delayedDamage);
    }

    public abstract void Attack(IUnit other);

}
