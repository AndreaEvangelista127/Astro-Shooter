using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    [Header("Basic")]
    public float startingHealth;
    public float currentHealth; // { get; private set; }
    public bool isDead = false;

    [Header("GameOver")]
    public GameController GC;  //usato per il gameover

    [Header("BuffSpawnAllaMorte")]
    public List<GameObject> ListaBuff;

    [Header("iFrames")]
    public float iFrameDuration;   //tempo invulnerabilità dopo essere stato colpito
    public int numberOfFlashes;    //numero di flash dopo invulnerabilità
    private SpriteRenderer spriteRend;  //usato per determinare il colore appena viene colpito

    [Header("Audio")]
    public AudioClip death;
    public AudioSource deathPlayer;
    public float volume = 1;

    [Header("Dissolve")]
    Material material;
    Material WeaponMat;

    bool isDissolving = false;
    float dissolve = 1f;
    public SpriteRenderer spriteWeapon;
    ParticleSystem smokeParticle;
    

    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        
    }

    void Start()
    {
        
        smokeParticle = GetComponent<ParticleSystem>();

        if (this.tag == "Player")
        {
            WeaponMat = spriteWeapon.material;
            material = GetComponent<SpriteRenderer>().material;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (isDead && this.tag != "Player")
        {
            //fai spawnareItem
            AudioSource.PlayClipAtPoint(death, transform.position, volume);
            SpawnBuff();
            Destroy(this.gameObject);
            
        }

        if (isDead && this.tag == "Player")
        {
            GetComponent<PlayerMovementConAnimazione>().enabled = false;
            GetComponentInChildren<Weapon>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            isDissolving = true;
            
        }

        if (isDissolving)
        {
            dissolve -= Time.deltaTime;
            if(dissolve <= 0f)
            {
                dissolve = 0f;
                isDissolving = false;
            }

            material.SetFloat("_Dissolve", dissolve);
            WeaponMat.SetFloat("_Dissolve", dissolve);
            smokeParticle.Stop();
            
        }

    }

    public void takeDamge(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        //se la vita è del giocatore
        if (this.tag == "Player")
        {
            Debug.Log("danni presi = " + damage);
            StartCoroutine(Invulnerability());

            //se muore il giocatore
            if (currentHealth <= 0)
            {
                //GamerOver();
                //CREA IL GAMEOVER, in cui ritorni alla schermata home e ti appare una scritta con il tuo punteggio
                //e il round a cui sei arrivato
                isDead = true;
                deathPlayer.Play();
                GC.GameOver();

            }


        }

        //se la vita non è del giocatore ma di un nemico per esempio
        if (this.tag != "Player")
        {
            if (currentHealth > 0)
            {

                //EnemyHurt
                //riga di animazione di danni
                //riga iframes

            }
            else
            {
                if (!isDead)
                {
                    //riga di animazione di morte
                    //GetComponent<playerMovement>().enabled = false; //disattiva movimento del giocatore
                    isDead = true;
                }
            }
        }
    }



    public void AddHealth(float _valore)
    {
        currentHealth = Mathf.Clamp(currentHealth + _valore, 0, startingHealth);
    }


    public void SpawnBuff()
    {
        Debug.Log("spawnBuff");
        int i = Random.Range(0, 3);
        if (i == 1)
        {
            Debug.Log("SpawnBuff");
            i = Random.Range(0, 5);
            Instantiate(ListaBuff[i], this.transform.position, Quaternion.identity);
        }
    }


    private IEnumerator Invulnerability()   //da capire cosa è IEnumerator
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);  //metti i due layer come parametri che non devono collidere (così che magari non trapassa i muri), che sono player e enemy
        //durata invulnerabilità

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);  //cambio colore in rosso

            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));  //così si aspett aprima che il codce fa eseguire la prossima linea di codice

            spriteRend.color = new Color(255, 255, 0, 255);

            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        spriteRend.color = new Color(255, 255, 255, 255);

        Physics2D.IgnoreLayerCollision(6, 7, false);  //rimetti le collisionià
    }
}