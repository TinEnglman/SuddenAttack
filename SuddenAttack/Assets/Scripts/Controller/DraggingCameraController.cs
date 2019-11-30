using UnityEngine;
using Zenject;
using SuddenAttack.Model;
using UnityEngine.U2D;

namespace SuddenAttack.Controllers
{
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
        private PixelPerfectCamera _pixelPerfectComponent;
        private Vector3 _currentDirectionVector;
        private IInputManager _inputManager = default;


        void Awake()
        {
            _currentDirectionVector = Vector3.zero;
            _pixelPerfectComponent = gameObject.AddComponent<PixelPerfectCamera>();
            _pixelPerfectComponent.refResolutionX = 1920;
            _pixelPerfectComponent.refResolutionY = 1200;
            _pixelPerfectComponent.assetsPPU = 100;
            _pixelPerfectComponent.cropFrameX = true;
            _pixelPerfectComponent.cropFrameY = true;
            _pixelPerfectComponent.upscaleRT = true;
        }

        [Inject]
        public void Construct(Camera camera, IInputManager inputManager)
        {
            _inputManager = inputManager;
            _camera = camera;
        }


        void Update()
        {
            if (_inputManager.IsDown(KeyCode.A))
            {
                _lockCamera = true;
            }

            if (_inputManager.IsDown(KeyCode.B))
            {
                _lockCamera = false;
            }

            if (_lockCamera)
            {
                return;
            }

            _currentDirectionVector = Vector3.zero;

            Vector2 mousePosition = _inputManager.GetMouseScreenPosition();
            if (mousePosition.x < _edgePadding && _camera.transform.position.x > _cameraPositionMin.x)
            {
                _currentDirectionVector = Vector3.left;
            }
            else if (mousePosition.x > Screen.width - _edgePadding && _camera.transform.position.x < _cameraPositionMax.x)
            {
                _currentDirectionVector = Vector3.right;
            }

            if (mousePosition.y < _edgePadding && _camera.transform.position.y < _cameraPositionMin.y)
            {
                _currentDirectionVector += Vector3.down;
            }
            else if (mousePosition.y > Screen.height - _edgePadding && _camera.transform.position.y > _cameraPositionMax.y)
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
}