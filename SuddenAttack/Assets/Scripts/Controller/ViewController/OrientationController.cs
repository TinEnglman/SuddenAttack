using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Controller.ViewController
{
    public class OrientationController : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _orientationTextures = default;
        [SerializeField]
        private SpriteRenderer _spriteRenrerer = default;

        private int _angleQuant = 0;

        private void Start()
        {
            _angleQuant = 360 / _orientationTextures.Count;
        }

        public void SetAngle(int angle)
        {
            _spriteRenrerer.sprite = GetTexture(angle);
        }

        public Sprite GetTexture(int angle)
        {
            angle += _angleQuant / 2;

            if (angle < 0)
            {
                angle += 360;
            }

            int index = angle / _angleQuant;
            return _orientationTextures[index];
        }

    }
}