using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileSpawn : MonoBehaviour
{

    public float speed;
    private Transform player;
    private Vector2 target;
    public float timeToLive;
    public float dannoProiettile;

    public GameObject projectile;
    public float spawnRate = 1f;
    private float nextSpawn = 0f;

    //creo le 4 direszioni oblique su cui poi può andare
    Vector2 v1 = new Vector2(3f, 3f);
    Vector2 v2 = new Vector2(-3f, -3f);
    Vector2 v3 = new Vector2(-3f, 3f);
    Vector2 v4 = new Vector2(3f, -3f);

    //creo le 4 direzioni verticali-orizzionatali
    Vector2 v5 = new Vector2(0f, 3f);
    Vector2 v6 = new Vector2(0f, -3f);
    Vector2 v7 = new Vector2(3f, 0f);
    Vector2 v8 = new Vector2(-3f, 0f);
    List<Vector2> listaDirezioni = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        listaDirezioni.Add(v1);
        listaDirezioni.Add(v2);
        listaDirezioni.Add(v3);
        listaDirezioni.Add(v4);
        listaDirezioni.Add(v5);
        listaDirezioni.Add(v6);
        listaDirezioni.Add(v7);
        listaDirezioni.Add(v8);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);   //Il proiettile del nemico andrà contro al personaggio a prescindere che lui ci sia o si sia spostato
        if ((this.transform.position.x == target.x) && (this.transform.position.y == target.y))
        {

            Invoke("DestroyProjectile", timeToLive);
            attivoSpawn();
        }
    }

    void attivoSpawn()
    {
        //Transform nuovaPosizionePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector2 nuovoTarget = new Vector2(nuovaPosizionePlayer.position.x, nuovaPosizionePlayer.position.y);    
        //projectile.GetComponent<projectileStraight>().setTarget(nuovoTarget);
        if (Time.time > nextSpawn)
            spawnaProiettile(); 
    }


    void spawnaProiettile()
    {
        nextSpawn = Time.time + spawnRate;
        //Transform nuovaPosizionePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector2 nuovoTarget = new Vector2(nuovaPosizionePlayer.position.x, nuovaPosizionePlayer.position.y);
        projectile.GetComponent<projectileStraight>().setTarget(listaDirezioni[Random.Range(0, 8)]);
        
        Instantiate(projectile, transform.position, Quaternion.identity);
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
