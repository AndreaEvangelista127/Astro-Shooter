using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileDivisorio : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;  //dove si strova il gicoatore
    public float timeToLive;
    public float dannoProiettile;

    public GameObject projectileCreato;

    //creo le 4 direszioni oblique su cui poi si divide
    Vector2 v1 = new Vector2(5f, 5f);
    Vector2 v2 = new Vector2(-5f, -5f);
    Vector2 v3 = new Vector2(-5f, 5f);
    Vector2 v4 = new Vector2(5f, -5f);

    //creo le 4 direzioni verticali-orizzionatali
    Vector2 v5 = new Vector2(0f, 5f);
    Vector2 v6 = new Vector2(0f, -5f);
    Vector2 v7 = new Vector2(5f, 0f);
    Vector2 v8 = new Vector2(-5f, 0f);


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //il proiettile che si divide inizialmente viaggia verso il giocatore
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //appena raggiunge la posizione del giocatore randomicamente si potrà dividere
        if ((this.transform.position.x == target.x) && (this.transform.position.y == target.y))
        {
            Destroy(this.gameObject);
            int a = Random.Range(0, 3); //minimo incluso e massimo escluso
            if (a == 0)
                divisioneProiettile4oblique();
            if (a == 1)
                divisioneProiettile4VerticaleOrizzontale();
            if (a == 2)
                divisioneProiettileCompleto();
        }
    }

    void divisioneProiettile4oblique()
    {
        projectileStraight proiettile = this.gameObject.AddComponent<projectileStraight>();
        proiettile.GetComponent<CircleCollider2D>().enabled = true;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
                proiettile.setTarget(v1);
            if (i == 1)
                proiettile.setTarget(v2);
            if (i == 2)
                proiettile.setTarget(v3);
            if (i == 3)
                proiettile.setTarget(v4);

            Instantiate(proiettile, transform.position, Quaternion.identity);
        }

    }

    void divisioneProiettile4VerticaleOrizzontale()
    {
        projectileStraight proiettile = this.gameObject.AddComponent<projectileStraight>();
        proiettile.GetComponent<CircleCollider2D>().enabled = true;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
                proiettile.setTarget(v5);
            if (i == 1)
                proiettile.setTarget(v6);
            if (i == 2)
                proiettile.setTarget(v7);
            if (i == 3)
                proiettile.setTarget(v8);

            Instantiate(proiettile, transform.position, Quaternion.identity);
        }

    }


    void divisioneProiettileCompleto()
    {
        projectileStraight proiettile = this.gameObject.AddComponent<projectileStraight>();
        proiettile.GetComponent<CircleCollider2D>().enabled = true;
        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
                proiettile.setTarget(v1);
            if (i == 1)
                proiettile.setTarget(v2);
            if (i == 2)
                proiettile.setTarget(v3);
            if (i == 3)
                proiettile.setTarget(v4);
            if (i == 4)
                proiettile.setTarget(v5);
            if (i == 5)
                proiettile.setTarget(v6);
            if (i == 6)
                proiettile.setTarget(v7);
            if (i == 7)
                proiettile.setTarget(v8);

            Instantiate(proiettile, transform.position, Quaternion.identity);
        }

    }

    void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Giocatore colpito");
        if (collision.tag == "Player")
        {
            collision.GetComponent<health>().takeDamge(dannoProiettile);
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Meteor")
        {
            Destroy(this.gameObject);
        }
    }

    public void setTarget(Vector2 target)
    {
        this.target = target;
    }
}
