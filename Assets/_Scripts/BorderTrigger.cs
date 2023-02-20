using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {   
        if(hit.tag == "Player")
            WarningEngine.instance.enterArea();
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.tag == "Player")
            WarningEngine.instance.exitArea();
    }
}