using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    public GameObject floatingText;
    // Start is called before the first frame update

    public void showScore(int score)
    {
        if (floatingText)
        {
            Invoke("DistruggiOggetto", 2);
            GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = ("+" + score.ToString());
            Invoke("DistruggiOggetto", 2);
        }
    }

    

}

