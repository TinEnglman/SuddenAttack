using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
