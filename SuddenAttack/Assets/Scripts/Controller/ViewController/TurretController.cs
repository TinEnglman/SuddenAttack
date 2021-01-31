using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.ViewController
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target = null;
        [SerializeField]
        private OrientationController _orientationController = null;


        public GameObject Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private void Awake()
        {
            _target = gameObject;
        }

        private void Update()
        {
            float targetAngle = 0.0f;

            if (_target == null)
            {
                _target = gameObject;
            }

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

            if (_orientationController != null)
            {
                _orientationController.SetAngle((int)targetAngle);
            }

        }
    }
}