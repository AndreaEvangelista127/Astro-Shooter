using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject Nemici;
    public GameObject NemiciCorpo;
    public List<Transform> EnemySpawners;
    public List<Transform> EnemyCorpoSpawners;
    private int spawnIndex;

    public float IntervalloNemici = 2.0f;  //Il tempo che bisogna aspettare tra uno spawn e l'altro
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(IntervalloNemici, Nemici));
        StartCoroutine(spawnEnemyCorpo(IntervalloNemici, NemiciCorpo));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)    //Abbiamo l'intervallo e il game object dei nemici come parametri
    {
        if(Nemici is not null)
        {
            yield return new WaitForSeconds(interval);

            spawnIndex = Random.Range(0, EnemySpawners.Count);

            GameObject newEnemy = Instantiate(enemy, EnemySpawners[spawnIndex].position, Quaternion.identity); //Instantiate ci permette di instanziare un nuovo game object di tipo Enemy in una certa posizione
                                                                                                                                                                    //Abbiamo un nuovo vettore che ha valori random che possono variare in quel range impostato da noi
            StartCoroutine(spawnEnemy(interval, newEnemy));    //Inizia così la routine di spawn, in caso volessimo farla fermare basterebbe un timer dopo il quaule si fermerebbe lo spawn
        }
        else
        {
            StopCoroutine(spawnEnemy(interval, enemy));
        }
    }

    private IEnumerator spawnEnemyCorpo(float interval, GameObject enemyCorpo)    //Abbiamo l'intervallo e il game object dei nemici come parametri
    {
        if (NemiciCorpo is not null)
        {
            yield return new WaitForSeconds(interval);

            spawnIndex = Random.Range(0, EnemyCorpoSpawners.Count);

            GameObject newEnemyCorpo = Instantiate(enemyCorpo, EnemyCorpoSpawners[spawnIndex].position, Quaternion.identity); //Instantiate ci permette di instanziare un nuovo game object di tipo Enemy in una certa posizione
                                                                                                               //Abbiamo un nuovo vettore che ha valori random che possono variare in quel range impostato da noi
            StartCoroutine(spawnEnemyCorpo(interval, newEnemyCorpo));    //Inizia così la routine di spawn, in caso volessimo farla fermare basterebbe un timer dopo il quaule si fermerebbe lo spawn
        }
        else
        {
            StopCoroutine(spawnEnemy(interval, enemyCorpo));
        }
    }
}