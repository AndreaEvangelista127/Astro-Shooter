using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NORMALE SCRIPT DEL NEMICO IN CUI SPARA DIRETTAMENTE VERSO VERSO IL PLAYER
public class Nemico : MonoBehaviour
{

    public float speed;     //Velocità con la quale il nemico insegue il personaggio
    private Transform player;   //Se il personaggio viene seguito
    public float stoppingDistance;
    public int score;
    //public float retreatDistance;   //Distanza dopo la quale il nemico si allontana dal personaggio

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    //In target ci va il game object che equivale al personaggio

        timeBtwShots = startTimeBtwShots;
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
            GetComponentInChildren<FloatingScore>().showScore(score);

        }

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)  //Controllo a che distanza sta il nemico dal personaggio, se sta a più di 3 il nemico può continuare ad inseguire il personaggio
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);  //Muove il nemico dalla sua posizione a quella del target(personaggio) ad una certa velocità

        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance /*&& Vector2.Distance(transform.position, player.position) > retreatDistance*/)
        {
            transform.position = this.transform.position;

        } /*else if (Vector2.Distance(transform.position, player.position) < retreatDistance) //Se il nemico è troppo vicino al personaggio, si allontana
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);  //Il nemico dalla si allontana dal personaggio ad una velocità opposta a quella del personaggio
        }*/

        if(timeBtwShots <= 0)
        {
            projectile.GetComponent<ProjectileTarget>().enabled = true;
            projectile.GetComponent<ProiettileVettore>().enabled = false;

            Instantiate(projectile, transform.position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

}
