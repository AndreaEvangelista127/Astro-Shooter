 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTarget : MonoBehaviour 
{

    public float speed;

    private Transform player;
    public Vector2 target;
    public float timeToLive;
    public float dannoProiettile;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", timeToLive);

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);   //Il proiettile del nemico andrà contro al personaggio a prescindere che lui ci sia o si sia spostato

        if ((this.transform.position.x == target.x) && (this.transform.position.y == target.y))
            Destroy(this.gameObject);

        /*if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }*/
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

   public void setTarget(Vector2 target)
    {
        this.target = target;
    }
}
