using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)//sono presenti sullo schermo da 1 o piu tocchi?
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // inseriamo in vector3 la posizione dello dito sullo schermo trasfromata nel worldPoint
            touchPosition.z = 0f; //dato che la touch position modifica la z
            transform.position = touchPosition; //sposta la mia posizione alla posizione del mio dito
        } 
    }
}
