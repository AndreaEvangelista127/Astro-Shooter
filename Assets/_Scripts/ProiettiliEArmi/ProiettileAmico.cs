using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileAmico : MonoBehaviour
{
    //vedere l'utilità di queste variabili
    //public float speed;
    //private bool hit;

    public float dannoProiettile;

    //usato per animazioni e cose più precise (tutorial di Pandemonium)
    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()  //usato per prendere collider e animazione, da approfondire come funziona
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (dannoProiettile > 1f)
            dannoProiettile = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //   if (hit) return;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        //if (collision.gameObject.tag != "Player")
        //{
            //Destroy(collision.gameObject);  //così disatruggo ogni oggetto che tocca il proiettile
            //Destroy(this.gameObject);
        //}
    //}
//}

    //tappando isTriggered permette al proiettile di attivare determinazione azioni prendendo spunto dalle collisioni e dalle loro caratteristiche
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("trigger");
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<health>().takeDamge(dannoProiettile);
            //Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if(collision.tag != "Player" && collision.tag != "ProiettiliNemici" && collision.tag != "Proiettile Amico")
        {
            Destroy(this.gameObject);
        }
    }
}