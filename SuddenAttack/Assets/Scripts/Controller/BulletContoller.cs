using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContoller : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile = null;
    private GameObject _target = null;
    private Vector3 _direction;

    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public void SetPosition(Vector3 position)
    {
        _projectile.transform.position = position;
    }

    public void Fire()
    {
        _projectile.SetActive(true);
        _direction = (_target.transform.position - transform.position).normalized;
        Vector3 angles = _projectile.transform.rotation.eulerAngles;
        Vector3 newRotation = new Vector3(angles.x, angles.y, Vector3.Angle(_direction, Vector3.right));
        _projectile.transform.localRotation = Quaternion.Euler(newRotation);
    }

    void Awake()
    {
        _projectile = GameObject.Instantiate(_projectile);
        _projectile.SetActive(false);
    }

    void Update()
    {
        if (_target != null)
        {

            float speed = 8;
            _projectile.transform.Translate(_direction * speed * Time.deltaTime, Space.World);
        }
    }
}
