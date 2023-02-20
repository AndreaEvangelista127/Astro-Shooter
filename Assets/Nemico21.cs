using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPT MODIFICATO DEL NEMICO IN CUI SPARA IN MANIERA SEMI CASUALE MA COMUNQUE VICINO AL PERSONAGGIO
public class Nemico21 : MonoBehaviour
{

    public float speed;     //Velocità con la quale il nemico insegue il personaggio
    private Transform player;   //Se il personaggio viene seguito
    public float stoppingDistance;
    public int score;
    public Vector3 target;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private bool isFacingLeft = true;

    [SerializeField]
    private Animator anim;

    public Transform firepoint;

    public GameObject projectile;
    public GameObject projectileSecondario;

    //usato per il flip salvo le variabili della scala all'inizio, così se la dimensione cambia rimarrà invariata
    float localX;
    float localY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    //In target ci va il game object che equivale al personaggio
        timeBtwShots = startTimeBtwShots;

        localX = transform.localScale.x;
        localY = transform.localScale.y;
    }


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
    }


    void FixedUpdate()
    {

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

        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < stoppingDistance)
        {

            anim.SetBool("isMoving", false);
        }
        else
        {

            anim.SetBool("isMoving", true);
        }

        if (timeBtwShots <= 0 && distance <= 15f)   //aggiunto && per fare sparare il nemico quando è abbastanza vicino
        {
            sparaPriettile();
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }



    void sparaPriettile()
    {
        //prendo un valore intorno al personaggio di circa 2 e sparo il proiettile verso quella direzione
        float randomX = Random.Range(player.transform.position.x - 0.1f, player.transform.position.x + 0.1f);
        float randomY = Random.Range(player.transform.position.y - 0.1f, player.transform.position.y + 0.1f);
        target = new Vector3(randomX, randomY);


        int i = Random.Range(0, 4);
        if (i == 0)
        {
            //spara proiettile esplosivo
            projectileSecondario.GetComponent<ProiettileEsplosione>().setTarget(target);
            Instantiate(projectileSecondario, transform.position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
        }


        //Vector2 target2 = (player.position - transform.position).normalized;
        Vector3 target2 = (target - transform.position).normalized;

        projectile.GetComponent<ProiettileVettore>().setTarget(target2);

        Instantiate(projectile, transform.position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
        timeBtwShots = startTimeBtwShots;
    }


}
