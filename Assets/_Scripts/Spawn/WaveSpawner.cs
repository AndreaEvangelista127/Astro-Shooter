using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour 
{
    public List<Enemy> nemici = new List<Enemy>();
    public int currentWave;
    private int waveValue;    //token da usare per ogni wave
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public List<Transform> SpawnPoints = new List<Transform>();
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public Transform spawnLocation;

    public GameObject testoRound;
    

    //variabili per far partire la wave dopo l'eliminazione di tutti i nemici
    // private bool killedAlEnemis = false;
    // int enemiesLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame           //ATTENZIONE è stato modficato da update a FIXEDupdate
    void FixedUpdate()
    {

        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                //creo numero casuale da scegliere per lo spawn dalla lista
              //spawnIndex = Random.Range(0, EnemySpawners.Count);
                int index = Random.Range(0,SpawnPoints.Count);
                Transform spawnAttuale = SpawnPoints[index];
                //nuova
                Instantiate(enemiesToSpawn[0], spawnAttuale.position, Quaternion.identity);
                //vecchia
        //      Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;  //se non rimangono nemici fai finire la wave
            }
        }

        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        // da qua inizia la prossima wave

        //controllo nemici su schermo
        GameObject[] nemiciRimasti = GameObject.FindGameObjectsWithTag("Enemy");


        if (enemiesToSpawn.Count == 0 && nemiciRimasti.Length == 0)
        {
            currentWave += 1;
            GenerateWave();
        }
        
    }

    public void GenerateWave()
    {
        //waveValue = currentWave * 10;   //attenzione questo determina quanti nemici spawnano

        //fai partire la coroutine per il testo del round
        StartCoroutine(TestoRound());

        waveValue = currentWave * 5;

        GenerateEnemis();

        //più la wave è grande più velocemente spawnano
        // spawnInterval = waveDuration / enemiesToSpawn.Count;  //gives a fixed time between each enemies
        Debug.Log("wave duration ="+(waveDuration));

        Debug.Log("wave duration divisa =" + waveDuration / (enemiesToSpawn.Count / 2));
        if (waveDuration / (enemiesToSpawn.Count / 2) >= 5)
        {
            //poco più veloce a spawnare i mob nelle prime ondate
            spawnInterval = waveDuration / enemiesToSpawn.Count;
        }           
        else
        {
            //rallenta un po nelle ondate avanzate il ritmo di spawn dei mob
            spawnInterval = waveDuration / (enemiesToSpawn.Count / 2);

            //NOTA:puoi quindi qui decidere quanto dura l'intervallo di spawn dei mostri, i quali, più sono presenti
            //da far spawnare più velocemente spawnano
        }



        waveTimer = waveDuration;
    }

    public void GenerateEnemis()
    {
        //crea una lista temporanea di nemici da generare

        List<GameObject> nemiciGenerati = new List<GameObject>();
        while(waveValue > 0)
        {
            int randEnemyId = Random.Range(0, nemici.Count);

            //Debug.Log("randEnemyId assegnato : " + randEnemyId);

            int randEnemyCost = nemici[randEnemyId].cost;    //cost si riferisce al costo di spawn (come se fosse un token per spawnare i nemici), è una variabile dentro Nemico


            if (waveValue - randEnemyCost >= 0)
            {
                nemiciGenerati.Add(nemici[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = nemiciGenerati;
    }


    [System.Serializable]

    public class Enemy
    {
        public GameObject enemyPrefab;
        public int cost;
    }

    private IEnumerator TestoRound()
    {
        GameObject testo = testoRound;
        testo.SetActive(true);
        testo.GetComponent<TextMesh>().text = ("Round " + currentWave);
        yield return new WaitForSeconds(3);
        testo.SetActive(false);
    }
}
