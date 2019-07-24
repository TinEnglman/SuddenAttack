using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField]
    private float _currentAngle = 0f;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Vector3 _destination;
    [SerializeField]
    private float _moveSpeed = 1.0f;


    void Awake()
    {
        _destination = GetComponent<Transform>().position;
    }

    void Update()
    {
        _animator.SetFloat("Angle", _currentAngle);

        if (_destination != transform.position)
        {
            Vector3 direction = (_destination - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
            if (Mathf.Abs(direction.x) == 0)
            { 
                if (direction == Vector3.up)
                {
                    _currentAngle = 0.0f;
                }
                else
                {
                    _currentAngle = 180.0f;
                }
            }
            else
            { 
                _currentAngle = Vector3.Angle(direction, Vector3.up) * (direction.x / Mathf.Abs(direction.x));
            }

            if ((_destination - transform.position).sqrMagnitude < 0.01f)
            {
                transform.position = _destination;
            }
        }
    }
}
