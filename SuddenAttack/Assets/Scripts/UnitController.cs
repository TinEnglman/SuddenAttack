using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField]
    private float _currentAngle = 0f;
    [SerializeField]
    private Animator animator;
    private Vector2 _direction;
    

    void Awake()
    {
        _direction = Vector2.up;
    }

    void Update()
    {
        animator.SetFloat("Angle", _currentAngle);
    }
}
