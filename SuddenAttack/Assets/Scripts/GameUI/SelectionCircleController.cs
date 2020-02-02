using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuddenAttack.Gui
{
    public class SelectionCircleController : MonoBehaviour
    {
        void Awake()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        public void Selectct()
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        public void Deselectct()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}