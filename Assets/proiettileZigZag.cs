using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proiettileZigZag : MonoBehaviour
{
    //Questoo proiettile viaggia in un punto e rimbalza verso un secondo punto
    private Transform player;

    public float speed;
    public Vector2 target1;
    public Vector2 target2;
    public float timeToLive;
    public float dannoProiettile;
    
    private Rigidbody2D myRigidBody;

    //usato per controlo delle fasi
    bool fintaPrimaParte = false;


    // Start is called before the first frame update
    void Start()
    {
        

        player = GameObject.FindGameObjectWithTag("Player").transform;

        myRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!fintaPrimaParte)
        {
            transform.position = Vector2.MoveTowards(transform.position, target1, speed * Time.deltaTime);   //Il proiettile del nemico andrà contro al personaggio a prescindere che lui ci sia o si sia spostato

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target2, speed * Time.deltaTime);   //Il proiettile del nemico andrà contro al personaggio a prescindere che lui ci sia o si sia spostato

        }


        if ((this.transform.position.x == target2.x) && (this.transform.position.y == target2.y))
            Destroy(this.gameObject);

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

        /*else if (transform.position == target)
        {
            Destroy(this.gameObject);
        }*/
    }

    public void setTarget(Vector2 target1, Vector2 target2)
    {
        this.target1 = target1;
        this.target2 = target2;
    }

}
