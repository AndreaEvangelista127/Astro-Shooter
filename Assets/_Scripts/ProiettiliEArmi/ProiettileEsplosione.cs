using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileEsplosione : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    public float dannoProiettile;
    public int tempoEsplosione;
    public bool esploso = false;
    
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);   //Il proiettile del nemico andrà contro al personaggio a prescindere che lui ci sia o si sia spostato

        if ((this.transform.position.x == target.x) && (this.transform.position.y == target.y) && !esploso)
        { 
            Esplosione();
            esploso = true;
        }   
    }

    void Esplosione()
    {
        transform.localScale += new Vector3(6f, 6f, 0f);
        Invoke("DestroyProjectile", tempoEsplosione);
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
            
        }
        else if (collision.tag == "Meteor")
        {
            Esplosione();
            esploso = true;
        }
    }

    public void setTarget(Vector2 target)
    {
        this.target = target;
    }
}
