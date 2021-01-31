using UnityEngine;
using Zenject;
using SuddenAttack.Model;
using UnityEngine.U2D;

namespace SuddenAttack.Controller.ViewController
{
    public class DraggingCameraController : MonoBehaviour
    {
        //private int DEFAULT_SCREEN_X = 1920;
        //private int DEFAULT_SCREEN_Y = 1080;
        //private int PIXELS_PER_UNIT = 100;

        [SerializeField]
        private bool _lockCamera = false;
        [SerializeField]
        private float _cameraSpeed = 1.0f;
        [SerializeField]
        private float _edgePadding = 10.0f;
        [SerializeField]
        private Vector3 _cameraPositionMin = new Vector3(-20, 7, -20);
        [SerializeField]
        private Vector3 _cameraPositionMax = new Vector3(20, -31, -20);

        private Camera _camera;
        private PixelPerfectCamera _pixelPerfectComponent;
        private Vector3 _currentDirectionVector;
        private IInputManager _inputManager = default;


        void Awake()
        {
            _currentDirectionVector = Vector3.zero;

            /*
            _pixelPerfectComponent = gameObject.AddComponent<PixelPerfectCamera>();
            _pixelPerfectComponent.refResolutionX = DEFAULT_SCREEN_X;
            _pixelPerfectComponent.refResolutionY = DEFAULT_SCREEN_Y;
            _pixelPerfectComponent.assetsPPU = PIXELS_PER_UNIT;
            _pixelPerfectComponent.cropFrameX = false;
            _pixelPerfectComponent.cropFrameY = false;
            _pixelPerfectComponent.upscaleRT = false;
            */

        }

        [Inject]
        public void Construct(Camera camera, IInputManager inputManager)
        {
            _inputManager = inputManager;
            _camera = camera;
        }


        void Update()
        {
            if (_inputManager.IsPressed(KeyCode.A))
            {
                _lockCamera = true;
            }

            if (_inputManager.IsPressed(KeyCode.B))
            {
                _lockCamera = false;
            }

            if (_inputManager.IsPressed(KeyCode.KeypadMinus))
            {
                _camera.orthographicSize += 0.1f;
            }

            if (_inputManager.IsPressed(KeyCode.KeypadPlus))
            {
                _camera.orthographicSize -= 0.1f;
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