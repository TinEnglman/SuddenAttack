using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingCameraController : MonoBehaviour
{

    [SerializeField]
    private bool _lockCamera = false;
    [SerializeField]
    private float _cameraSpeed = 1.0f;
    [SerializeField]
    private float _edgePadding = 10.0f;
    [SerializeField]
    private Vector3 _cameraPositionMin = new Vector3(-20, 7, -10);
    [SerializeField]
    private Vector3 _cameraPositionMax = new Vector3(20, -31, -10);

    private Camera _camera;
    private Vector3 _currentDirectionVector;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _currentDirectionVector = Vector3.zero;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _lockCamera = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _lockCamera = false;
        }


        if (_lockCamera)
        {
            return;
        }

        _currentDirectionVector = Vector3.zero;

        if (Input.mousePosition.x < _edgePadding && _camera.transform.position.x >  _cameraPositionMin.x)
        {
            _currentDirectionVector = Vector3.left;
        }
        else if (Input.mousePosition.x > Screen.width - _edgePadding && _camera.transform.position.x < _cameraPositionMax.x)
        {
            _currentDirectionVector = Vector3.right;
        }

        if (Input.mousePosition.y < _edgePadding && _camera.transform.position.y < _cameraPositionMin.y)
        {
            _currentDirectionVector += Vector3.down;
        }
        else if (Input.mousePosition.y > Screen.height - _edgePadding && _camera.transform.position.y > _cameraPositionMax.y)
        {
            _currentDirectionVector += Vector3.up;
        }
    }

    private void FixedUpdate()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        _camera.transform.position += _currentDirectionVector * _cameraSpeed;
    }
}
