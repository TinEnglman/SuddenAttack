using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.ViewController.Units
{
    public class OrientationController : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _orientationTextures = default;
        [SerializeField]
        private List<Transform> _projectileOffsets = new List<Transform>();
        [SerializeField]
        private SpriteRenderer _spriteRenrerer = default;

        private int _currentAngle = 0;
        private int _angleQuant = 0;

        private void Start()
        {
            _angleQuant = 360 / _orientationTextures.Count;
        }

        public void SetAngle(int angle)
        {
            _currentAngle = angle;
            int index = GetIndex(angle);
            int resultAngle = -angle + index * _angleQuant;
            _spriteRenrerer.sprite = GetTexture();
            _spriteRenrerer.transform.rotation = Quaternion.AngleAxis(resultAngle, new Vector3(0, 0, 1));
        }

        public float GetAngle()
        {
            return _currentAngle;
        }

        public int GetIndex(int angle)
        {
            angle += _angleQuant / 2;

            if (angle < 0)
            {
                angle += 360;
            }

            return angle / _angleQuant;
        }

        public Sprite GetTexture()
        {
            int index = GetIndex(_currentAngle);
            return _orientationTextures[index];
        }

        public Transform GetProjectalOrigin()
        {
            int index = GetIndex(_currentAngle);
            return _projectileOffsets[index];
        }

    }
}