using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.ViewController.Units.Tanks
{
    public class FireController : MonoBehaviour
    {
        private const float FRAME_INTERVAL = 0.05f;

        [SerializeField]
        private SpriteRenderer _spriteRenderer = default;
        [SerializeField]
        private System.Collections.Generic.List<Sprite> _sprites = default;

        private float _time = 0;
        private bool _isFired = false;
        private int _currentIndex = 0;

        public void OnFire()
        {
            _time = 0;
            _currentIndex = 0;
            _isFired = true;

            var orientationController = gameObject.GetComponentInChildren<TurretController>().gameObject.GetComponent<OrientationController>(); // wonky, refactor candidate
            _spriteRenderer.transform.position = orientationController.GetProjectalOrigin().position;

            float angle = orientationController.GetAngle();
            _spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -angle));
            _spriteRenderer.gameObject.SetActive(true);
            _spriteRenderer.sprite = _sprites[_currentIndex];
        }

        private void Start()
        {
            _spriteRenderer.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_isFired)
            {
                _time += Time.deltaTime;

                if (_time > FRAME_INTERVAL && (_currentIndex + 1 ) == _sprites.Count)
                {
                    _time = 0;
                    _isFired = false;
                    _currentIndex = 0;
                    _spriteRenderer.gameObject.SetActive(false);

                }

                if (_time > FRAME_INTERVAL && (_currentIndex + 1) < _sprites.Count)
                {
                    _time = 0;
                    _currentIndex++;
                    _spriteRenderer.sprite = _sprites[_currentIndex];
                }
            }
        }
    }
}