using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SuddenAttack.Controllers
{
    public class BulletContoller : MonoBehaviour
    {
        [SerializeField]
        private GameObject _projectile = null;
        private GameObject _target = null;
        private Vector3 _direction;
        private float _projectileSpeed = 1;
        private Vector3 _projectileOrigin;

        public GameObject Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Vector3 ProjectileOrigin
        {
            get { return _projectileOrigin; }
            set { _projectileOrigin = value; }
        }

        public float ProjectileSpeed
        {
            get { return _projectileSpeed; }
            set { _projectileSpeed = value; }
        }

        public void Fire()
        {
            _projectile.SetActive(true);
            _direction = (_target.transform.position - transform.position).normalized;
            Vector3 newRotation = new Vector3(0, 0, Vector3.Angle(Vector3.right, _direction));
            newRotation.z *= _direction.y / Mathf.Abs(_direction.y);
            _projectile.transform.position = _projectileOrigin;
            _projectile.transform.rotation = Quaternion.Euler(newRotation);
        }

        public void HitTarget()
        {
            _projectile.SetActive(false);
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
                _projectile.transform.Translate(_direction * _projectileSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                _projectile.SetActive(false);
            }
        }
    }
}