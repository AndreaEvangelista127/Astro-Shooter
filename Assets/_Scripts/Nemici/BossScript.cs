using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed;     //Velocità con la quale il nemico insegue il personaggio
    private Transform player;   //Se il personaggio viene seguito
    public float stoppingDistance;
    public int score;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectileDivisorio;
    public GameObject projectileSpawnante;
    public GameObject projectileVettore;
    public GameObject projectileEsplosivo;

    public List<Transform> occhiDiSparo;

    private bool isFacingLeft = true;

    //usato per il flip salvo le variabili della scala all'inizio, così se la dimensione cambia rimarrà invariata
    float localX;
    float localY;

    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    //In target ci va il game object che equivale al personaggio

        timeBtwShots = startTimeBtwShots;

        localY = transform.localScale.y;
        localX = transform.localScale.x;
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

    // Update is called once per frame
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

        if (timeBtwShots <= 0 && distance <= 15f)
        {
            
            sparaProiettile();

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    void sparaProiettile()
    {
        anim.SetTrigger("attack");

        //prendo un valore intorno al personaggio di circa 2 e sparo il proiettile verso quella direzione
        float randomX = Random.Range(player.transform.position.x - 0.1f, player.transform.position.x + 0.1f);
        float randomY = Random.Range(player.transform.position.y - 0.1f, player.transform.position.y + 0.1f);
        Vector3 target = new Vector3(randomX, randomY);


        int i = Random.Range(0, 6);
        if (i == 0 || i == 1)
        {
            int j = Random.Range(0, 3);
            projectileDivisorio.GetComponent<ProiettileDivisorio>().setTarget(target);
            Instantiate(projectileDivisorio, occhiDiSparo[0].position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
        }
        if (i == 2 || i == 5)
        {
            projectileSpawnante.GetComponent<ProiettileSpawn2>().setTarget(target);
            Instantiate(projectileSpawnante, occhiDiSparo[0].position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
        }

        if (i == 3)
        {
            Vector3 target2 = (target - transform.position).normalized;
            projectileVettore.GetComponent<ProiettileVettore>().setTarget(target2);
            Instantiate(projectileVettore, occhiDiSparo[0].position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
        }

        if (i == 4)
        {
            projectileEsplosivo.GetComponent<ProiettileEsplosione>().setTarget(target);
            Instantiate(projectileEsplosivo, occhiDiSparo[0].position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
        }
        timeBtwShots = startTimeBtwShots;

        //projectile.GetComponent<ProiettileVettore>().setTarget(target);
        //Instantiate(projectile, firepoint.position, Quaternion.identity);   //(whatDoWeSpawn, position, rotation)
    }

}