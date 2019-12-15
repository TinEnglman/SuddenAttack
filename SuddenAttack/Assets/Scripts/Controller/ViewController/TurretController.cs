using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.ViewController
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null;
        [SerializeField]
        private GameObject _target = null;


        public GameObject Target
        {
            get { return _target; }
            set { _target = value; }
        }

        void Awake()
        {
            _target = gameObject;
        }

        void Update()
        {
            float targetAngle = 0.0f;
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            if (Mathf.Abs(direction.x) == 0)
            {
                if (direction == Vector3.up)
                {
                    targetAngle = 0.0f;
                }
                else
                {
                    targetAngle = 180.0f;
                }
            }
            else
            {
                targetAngle = Vector3.Angle(direction, Vector3.up) * (direction.x / Mathf.Abs(direction.x));
            }
            _animator.SetFloat("Angle", targetAngle);
        }
    }
}