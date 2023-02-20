using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileSpawn2 : MonoBehaviour
{

    public float speed;
    private Transform player;
    private Vector2 target;
    public float timeToLive;
    public float dannoProiettile;

    public GameObject projectile;
    public float spawnRate = 1f;
    private float nextSpawn = 0f;

    static int posizioneDirezione = 0;
     int a;

    //creo le 4 direszioni oblique su cui poi può andare
    Vector2 v1 = new Vector2(2f, 2f);
    Vector2 v2 = new Vector2(2f, -2f);
    Vector2 v3 = new Vector2(-2f, -2f);
    Vector2 v4 = new Vector2(-2f, 2f);

    //creo le 4 direzioni verticali-orizzionatali
    Vector2 v5 = new Vector2(2.5f, 0f);
    Vector2 v6 = new Vector2(0f, -2.5f);
    Vector2 v7 = new Vector2(-2.5f, 0f);
    Vector2 v8 = new Vector2(0f, 2.5f);
    List<Vector2> listaDirezioni = new List<Vector2>();
    List<Vector2> listaDirezioni2 = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        a = Random.Range(0, 2);
        listaDirezioni.Add(v1);
        listaDirezioni.Add(v5);
        listaDirezioni.Add(v2);
        listaDirezioni.Add(v6);
        listaDirezioni.Add(v3);
        listaDirezioni.Add(v7);
        listaDirezioni.Add(v4);
        listaDirezioni.Add(v8);

        listaDirezioni2.Add(v8);
        listaDirezioni2.Add(v4);
        listaDirezioni2.Add(v7);
        listaDirezioni2.Add(v3);
        listaDirezioni2.Add(v6);
        listaDirezioni2.Add(v2);
        listaDirezioni2.Add(v5);
        listaDirezioni2.Add(v1);
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
        if (Time.time > nextSpawn)
            spawnaProiettile();
    }


    void spawnaProiettile()
    {
        nextSpawn = Time.time + spawnRate;
        //Transform nuovaPosizionePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector2 nuovoTarget = new Vector2(nuovaPosizionePlayer.position.x, nuovaPosizionePlayer.position.y);
        if(a == 0)
        {
            projectile.GetComponent<projectileStraight>().setTarget(listaDirezioni2[posizioneDirezione]);
            Instantiate(projectile, transform.position, Quaternion.identity);
            posizioneDirezione++;
            if (posizioneDirezione > 7)
                posizioneDirezione = 0;
        }

        if(a == 1)
        {
            projectile.GetComponent<projectileStraight>().setTarget(listaDirezioni[posizioneDirezione]);
            Instantiate(projectile, transform.position, Quaternion.identity);
            posizioneDirezione++;
            if (posizioneDirezione > 7)
                posizioneDirezione = 0;

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
