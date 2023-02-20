using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemicoCorpo : MonoBehaviour
{
    //id univoco
    // public int id; 

    public float speed;     //Velocità con la quale il nemico insegue il personaggio
    private Transform player;   //Se il personaggio viene seguito
    public float stoppingDistance;
    public int score;
    //public float retreatDistance;   //Distanza dopo la quale il nemico si allontana dal personaggio
    bool isFacingLeft = true;

    public Animator anim;

    private float salute;

    private bool isMoving;

    //wave
    public int cost; //costo per far spawnare il nemico, usato per lo wave spawn
    public GameObject enemyPrefab;

    //usato per il flip salvo le variabili della scala all'inizio, così se la dimensione cambia rimarrà invariata
    float localX;
    float localY;

    // Start is called before the first frame update
    void Start()
    {
        //Aggiunto oggi
        //anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;    //In target ci va il game object che equivale al personaggio

        localX = transform.localScale.x;
        localY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
       

        //se è morto assegna il punteggio
        if (this.GetComponent<health>().isDead)
        {
            Debug.Log("Assegno punteggio" + score);
            //chiama metodo per aumentare il punteggio
            ScoreManager.instance.AddPoint(score);
            GetComponent<FloatingScore>().showScore(score);


        }

        //flip quando ti sposti si flippa
        if (player.transform.position.x > transform.position.x && isFacingLeft)
        {
            transform.localScale = new Vector3(-localX, localY, 1);
            isFacingLeft = false;
        }

        if (player.transform.position.x < transform.position.x && !isFacingLeft)
        {
            transform.localScale = new Vector3(localX, localY, 1);
            isFacingLeft = true;
        }



        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)  //Controllo a che distanza sta il nemico dal personaggio, se sta a più di 3 il nemico può continuare ad inseguire il personaggio
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);  //Muove il nemico dalla sua posizione a quella del target(personaggio) ad una certa velocità

        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance /*&& Vector2.Distance(transform.position, player.position) > retreatDistance*/)
        {
            transform.position = this.transform.position;

        } /*else if (Vector2.Distance(transform.position, player.position) < retreatDistance) //Se il nemico è troppo vicino al personaggio, si allontana
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);  //Il nemico dalla si allontana dal personaggio ad una velocità opposta a quella del personaggio
        }*/

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < stoppingDistance)
        {
            //Debug.Log("Sono qui");
            anim.SetBool("isMoving", false);
        }
        else
        {
            //Debug.Log("Sono anche qui");
            anim.SetBool("isMoving", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Giocatore colpito");
        if (collision.tag == "Player")
        {
            Debug.Log("Sto attaccando");
            anim.SetTrigger("attack");
            collision.GetComponent<health>().takeDamge(1);
            //Destroy(this.gameObject);
        }
        else if (collision.tag == "Proiettile Amico")
        {
            //salute = this.gameObject.GetComponent<health>().stampaSalute();
            //if (salute == 0)
            //{
            //    anim.SetTrigger("attack");
            //}
        }
    }
}
